using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BaristaLabs.ChromeDevTools.Runtime;
using System.Net.Http;
using Newtonsoft.Json;
using Page = BaristaLabs.ChromeDevTools.Runtime.Page;
using Runtime = BaristaLabs.ChromeDevTools.Runtime.Runtime;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using BaristaLabs.ChromeDevTools.Runtime.DOM;
using Gripper.Authenticated.Browser.ProcessManagement;
using System.Collections.Concurrent;
using BaristaLabs.ChromeDevTools.Runtime.Page;
using System.IO;
using BaristaLabs.ChromeDevTools.Runtime.Runtime;

namespace Gripper.Authenticated.Browser.BaristaLabsCdtr
{
    public partial class CdtrChromeClient : IWebClient
    {
        #region Private

        private ILogger _logger;
        private ILoggerFactory _loggerFactory;
        private ICdtrElementFactory _cdtrElementFactory;
        private ICdtrContextFactory _cdtrContextFactory;
        private IJsBuilder _jsBuilder;

        private ChromeSession _chromeSession;
        private Process _chromeProcess;

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        private bool _hasDisposalStarted;
        private ConcurrentDictionary<int, ExecutionContextDescription> _executionContexts;
        private Action<FrameStoppedLoadingEvent> _frameStoppedLoading;
        //private IContext? _mainContext;


        public IContext MainContext
        {
            get
            {
                _logger.LogDebug("{myName} running {name} to get main context.", nameof(CdtrChromeClient), nameof(GetContextsAsync));
                var contexts = GetContextsAsync().Result;
                if (!contexts.Any())
                {
                    throw new ApplicationException($"{nameof(GetContextsAsync)} returned no contexts.");
                }
                return contexts.First();
            }
        }

        private async Task SubscribeToRdpEventsAsync()
        {
            await _chromeSession.Network.Enable(
                new BaristaLabs.ChromeDevTools.Runtime.Network.EnableCommand { },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: _cancellationToken);

            _chromeSession.Network.SubscribeToRequestWillBeSentEvent(x =>
            {
                WebClientEvent?.Invoke(this, new Network_RequestWillBeSentEventArgs(x.RequestId, x.Request.Headers, x.Request.Method, x.Request.Url));
            });

            await _chromeSession.Page.Enable(throwExceptionIfResponseNotReceived: false, cancellationToken: _cancellationToken);

            _chromeSession.Page.SubscribeToFrameStoppedLoadingEvent(x => _frameStoppedLoading?.Invoke(x));
            _frameStoppedLoading += x => _logger.LogDebug("Frame stopped loading: {id}", x.FrameId);

            await _chromeSession.Runtime.Enable(throwExceptionIfResponseNotReceived: false, cancellationToken: _cancellationToken);

            _chromeSession.Runtime.SubscribeToExecutionContextCreatedEvent(x =>
            {
                _logger.LogDebug("execution context created: {id}, {name}, {origin}.", x.Context.Id, x.Context.Name, x.Context.Origin);
                _executionContexts[(int)x.Context.Id] = x.Context;
            });

            _chromeSession.Runtime.SubscribeToExecutionContextDestroyedEvent(x =>
            {
                _logger.LogDebug("execution context destroyed: {id}.", x.ExecutionContextId);
                var contextId = x.ExecutionContextId;
                _executionContexts.TryRemove((int)contextId, out _);
            });

            await _chromeSession.DOM.Enable(throwExceptionIfResponseNotReceived: false);

            _chromeSession.DOM.SubscribeToSetChildNodesEvent(x =>
            {
                _logger.LogDebug("set child node: Parent: {nodeParent}, nodes: {nodes}", x.ParentId, string.Join(' ', x.Nodes.Select(x => x.NodeId + " " + x.NodeName + " " + x.NodeValue)));
            });
        }

        /// <summary>
        /// Continuously enforce that events triggered on children iFrames are captured.
        /// Make sure to <see cref="SubscribeToRdpEventsAsync"/>
        /// </summary>
        /// <param name="targetAttachment">What strategy shall be used.</param>
        /// <returns></returns>
        private void SetupTargetAttachment(TargetAttachmentMode targetAttachment)
        {
            switch (targetAttachment)
            {
                case TargetAttachmentMode.Default:
                case TargetAttachmentMode.Auto:
                    _frameStoppedLoading += async x =>
                    {
                        await _chromeSession.Target.SetAutoAttach(new BaristaLabs.ChromeDevTools.Runtime.Target.SetAutoAttachCommand
                        {
                            AutoAttach = true,
                            // Setting WaitForDebuggerOnStart = true requires releasing stalled frames by calling Runtime.RunIfWaitingForDebugger
                            WaitForDebuggerOnStart = false, 
                            Flatten = true
                        },
                        throwExceptionIfResponseNotReceived: false,
                        cancellationToken: _cancellationToken);

                    };

                    break;

                case TargetAttachmentMode.SeekAndAttach:
                    LoopSeekAndAttachTargetsAsync(TimeSpan.FromSeconds(10));
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private async Task LoopSeekAndAttachTargetsAsync(TimeSpan loopPeriod)
        {
            while (true)
            {
                await Task.Delay(loopPeriod);
                var targets = await _chromeSession.Target.GetTargets(throwExceptionIfResponseNotReceived: false, cancellationToken: _cancellationToken);

                _logger.LogDebug("Found targets: {targetsCount} of which attached: {attachedTargetsCount}", targets.TargetInfos.Length, targets.TargetInfos.Count(x => x.Attached));

                foreach (var target in targets.TargetInfos.Where(x => !x.Attached))
                {
                    _logger.LogDebug("Attaching to target: {targetId}", target.TargetId);
                    await _chromeSession.Target.AttachToTarget(new BaristaLabs.ChromeDevTools.Runtime.Target.AttachToTargetCommand
                    {
                        TargetId = target.TargetId
                    },
                    throwExceptionIfResponseNotReceived: false,
                    cancellationToken: _cancellationToken);
                }
            }
        }
        private void DoPreStartupCleanup(DirectoryInfo userDataDir, BrowserCleanupSettings startupCleanup)
        {
            try
            {
                if (startupCleanup == BrowserCleanupSettings.None)
                {
                    _logger.LogDebug("{name} set to {value}. Exiting {this}.", nameof(BrowserCleanupSettings), BrowserCleanupSettings.None, nameof(DoPreStartupCleanup));
                    return;
                }

                if (!userDataDir.Exists)
                {
                    return;
                }

                var profileDirectory = userDataDir.GetDirectories().FirstOrDefault(x => x.Name.ToLower() == "default");

                if (profileDirectory == null)
                {
                    return;
                }

                if (startupCleanup.HasFlag(BrowserCleanupSettings.Profile))
                {
                    profileDirectory.Delete(true);
                    return;
                }

                if (startupCleanup.HasFlag(BrowserCleanupSettings.Cache))
                {
                    _logger.LogWarning("Clearing browser cache.");
                    profileDirectory.GetDirectories().FirstOrDefault(x => x.Name.ToLower() == "cache")?.Delete(true);
                    profileDirectory.GetDirectories().FirstOrDefault(x => x.Name.ToLower() == "storage")?.Delete(true);
                    profileDirectory.GetDirectories().FirstOrDefault(x => x.Name.ToLower() == "session storage")?.Delete(true);
                    profileDirectory.GetDirectories().FirstOrDefault(x => x.Name.ToLower() == "sessions")?.Delete(true);
                    profileDirectory.GetDirectories().FirstOrDefault(x => x.Name.ToLower() == "local storage")?.Delete(true);
                }

                if (startupCleanup.HasFlag(BrowserCleanupSettings.Cookies))
                {
                    _logger.LogWarning("Clearing browser cookies.");
                    profileDirectory.GetFiles().FirstOrDefault(x => x.Name.ToLower() == "cookies")?.Delete();
                    profileDirectory.GetFiles().FirstOrDefault(x => x.Name.ToLower() == "cookies-journal")?.Delete();
                }

                if (startupCleanup.HasFlag(BrowserCleanupSettings.Logins))
                {
                    _logger.LogWarning("Clearing browser logins.");
                    profileDirectory.GetFiles().FirstOrDefault(x => x.Name.ToLower() == "login data")?.Delete();
                    profileDirectory.GetFiles().FirstOrDefault(x => x.Name.ToLower() == "login data for account")?.Delete();
                    profileDirectory.GetFiles().FirstOrDefault(x => x.Name.ToLower() == "login data for account-journal")?.Delete();
                    profileDirectory.GetFiles().FirstOrDefault(x => x.Name.ToLower() == "login data-journal")?.Delete();
                }

            }
            catch (Exception e)
            {
                _logger.LogCritical("Failed to {name}.", nameof(DoPreStartupCleanup));
                throw;
            }
        }

        // TODO: replace this with a separated launcher that will return references to services on demand.
        private async Task<(Process, ChromeSession, ICdtrContextFactory)> LaunchAsync(WebClientSettings settings)
        {
            DoPreStartupCleanup(settings.UserDataDir, settings.StartupCleanup);

            var browserArgs = new StringBuilder()
                .Append(" --remote-debugging-port=").Append(settings.RemoteDebuggingPort)
                .Append(" --user-data-dir=").Append(settings.UserDataDir.FullName);

            switch (settings.TargetAttachment)
            {
                case TargetAttachmentMode.Default:
                case TargetAttachmentMode.Auto:
                    browserArgs.Append(" --disable-features=IsolateOrigins,site-per-process");
                    break;

                case TargetAttachmentMode.SeekAndAttach:
                    break;

                default:
                    throw new NotImplementedException();
            }

            if (settings.UseProxy)
            {
                _logger.LogDebug("{this} launching chrome with proxy: {proxy}.", nameof(CdtrChromeClient), settings.Proxy?.Address?.ToString() ?? "null");
                browserArgs.Append(" --proxy-server=").Append(settings.Proxy?.Address ?? throw new ApplicationException("Null proxy when UseProxy flag was up."));

                if (browserArgs[^1] == '/')
                {
                    browserArgs.Remove(browserArgs.Length - 1, 1);
                }
            }
            else
            {
                _logger.LogDebug("{this} launching chrome without proxy.", nameof(CdtrChromeClient));

            }

            _logger.LogDebug("{this} launching chrome with args: {args}", nameof(CdtrChromeClient), browserArgs.ToString());

            var chromeProcess = Process.Start(settings.BrowserLocation, browserArgs.ToString());

            ChildProcessTracker.AddProcess(chromeProcess);

            using var httpClient = new HttpClient();
            var remoteSessions = await httpClient.GetAsync($"http://localhost:{settings.RemoteDebuggingPort}/json");

            _logger.LogDebug("sessions response: {status}", remoteSessions.StatusCode);

            var remoteSessionsContent = await remoteSessions.Content.ReadAsStringAsync();

            var sessionInfos = JsonConvert.DeserializeObject<List<ChromeSessionInfo>>(remoteSessionsContent);

            _logger.LogDebug("Remote session infos: {remoteSessions}", remoteSessionsContent);

            var chromeSession = new ChromeSession(_loggerFactory.CreateLogger<ChromeSession>(), sessionInfos.First(x => x.Type == "page").WebSocketDebuggerUrl);

            _logger.LogDebug("ChromeSession launched and connected to Chrome RDP server");

            //DoPostStartupCleanupAsync(chromeSession, settings.StartupCleanup).Wait();

            var contextFactory = new CdtrContextFactory(_loggerFactory, _cdtrElementFactory, _jsBuilder, chromeSession);

            return (chromeProcess, chromeSession, contextFactory);
        }
        #endregion

        #region Ctor

        public CdtrChromeClient(
            ILoggerFactory loggerFactory,
            ICdtrElementFactory cdtrElementFactory,
            IJsBuilder jsBuilder,
            WebClientSettings settings)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<CdtrChromeClient>();
            _cdtrElementFactory = cdtrElementFactory;
            _jsBuilder = jsBuilder;

            (_chromeProcess, _chromeSession, _cdtrContextFactory) = LaunchAsync(settings).Result;

            _executionContexts = new ConcurrentDictionary<int, ExecutionContextDescription>();

            _logger.LogDebug("Subscribing to rdp events.");

            SubscribeToRdpEventsAsync().Wait();
            SetupTargetAttachment(settings.TargetAttachment);

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            _hasDisposalStarted = false;

            GoToUrlAsync(settings.Homepage, settings.DefaultPageLoadPollSettings, CancellationToken.None).Wait();

            _logger.LogDebug("Exiting {this} constructor.", nameof(CdtrChromeClient));

        }

        #endregion

        #region Public IWebClient implementation

        public event EventHandler<RdpEventArgs>? WebClientEvent;

        public async Task<CookieContainer> GetAllCookiesAsync()
        {
            try
            {
                var rawCookies = await _chromeSession.Network.GetAllCookies(new BaristaLabs.ChromeDevTools.Runtime.Network.GetAllCookiesCommand { }, throwExceptionIfResponseNotReceived: false, cancellationToken: _cancellationToken);

                _logger.LogDebug("raw cookies: {rawCookies}", rawCookies?.Cookies?.Length.ToString() ?? "null");

                var cookieContainer = new CookieContainer();

                if (rawCookies?.Cookies == null)
                {
                    return cookieContainer;
                }

                foreach (var cookie in rawCookies.Cookies)
                {
                    if (cookie == null)
                    {
                        continue;
                    }

                    cookieContainer.Add(new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
                }

                return cookieContainer;
            }
            catch (Exception e)
            {
                _logger.LogError("Exception getting all cookies: {e}", e);
                throw;
            }

        }

        public async Task<string?> GetCurrentUrlAsync()
        {
            try
            {
                var navigationHistory = await _chromeSession.Page.GetNavigationHistory(new Page.GetNavigationHistoryCommand
                {

                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: _cancellationToken);

                var currentIndex = navigationHistory?.CurrentIndex;
                var entries = navigationHistory?.Entries;

                if (currentIndex == null || entries == null)
                {
                    return null;
                }

                var currentEntry = entries[(int)currentIndex];
                return currentEntry.Url;
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to get current Url: {e}", e);
                throw;
            }
        }

        public async Task GoToUrlAsync(string address, PollSettings pollSettings, CancellationToken cancellationToken)
        {
            try
            {

                var loadStopwatch = Stopwatch.StartNew();

                var loadedFramesIds = new ConcurrentDictionary<string, byte>();

                Action<FrameStoppedLoadingEvent> frameStoppedLoading = x =>
                {
                    loadedFramesIds[x.FrameId] = 0;
                };

                _frameStoppedLoading += frameStoppedLoading;

                var navigateCommandResponse = await _chromeSession.Page.Navigate(new Page.NavigateCommand
                {
                    Url = address
                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: _cancellationToken);

                var frameId = navigateCommandResponse.FrameId;

                bool FramesLoaded(FrameTree frameTree)
                {
                    // empty or unloaded
                    if (frameTree?.Frame?.Id == null || !loadedFramesIds.ContainsKey(frameTree.Frame.Id))
                    {
                        return false;
                    }

                    // leaf or loaded children
                    return frameTree.ChildFrames == null || frameTree.ChildFrames.All(x => FramesLoaded(x));
                }

                while (!cancellationToken.IsCancellationRequested)
                {
                    var timedOut = loadStopwatch.ElapsedMilliseconds > pollSettings.TimeoutMs;

                    if (timedOut)
                    {
                        _logger.LogInformation("{name} waiting for new iFrames timed out. Some may have not been attached. Consider relaxing the {timeout} parameter.", nameof(GoToUrlAsync), nameof(PollSettings.TimeoutMs));
                        break;
                    }

                    await Task.Delay(pollSettings.PeriodMs, cancellationToken);

                    var frameTreeResult = await _chromeSession.Page.GetFrameTree(throwExceptionIfResponseNotReceived: false);

                    var framesCount = loadedFramesIds.Count;

                    var framesLoaded = FramesLoaded(frameTreeResult.FrameTree);

                    _logger.LogDebug("Loaded frames ids: {framesCount}. All loaded: {frameTreeResult}", framesCount, framesLoaded);

                    if (framesLoaded)
                    {
                        await Task.Delay(pollSettings.PeriodMs, cancellationToken);

                        // Some previously unseen frames might have been attached during the delay.

                        var verificationFrameTreeResult = await _chromeSession.Page.GetFrameTree(throwExceptionIfResponseNotReceived: false);

                        var framesLoadedVerification = FramesLoaded(verificationFrameTreeResult.FrameTree);

                        _logger.LogDebug("Verification: Loaded frames ids: {framesCount}. All loaded: {frameTreeResult}", loadedFramesIds.Count, framesLoadedVerification);

                        if (framesLoadedVerification && loadedFramesIds.Count == framesCount)
                        {
                            break;
                        }
                    }
                }

                _logger.LogDebug("{name} removing delegate from execution list.", nameof(GoToUrlAsync));
                _logger.LogDebug("Exiting {name} in {elapsed}", nameof(GoToUrlAsync), loadStopwatch.Elapsed);

                Delegate.Remove(_frameStoppedLoading, frameStoppedLoading);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to {name}: {e}", nameof(GoToUrlAsync), e);
                throw;
            }
        }

        public async Task ReloadAsync(PollSettings pollSettings, CancellationToken cancellationToken)
        {
            try
            {
                await _chromeSession.Page.Reload(new Page.ReloadCommand { }, throwExceptionIfResponseNotReceived: false, cancellationToken: _cancellationToken);
                //await Task.Run(() => _pageLoaded.WaitOne());
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to reload: {e}", e);
                throw;
            }
        }

        public Task EnterFullScreenAsync()
        {
            throw new NotImplementedException();
        }


        public async Task<IReadOnlyCollection<IContext>> GetContextsAsync()
        {
            var frameTree = (await _chromeSession.Page.GetFrameTree(throwExceptionIfResponseNotReceived: false)).FrameTree;

            List<Frame> AddFrames(List<Frame> frames, FrameTree frameTree)
            {
                frames.Add(frameTree.Frame);
                if (frameTree?.ChildFrames != null && frameTree.ChildFrames.Any())
                {
                    foreach (var child in frameTree.ChildFrames)
                    {
                        AddFrames(frames, child);
                    }
                }
                return frames;
            }

            var frames = new List<Frame>();
            AddFrames(frames, frameTree);

            List<IContext> contexts = new();

            foreach (var frame in frames)
            {
                var description = _executionContexts.FirstOrDefault(x => ((JObject)x.Value.AuxData)["frameId"]?.ToString() == frame.Id);
                if (description.Value != null && _cdtrContextFactory.TryCreateContext(description.Key, frame, out IContext? context))
                {
                    contexts.Add(context ?? throw new ApplicationException($"{nameof(CdtrContextFactory.TryCreateContext)} returned true, {nameof(context)} cannot be null."));
                }
            }

            return contexts;
        }

        public async Task<JToken> ExecuteRdpCommandAsync(string commandName, JToken commandParams)
        {
            try
            {
                var resultToken = await Task.Run(() => _chromeSession.SendCommand(commandName, commandParams, throwExceptionIfResponseNotReceived: false, cancellationToken: _cancellationToken));
                return resultToken;
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to execute rdp command: {e}", e);
                throw;
            }
        }

        public void Dispose()
        {
            if (_hasDisposalStarted)
            {
                _logger.LogDebug("Not trigerring {this} dispose. Disposal already started on another thread.", nameof(CdtrChromeClient));
                return;
            }

            _hasDisposalStarted = true;

            _logger.LogWarning("Disposing {this}.", nameof(CdtrChromeClient));

            _cancellationTokenSource.Cancel();

            try
            {
                _chromeProcess.KillTree();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to kill chrome process tree: {e}", e);
            }
            try
            {
                _chromeSession.Dispose();
            }
            catch (ObjectDisposedException)
            {
                _logger.LogDebug("ObjectDisposedException when disposing {object} at {this}.", nameof(_chromeSession), nameof(CdtrChromeClient));
            }
            catch (Exception e)
            {
                _logger.LogError("Error disposing {this}: {e}", nameof(_chromeSession), e);
            }
        }


        #endregion
    }
}

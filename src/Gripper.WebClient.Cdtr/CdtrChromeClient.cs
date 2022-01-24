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
using Gripper.WebClient.ProcessManagement;
using System.Collections.Concurrent;
using BaristaLabs.ChromeDevTools.Runtime.Page;
using System.IO;
using BaristaLabs.ChromeDevTools.Runtime.Runtime;
using Microsoft.Extensions.Options;

namespace Gripper.WebClient.Cdtr
{
    public partial class CdtrChromeClient : IWebClient
    {
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IElementFactory _cdtrElementFactory;
        private readonly IContextFactory _cdtrContextFactory;
        private readonly IJsBuilder _jsBuilder;

        private readonly IBrowserManager _browserManager;
        private readonly ChromeSession _chromeSession;
        private readonly Process _chromeProcess;

        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _cancellationToken;

        private bool _hasDisposalStarted;
        private Action<FrameStoppedLoadingEvent>? _frameStoppedLoading;

        public IContext? MainContext
        {
            get
            {
                _logger.LogDebug("{myName} running {name} to get main context.", nameof(CdtrChromeClient), nameof(GetContextsAsync));
                var contexts = GetContextsAsync().Result;
                if (!contexts.Any())
                {
                    _logger.LogWarning("{name} returned no contexts.", nameof(GetContextsAsync));
                    return null;
                }
                return contexts.First();
            }
        }

        public CdtrChromeClient(
            ILoggerFactory loggerFactory,
            IElementFactory cdtrElementFactory,
            IJsBuilder jsBuilder,
            IOptions<WebClientSettings> options,
            IBrowserManager browserManager)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<CdtrChromeClient>();
            _cdtrElementFactory = cdtrElementFactory;
            _jsBuilder = jsBuilder;
            _browserManager = browserManager;

            var settings = options.Value;

            _browserManager.LaunchAsync().Wait();
            _chromeProcess = _browserManager.BrowserProcess;
            
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            _hasDisposalStarted = false;

            NavigateAsync(
                settings.Homepage ?? throw new ArgumentNullException(nameof(settings.Homepage)),
                settings.DefaultPageLoadPollSettings ?? throw new ArgumentNullException(nameof(settings.DefaultPageLoadPollSettings)),
                CancellationToken.None)
                .Wait();

            _logger.LogDebug("Exiting {this} constructor.", nameof(CdtrChromeClient));

        }
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

        public async Task NavigateAsync(string address, PollSettings pollSettings, CancellationToken cancellationToken)
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
                        _logger.LogInformation("{name} waiting for new iFrames timed out. Some may have not been attached. Consider relaxing the {timeout} parameter.", nameof(NavigateAsync), nameof(PollSettings.TimeoutMs));
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

                _logger.LogDebug("{name} removing delegate from execution list.", nameof(NavigateAsync));
                _logger.LogDebug("Exiting {name} in {elapsed}", nameof(NavigateAsync), loadStopwatch.Elapsed);

                Delegate.Remove(_frameStoppedLoading, frameStoppedLoading);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to {name}: {e}", nameof(NavigateAsync), e);
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

        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/9bde4870-1599-4958-9ab4-902fa98ba53a/how-do-i-maximizeminimize-applications-programmatically-in-c?forum=csharpgeneral
        //public Task EnterFullScreenAsync()
        //{
        //    throw new NotImplementedException();
        //}

        private static void AddFrames(List<Frame> frames, FrameTree frameTree)
        {
            if (frameTree?.Frame == null)
            {
                return;
            }

            frames.Add(frameTree.Frame);

            if (frameTree?.ChildFrames != null && frameTree.ChildFrames.Any())
            {
                foreach (var child in frameTree.ChildFrames)
                {
                    AddFrames(frames, child);
                }
            }
        }

        public async Task<IReadOnlyCollection<IContext>> GetContextsAsync()
        {
            var frameTree = (await _chromeSession.Page.GetFrameTree(throwExceptionIfResponseNotReceived: false)).FrameTree;

            var frames = new List<Frame>();

            AddFrames(frames, frameTree);

            List<IContext> contexts = new();

            foreach (var frame in frames)
            {
                var frameContexts = _executionContexts.Where(x => ((JObject)x.Value.AuxData)["frameId"]?.ToString() == frame.Id).ToList();
                if (frameContexts.Count == 0)
                {
                    _logger.LogError("{name} failed to find context by frameId {frameId}.", nameof(GetContextsAsync), frame.Id);
                }
                else if (frameContexts.Count == 1)
                {
                    var frameContext = frameContexts[0];
                    if (frameContext.Value == null)
                    {
                        _logger.LogError("{name} error: Context with frameId {frameId} was null.", nameof(GetContextsAsync), frame.Id);
                    }
                    else
                    {
                        if (_cdtrContextFactory.TryCreateContext(frameContext.Key, frame, out IContext? context))
                        {
                            contexts.Add(context ?? throw new ApplicationException($"{nameof(CdtrContextFactory.TryCreateContext)} returned true, {nameof(context)} cannot be null."));
                        }
                        else
                        {
                            _logger.LogError("{name} error: Failed to create context with frameId {frameId}.", nameof(GetContextsAsync), frame.Id);
                        }
                    }
                }
                else
                {
                    var maxId = frameContexts.Max(x => x.Key);

                    _logger.LogWarning(
                        "{name} found {count} contexts with frameId {frameId}. Creating context object for the highest context id: {maxId}.",
                        nameof(GetContextsAsync), frameContexts.Count, frame.Id, maxId);

                    if (_cdtrContextFactory.TryCreateContext(maxId, frame, out IContext? context))
                    {
                        contexts.Add(context ?? throw new ApplicationException($"{nameof(CdtrContextFactory.TryCreateContext)} returned true, {nameof(context)} cannot be null."));
                    }
                    else
                    {
                        _logger.LogError("{name} error: Failed to create context with frameId {frameId}.", nameof(GetContextsAsync), frame.Id);
                    }
                }
            }

            return contexts;
        }

        public async Task<JToken> ExecuteCdpCommandAsync(string commandName, JToken commandParams)
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

        public Task WaitUntilFramesLoadedAsync(PollSettings pollSettings, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

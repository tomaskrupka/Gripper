using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BaristaLabs.ChromeDevTools.Runtime;
using Page = BaristaLabs.ChromeDevTools.Runtime.Page;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using BaristaLabs.ChromeDevTools.Runtime.Page;
using Microsoft.Extensions.Options;
using BaristaLabs.ChromeDevTools.Runtime.Runtime;

namespace Gripper.WebClient.Cdtr
{
    internal class CdtrChromeClient : IWebClient
    {
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IElementFactory _ElementFactory;
        private readonly IContextFactory _ContextFactory;
        private readonly IJsBuilder _jsBuilder;

        private readonly IBrowserManager _browserManager;
        private readonly ICdpAdapter _cdpAdapter;
        private readonly ChromeSession _chromeSession;
        private readonly Process _chromeProcess;

        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _cancellationToken;

        private readonly ConcurrentDictionary<string, byte> _loadedFramesIds;

        private void HandleWebClientEvent(object? sender, RdpEventArgs e)
        {

            if (e is Events.Page_FrameStoppedLoadingEventArgs fslEvent)
            {
                _loadedFramesIds[fslEvent.FrameId] = 0; // using concurrent dictionary as hashset.

                // TODO: in rare cases this is a memory leak. Figure out when to remove the delegate.
            }
        }

        internal CdtrChromeClient(
            ILoggerFactory loggerFactory,
            IElementFactory cdtrElementFactory,
            IJsBuilder jsBuilder,
            IBrowserManager browserManager,
            ICdpAdapter cdpAdapter,
            IContextFactory contextFactory,
            IOptions<WebClientSettings> options)
        {
            var settings = options.Value;

            #region Sanitize settings

            var browserLaunchTimeoutMs =
                settings.BrowserLaunchTimeoutMs ??
                throw new ArgumentNullException("Please set the {name} parameter.", nameof(WebClientSettings.BrowserLaunchTimeoutMs));

            var homepage =
                settings.Homepage ??
                throw new ArgumentNullException("Please set the {name} parameter.", nameof(WebClientSettings.Homepage));

            var defaultPageLoadSettings =
                settings.DefaultPageLoadPollSettings ??
                throw new ArgumentNullException("Please set the {name} parameter.", nameof(WebClientSettings.DefaultPageLoadPollSettings));

            #endregion

            var startupCts = new CancellationTokenSource(browserLaunchTimeoutMs);

            #region Populate members

            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<CdtrChromeClient>();
            _ElementFactory = cdtrElementFactory;
            _jsBuilder = jsBuilder;
            _browserManager = browserManager;
            _cdpAdapter = cdpAdapter;
            _ContextFactory = contextFactory;

            _chromeProcess = _browserManager.BrowserProcess;
            _chromeSession = _cdpAdapter.GetChromeSessionAsync().Result;
            _loadedFramesIds = new ConcurrentDictionary<string, byte>();

            #endregion

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            // Tunnelling CDP events for external subscribers.
            // _cdpAdapter.WebClientEvent += WebClientEvent here would just copy current fifo and ignore future subscriptions.
            _cdpAdapter.WebClientEvent += (s, e) => WebClientEvent?.Invoke(s, e);

            // Private subscribers.
            _cdpAdapter.WebClientEvent += HandleWebClientEvent;

            NavigateAsync(homepage, defaultPageLoadSettings, startupCts.Token).Wait();

            _logger.LogDebug("Exiting {this} constructor.", nameof(CdtrChromeClient));

        }

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
                var navigateCommandResponse = await _chromeSession.Page.Navigate(new Page.NavigateCommand
                {
                    Url = address
                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: _cancellationToken);

                await WaitUntilFramesLoadedAsync(pollSettings, cancellationToken);

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

        public async Task<IReadOnlyCollection<IContext>> GetContextsAsync()
        {
            static void AddFrames(List<Frame> frames, FrameTree frameTree)
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

            var frameTree = (await _chromeSession.Page.GetFrameTree(throwExceptionIfResponseNotReceived: false)).FrameTree;
            var frames = new List<Frame>();

            AddFrames(frames, frameTree);

            List<IContext> contexts = new();

            foreach (var frame in frames)
            {
                var frameInfo = new CdtrFrameInfo(frame);
                var context = await _ContextFactory.CreateContextAsync(frameInfo);

                if (context != null)
                {
                    contexts.Add(context);
                }
                else
                {
                    _logger.LogWarning("{name} returned null context.", nameof(_ContextFactory));
                }
            }

            return contexts;
        }

        public async Task<JToken> ExecuteCdpCommandAsync(string commandName, JToken commandParams)
        {
            try
            {
                var resultToken = await Task.Run(() => _chromeSession.SendCommand(
                    commandName,
                    commandParams,
                    throwExceptionIfResponseNotReceived: false,
                    cancellationToken: _cancellationToken));
                return resultToken;
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to execute rdp command: {e}", e);
                throw;
            }
        }

        public async Task WaitUntilFramesLoadedAsync(PollSettings pollSettings, CancellationToken cancellationToken)
        {
            if (pollSettings == PollSettings.PassThrough)
            {
                _logger.LogDebug("{name} recieved {settingsName} poll settings. Fast-tracking.", nameof(WaitUntilFramesLoadedAsync), nameof(PollSettings.PassThrough));
                return;
            }

            var domBuildStopwatch = Stopwatch.StartNew();

            bool FramesLoaded(FrameTree frameTree)
            {
                // empty or unloaded
                if (frameTree?.Frame?.Id == null || !_loadedFramesIds.ContainsKey(frameTree.Frame.Id))
                {
                    return false;
                }

                // leaf or loaded children
                return frameTree.ChildFrames == null || frameTree.ChildFrames.All(x => FramesLoaded(x));
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                var timedOut = domBuildStopwatch.ElapsedMilliseconds > pollSettings.TimeoutMs;

                if (timedOut)
                {
                    _logger.LogInformation("{name} waiting for new iFrames timed out. Some may have not been attached. Consider relaxing the {timeout} parameter.", nameof(NavigateAsync), nameof(PollSettings.TimeoutMs));
                    break;
                }

                await Task.Delay(pollSettings.PeriodMs, cancellationToken);

                var frameTreeResult = await _chromeSession.Page.GetFrameTree(throwExceptionIfResponseNotReceived: false);

                var framesCount = _loadedFramesIds.Count;

                var framesLoaded = FramesLoaded(frameTreeResult.FrameTree);

                _logger.LogDebug("Loaded frames ids: {framesCount}. All loaded: {frameTreeResult}", framesCount, framesLoaded);

                // Wait for one more poll period and check one last time. That's to catch fresh iFrames that might have been attached by a background worker thread.

                if (framesLoaded)
                {
                    await Task.Delay(pollSettings.PeriodMs, cancellationToken);

                    var verificationFrameTreeResult = await _chromeSession.Page.GetFrameTree(throwExceptionIfResponseNotReceived: false);

                    var framesLoadedVerification = FramesLoaded(verificationFrameTreeResult.FrameTree);

                    _logger.LogDebug("Verification: Loaded frames ids: {framesCount}. All loaded: {frameTreeResult}", _loadedFramesIds.Count, framesLoadedVerification);

                    if (framesLoadedVerification && _loadedFramesIds.Count == framesCount)
                    {
                        break;
                    }
                }
            }

            _logger.LogDebug("Exiting {name} in {elapsed}", nameof(NavigateAsync), domBuildStopwatch.Elapsed);

        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }

    }
}

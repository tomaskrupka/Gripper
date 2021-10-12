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
using BaristaLabs.ChromeDevTools.Runtime.Browser;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using BaristaLabs.ChromeDevTools.Runtime.DOM;

namespace WebScrapingServices.Authenticated.Browser.BaristaLabsCdtr
{
    public class CdtrChromeClient : IWebClient
    {
        private ChromeSession _chromeSession;

        private ILogger _logger;
        private ILoggerFactory _loggerFactory;
        private ICdtrElementFactory _cdtrElementFactory;

        private CookieContainer _cookies;
        private Process _chromeProcess;

        public CookieContainer Cookies => _cookies;

        //private AutoResetEvent _pageLoaded;

        private async Task<Node> GetDocumentNodeAsync()
        {
            var getDocumentResult = await _chromeSession.DOM.GetDocument(new GetDocumentCommand
            {
                Depth = 1
            },
            throwExceptionIfResponseNotReceived: false);

            return getDocumentResult.Root;
        }

        private async Task SubscribeToRdpEventsAsync()
        {
            await _chromeSession.Network.Enable(new BaristaLabs.ChromeDevTools.Runtime.Network.EnableCommand
            {

            }
            , throwExceptionIfResponseNotReceived: false);

            _chromeSession.Network.SubscribeToRequestWillBeSentEvent(x =>
            {
                WebClientEvent?.Invoke(this, new Network_RequestWillBeSentEventArgs(x.RequestId, x.Request.Headers, x.Request.Method, x.Request.Url));
            });

            await _chromeSession.Page.Enable(throwExceptionIfResponseNotReceived: false);

            //_chromeSession.Network.SubscribeToLoadingFinishedEvent(x =>
            //{
            //    _logger.LogDebug("Page loaded for request id: {requestId}", x.RequestId);
            //    _pageLoaded.Set();
            //});
        }

        private async Task AttachFramesAsync(TargetAttachment targetAttachment)
        {
            switch (targetAttachment)
            {
                case TargetAttachment.Default:
                case TargetAttachment.Auto:
                    await _chromeSession.Target.SetAutoAttach(new BaristaLabs.ChromeDevTools.Runtime.Target.SetAutoAttachCommand
                    {
                        AutoAttach = true,
                        WaitForDebuggerOnStart = true,
                        Flatten = true
                    }, 
                    throwExceptionIfResponseNotReceived: false);
                    break;

                case TargetAttachment.SeekAndAttach:
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
                var targets = await _chromeSession.Target.GetTargets(new BaristaLabs.ChromeDevTools.Runtime.Target.GetTargetsCommand
                {

                },
                throwExceptionIfResponseNotReceived: false);

                _logger.LogDebug("Found targets: {targetsCount} of which attached: {attachedTargetsCount}", targets.TargetInfos.Length, targets.TargetInfos.Count(x => x.Attached));

                foreach (var target in targets.TargetInfos.Where(x => !x.Attached))
                {
                    _logger.LogDebug("Attaching to target: {targetId}", target.TargetId);
                    await _chromeSession.Target.AttachToTarget(new BaristaLabs.ChromeDevTools.Runtime.Target.AttachToTargetCommand
                    {
                        TargetId = target.TargetId
                    },
                    throwExceptionIfResponseNotReceived: false);
                }
            }
        }
        private async Task<(Process, ChromeSession)> LaunchAndConnectAsync(WebClientSettings settings)
        {
            var browserArgs = new StringBuilder()
                .Append("--remote-debugging-port=").Append(settings.RemoteDebuggingPort)
                .Append(@" --user-data-dir=C:\CdtrProfiles\").Append(settings.UserProfileName).Append('\\');

            switch (settings.TargetAttachment)
            {
                case TargetAttachment.Default:
                case TargetAttachment.Auto:
                    browserArgs.Append(" --disable-features=IsolateOrigins,site-per-process");
                    break;

                case TargetAttachment.SeekAndAttach:
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

            var chromeProcess = Process.Start(settings.BrowserLocation, browserArgs.ToString());

            using var httpClient = new HttpClient();
            var remoteSessions = await httpClient.GetAsync($"http://localhost:{settings.RemoteDebuggingPort}/json");

            _logger.LogDebug("sessions response: {status}", remoteSessions.StatusCode);

            var remoteSessionsContent = await remoteSessions.Content.ReadAsStringAsync();

            var sessionInfos = JsonConvert.DeserializeObject<List<ChromeSessionInfo>>(remoteSessionsContent);

            _logger.LogDebug("Remote session infos: {remoteSessions}", remoteSessionsContent);

            var chromeSession = new ChromeSession(_loggerFactory.CreateLogger<ChromeSession>(), sessionInfos.First(x => x.Type == "page").WebSocketDebuggerUrl);

            //Navigate to homepage.
            var navigateResult = await chromeSession.Page.Navigate(new Page.NavigateCommand
            {
                Url = settings.Homepage
            });

            _logger.LogDebug("ChromeSession launched and connected to Chrome RDP server");

            return (chromeProcess, chromeSession);
        }


        internal CdtrChromeClient(ILoggerFactory loggerFactory, ICdtrElementFactory cdtrElementFactory, WebClientSettings settings)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<CdtrChromeClient>();
            _cdtrElementFactory = cdtrElementFactory;

            (_chromeProcess, _chromeSession) = LaunchAndConnectAsync(settings).Result;

            _cookies = new CookieContainer();

            SubscribeToRdpEventsAsync();
            AttachFramesAsync(settings.TargetAttachment);

            if (Debugger.IsAttached)
            {
                Task.Run(LoopMonitorWebSockets);
                Task.Run(KeyboardListener);
            }
            _logger.LogDebug("Exiting {this} constructor.", nameof(CdtrChromeClient));
        }


        public event EventHandler<RdpEventArgs>? WebClientEvent;

        public async Task<string> ExecuteScriptAsync(string script)
        {
            try
            {
                var result = await _chromeSession.Runtime.Evaluate(new Runtime.EvaluateCommand
                {
                    Expression = script
                },
                throwExceptionIfResponseNotReceived: false);

                return result?.Result?.Description ?? "null";
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to execute script: {e}", e);
                throw;

            }
        }

        public async Task<IElement?> FindElementByCssSelectorAsync(string cssSelector)
        {
            try
            {

                var documentNode = await GetDocumentNodeAsync();

                if (documentNode == null)
                {
                    return null;
                }

                //_logger.LogDebug("Resolved documentNode id: {documentNodeId}", documentNode.NodeId);

                if (documentNode.NodeId == 0)
                {
                    return null;
                }

                var querySelectorResult = await _chromeSession.DOM.QuerySelector(new QuerySelectorCommand
                {
                    Selector = cssSelector,
                    NodeId = documentNode.NodeId
                }, throwExceptionIfResponseNotReceived: false);

                //_logger.LogDebug("Resolved node id: {nodeId}", querySelectorResult.NodeId);

                if (querySelectorResult.NodeId == 0)
                {
                    _logger.LogWarning("Node id resolved as 0: {cssSelector}", cssSelector);
                    return null;
                }

                return _cdtrElementFactory.CreateCdtrElement(querySelectorResult.NodeId, _chromeSession);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to {name}: {e}.", nameof(FindElementByCssSelectorAsync), e);
                throw;
            }
        }

        public async Task<IElement?> WaitUntilElementPresentAsync(string cssSelector, CancellationToken cancellationToken, PollSettings pollSettings)
        {
            var stopwatch = Stopwatch.StartNew();

            while (!cancellationToken.IsCancellationRequested && stopwatch.ElapsedMilliseconds < pollSettings.TimeoutMs)
            {
                var element = await FindElementByCssSelectorAsync(cssSelector);
                if (element != null)
                {
                    return element;
                }
                else
                {
                    await Task.Delay(pollSettings.PeriodMs);
                }
            }

            return null;
        }

        public async Task<CookieContainer> GetAllCookiesAsync()
        {
            try
            {
                var rawCookies = await _chromeSession.Network.GetAllCookies(new BaristaLabs.ChromeDevTools.Runtime.Network.GetAllCookiesCommand { }, throwExceptionIfResponseNotReceived: false);

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
        private async Task<CookieContainer> TestSession(CancellationToken token)
        {
            try
            {
                var rawCookies = await _chromeSession.Network.GetAllCookies(new BaristaLabs.ChromeDevTools.Runtime.Network.GetAllCookiesCommand { }, throwExceptionIfResponseNotReceived: false);

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

        public async Task<string> GetCurrentUrlAsync()
        {
            try
            {

                var navigationHistory = await _chromeSession.Page.GetNavigationHistory(new Page.GetNavigationHistoryCommand
                {

                }, 
                throwExceptionIfResponseNotReceived: false);

                var currentEntry = navigationHistory.Entries[navigationHistory.CurrentIndex];
                return currentEntry.Url;
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to get current Url: {e}", e);
                throw;
            }
        }

        public async Task GoToUrlAsync(string address)
        {
            try
            {

                await _chromeSession.Page.Navigate(new Page.NavigateCommand
                {
                    Url = address
                },
                throwExceptionIfResponseNotReceived: false);
                //await Task.Run(() => _pageLoaded.WaitOne());
            }
            catch (Exception e)
            {

                _logger.LogError("Failed to go to url: {e}", e);
                throw;
            }
        }

        public async Task ReloadAsync()
        {
            try
            {
                await _chromeSession.Page.Reload(new Page.ReloadCommand { }, throwExceptionIfResponseNotReceived: false);
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

        public async Task<IRdpCommandResult> ExecuteRdpCommandAsync(string commandName)
        {
            try
            {
                var resultToken = await Task.Run(() => _chromeSession.SendCommand(commandName, JToken.Parse("{}"), throwExceptionIfResponseNotReceived: false));
                return new CdtrRdpCommandResult(resultToken);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to execute rdp command: {e}", e);
                throw;
            }
        }

        public async Task<JToken> ExecuteRdpCommandAsync(string commandName, JToken commandParams)
        {
            try
            {
                var resultToken = await Task.Run(() => _chromeSession.SendCommand(commandName, commandParams, throwExceptionIfResponseNotReceived: false));
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
            _logger.LogWarning("Disposing {this}.", nameof(CdtrChromeClient));

            try
            {
                _chromeProcess.Close(); 
            }
            catch (ObjectDisposedException)
            {
                _logger.LogDebug("ObjectDisposedException when disposing {object} at {this}.", nameof(_chromeProcess), nameof(CdtrChromeClient));
            }
            catch (Exception e)
            {
                _logger.LogError("Error disposing {this}: {e}", nameof(_chromeProcess), e);
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

            //GC.SuppressFinalize(this);
        }



        private async Task LoopMonitorWebSockets()
        {
            using var httpClient = new HttpClient();

            while (true)
            {
                await Task.Delay(500);

                var remoteSessions = await httpClient.GetStringAsync($"http://localhost:9223/json");
                var sessionInfos = JsonConvert.DeserializeObject<List<ChromeSessionInfo>>(remoteSessions);

                bool clientAlive = true;

                try
                {
                    _cookies = await this.GetAllCookiesAsync();
                }
                catch (Exception)
                {
                    clientAlive = false;
                }

                _logger.LogDebug("Alive: {clientAlive}, sockets: {sockets}", clientAlive, string.Join(" | ", sessionInfos.Select(x => x?.WebSocketDebuggerUrl?.Replace("ws://localhost:9223/devtools/page/", ""))));
            }
        }
        private async Task KeyboardListener()
        {
            _logger.LogWarning("{name} triggered a keyboard command listener. You can try to kill it by typing 'q'.", nameof(CdtrChromeClient));
            while (true)
            {
                var key = Console.ReadKey();
                if (key.KeyChar == 'd')
                {
                    await ExecuteRdpCommandAsync("Network.disable"); ;
                }
                if (key.KeyChar == 'e')
                {
                    await ExecuteRdpCommandAsync("Network.enable");
                }
                if (key.KeyChar == 'q')
                {
                    break;
                }
            }
            _logger.LogWarning("Keyboard command listener exited.");
        }

    }

}

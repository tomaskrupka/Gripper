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
        private CdtrRdpSession _rdpSession;
        private CdtrChromeBrowserWindow _browserWindow;
        private ChromeSession _chromeSession;

        private ILogger _logger;
        private ILoggerFactory _loggerFactory;
        private ICdtrElementFactory _cdtrElementFactory;

        //private IJsBuilder _jsBuilder;

        private async Task<Node> GetDocumentNodeAsync()
        {
            var getDocumentResult = await _chromeSession.DOM.GetDocument(new GetDocumentCommand
            {
                Depth = 1
            });

            return getDocumentResult.Root;
        }

        public IRdpSession RdpClient => _rdpSession;
        public IBrowserWindow BrowserWindow => _browserWindow;

        public CookieContainer Cookies => throw new NotImplementedException();

        public event EventHandler WebClientEvent;

        internal CdtrChromeClient(ILoggerFactory loggerFactory, ICdtrElementFactory cdtrElementFactory, WebClientSettings settings)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<CdtrChromeClient>();
            _cdtrElementFactory = cdtrElementFactory;
            (_chromeSession, _browserWindow, _rdpSession) = LaunchAndConnectAsync(settings).Result;
        }

        private async Task<(ChromeSession, CdtrChromeBrowserWindow, CdtrRdpSession)> LaunchAndConnectAsync(WebClientSettings settings)
        {
            // see https://github.com/BaristaLabs/chrome-dev-tools-runtime/blob/master/ChromeDevToolsCLI/Program.cs

            var browserArgs = new StringBuilder()
                .Append("--remote-debugging-port=").Append(settings.RemoteDebuggingPort)
                .Append(@" --user-data-dir=C:\CdtrProfiles\").Append(settings.UserProfileName).Append('\\');

            if (settings.UseProxy)
            {
                browserArgs.Append(" --proxy-server=").Append(settings.Proxy?.Address ?? throw new ApplicationException("Null proxy when UseProxy flag was up."));
                if (browserArgs[^1] == '/')
                {
                    browserArgs.Remove(browserArgs.Length - 1, 1);
                }
            }

            Process.Start(settings.BrowserLocation, browserArgs.ToString());

            using var httpClient = new HttpClient();
            var remoteSessions = await httpClient.GetStringAsync($"http://localhost:{settings.RemoteDebuggingPort}/json");
            var sessionInfos = JsonConvert.DeserializeObject<List<ChromeSessionInfo>>(remoteSessions);

            var chromeSession = new ChromeSession(sessionInfos.First(x => x.Type == "page").WebSocketDebuggerUrl);

            long executionContextId = -1;
            var s = new SemaphoreSlim(0, 1);

            //Navigate to homepage.
            var navigateResult = await chromeSession.Page.Navigate(new Page.NavigateCommand
            {
                Url = "https://www.google.com"
            });

            //Find execution context id.
            chromeSession.Runtime.SubscribeToExecutionContextCreatedEvent((e) =>
            {
                var auxData = e.Context.AuxData as JObject;
                var frameId = auxData?["frameId"]?.Value<string>();

                if (frameId == navigateResult.FrameId)
                {
                    executionContextId = e.Context.Id;
                    s.Release();
                }
            });

            var enableRuntimeResult = await chromeSession.Runtime.Enable(new Runtime.EnableCommand());

            await s.WaitAsync();

            var rdpSession = new CdtrRdpSession(chromeSession);

            var browserLogger = _loggerFactory.CreateLogger<CdtrChromeBrowserWindow>();
            var browserWindow = new CdtrChromeBrowserWindow(browserLogger, chromeSession);

            return (chromeSession, browserWindow, rdpSession);
        }

        public void Dispose()
        {
            try
            {
                _rdpSession.Dispose();
            }
            catch (ObjectDisposedException)
            {
                _logger.LogDebug("ObjectDisposedException when disposing {object} at {this}.", nameof(_rdpSession), nameof(CdtrChromeClient));
            }

            try
            {
                _browserWindow.Dispose();
            }
            catch (ObjectDisposedException)
            {
                _logger.LogDebug("ObjectDisposedException when disposing {object} at {this}.", nameof(_browserWindow), nameof(CdtrChromeClient));
            }

            try
            {
                _chromeSession.Dispose();
            }
            catch (ObjectDisposedException)
            {
                _logger.LogDebug("ObjectDisposedException when disposing {object} at {this}.", nameof(_chromeSession), nameof(CdtrChromeClient));
            }
        }

        public async Task<string> ExecuteScriptAsync(string script)
        {
            var result = await _chromeSession.Runtime.Evaluate(new Runtime.EvaluateCommand
            {
                Expression = script
            });

            return result.Result.Description;
        }

        public async Task<IElement?> FindElementByCssSelectorAsync(string cssSelector)
        {
            var documentNode = await GetDocumentNodeAsync();

            _logger.LogDebug("Resolved documentNode id: {documentNodeId}", documentNode.NodeId);

            if (documentNode.NodeId == 0)
            {
                return null;
            }

            var querySelectorResult = await _chromeSession.DOM.QuerySelector(new QuerySelectorCommand
            {
                Selector = cssSelector,
                NodeId = documentNode.NodeId
            });

            _logger.LogDebug("Resolved node id: {nodeId}", querySelectorResult.NodeId);

            if (querySelectorResult.NodeId == 0)
            {
                _logger.LogWarning("Node id resolved as 0: {cssSelector}", cssSelector);
                return null;
            }

            return _cdtrElementFactory.CreateCdtrElement(querySelectorResult.NodeId, _chromeSession);
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
    }

}

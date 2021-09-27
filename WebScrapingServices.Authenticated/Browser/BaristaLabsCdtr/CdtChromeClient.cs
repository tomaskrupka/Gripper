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

namespace WebScrapingServices.Authenticated.Browser.BaristaLabsCDTR
{
    public class CdtrChromeClient : IWebClient
    {
        private ChromeSession _chromeSession;
        private long _executionContextId;

        public IRdpSession RdpClient => throw new NotImplementedException();

        public IBrowserWindow BrowserWindow => throw new NotImplementedException();

        public CookieContainer Cookies => throw new NotImplementedException();

        public event EventHandler WebClientEvent;

        public CdtrChromeClient(WebClientSettings settings)
        {
            (_chromeSession, _executionContextId) = LaunchAndConnectAsync(settings).Result;
        }

        private async Task<(ChromeSession, long)> LaunchAndConnectAsync(WebClientSettings settings)
        {
            // see https://github.com/BaristaLabs/chrome-dev-tools-runtime/blob/master/ChromeDevToolsCLI/Program.cs

            using var httpClient = new HttpClient();
            var remoteSessions = await httpClient.GetStringAsync("http://localhost:9223/json");
            var sessionInfos = JsonConvert.DeserializeObject<List<ChromeSessionInfo>>(remoteSessions);

            var chromeSession = new ChromeSession(sessionInfos.First(x => x.Type == "page").WebSocketDebuggerUrl);

            long executionContextId = -1;
            var s = new SemaphoreSlim(0, 1);

            //Navigate to homepage.
            var navigateResult = await chromeSession.Page.Navigate(new Page.NavigateCommand
            {
                Url = "https://www.google.com"
            });

            var enableRuntimeResult = await _chromeSession.Runtime.Enable(new Runtime.EnableCommand());

            //Find execution context id.
            _chromeSession.Runtime.SubscribeToExecutionContextCreatedEvent((e) =>
            {
                var auxData = e.Context.AuxData as JObject;
                var frameId = auxData?["frameId"]?.Value<string>();

                if (e.Context.Origin == "https://www.amazon.com" && frameId == navigateResult.FrameId)
                {
                    executionContextId = e.Context.Id;
                    s.Release();
                }
            });

            await s.WaitAsync();

            return (_chromeSession, executionContextId);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<string> ExecuteScriptAsync(string script)
        {
            //Evaluate a complex answer.
            var result = await _chromeSession.Runtime.Evaluate(new Runtime.EvaluateCommand
            {
                //ContextId = _executionContextId,
                //ObjectGroup = "test123",
                Expression = script
            });
            throw new NotImplementedException();
        }

        public Task<IElement?> FindElementByCssSelectorAsync(string cssSelector)
        {
            throw new NotImplementedException();
        }

        public Task<IElement?> WaitUntilElementPresentAsync(string cssSelector, CancellationToken cancellationToken, PollSettings settings)
        {
            throw new NotImplementedException();
        }
    }
    public class CdtRdpSession : IRdpSession
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IRdpCommandResult> ExecuteRdpCommandAsync(string commandName)
        {
            throw new NotImplementedException();
        }

        public Task<IRdpCommandResult> ExecuteRdpCommandAsync(string commandName, WebClientCommandParam commandParam)
        {
            throw new NotImplementedException();
        }

        public Task<JToken> ExecuteRdpCommandAsync(string commandName, JToken commandParams)
        {
            throw new NotImplementedException();
        }

        public Task<CookieCollection> GetCookies()
        {
            throw new NotImplementedException();
        }
    }

    public class CdtChromeBrowserWindow : IBrowserWindow
    {
        public string Url => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task EnterFullScreenAsync()
        {
            throw new NotImplementedException();
        }

        public Task GoToUrlAsync(string address)
        {
            throw new NotImplementedException();
        }

        public Task ReloadAsync()
        {
            throw new NotImplementedException();
        }
    }
}

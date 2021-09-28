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

namespace WebScrapingServices.Authenticated.Browser.BaristaLabsCdtr
{
    public class CdtrChromeClient : IWebClient
    {
        private ChromeSession _chromeSession;
        private CdtrRdpSession _rdpSession;

        public IRdpSession RdpClient => _rdpSession;

        public IBrowserWindow BrowserWindow => throw new NotImplementedException();

        public CookieContainer Cookies => throw new NotImplementedException();

        public event EventHandler WebClientEvent;

        public CdtrChromeClient(WebClientSettings settings)
        {
            (_chromeSession, _rdpSession) = LaunchAndConnectAsync(settings).Result;
        }

        private async Task<(ChromeSession, CdtrRdpSession)> LaunchAndConnectAsync(WebClientSettings settings)
        {
            // see https://github.com/BaristaLabs/chrome-dev-tools-runtime/blob/master/ChromeDevToolsCLI/Program.cs

            // "user-data-dir=C:\SeleniumProfiles\" + config.UserName + "\\"

            var browserArgs = new StringBuilder()
                .Append("--remote-debugging-port=").Append(settings.RemoteDebuggingPort)
                .Append(@" --user-data-dir=C:\CdtrProfiles\").Append(settings.UserProfileName).Append('\\');

            Process.Start(settings.BrowserLocation, browserArgs.ToString());

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
            var browserWindow = new CdtrChromeBrowserWindow(chromeSession);

            return (chromeSession, rdpSession);
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
    public class CdtrRdpSession : IRdpSession
    {
        private ChromeSession _chromeSession;
        public CdtrRdpSession(ChromeSession chromeSession)
        {
            _chromeSession = chromeSession;
        }
        public void Dispose()
        {
            _chromeSession.Dispose();
        }

        public async Task<JToken> ExecuteRdpCommandAsync(string commandName, JToken commandParams)
        {
            var resultToken = await Task.Run(() => _chromeSession.SendCommand(commandName, commandParams));
            return resultToken;
        }

        public async Task<IRdpCommandResult> ExecuteRdpCommandAsync(string commandName)
        {
            var resultToken = await Task.Run(() => _chromeSession.SendCommand(commandName, JToken.Parse("{}")));
            return new CdtrRdpCommandResult(resultToken);
        }

        public Task<CookieCollection> GetCookies()
        {
            throw new NotImplementedException();
        }
    }

    public class CdtrRdpCommandResult : IRdpCommandResult
    {
        public CdtrRdpCommandResult(JToken resultToken)
        {
            throw new NotImplementedException();
        }
        public string Message => throw new NotImplementedException();
    }

    public class CdtrChromeBrowserWindow : IBrowserWindow
    {
        public string Url => throw new NotImplementedException();

        public CdtrChromeBrowserWindow(ChromeSession chromeSession)
        {
            throw new NotImplementedException();
        }

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

using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;
using BaristaLabs.ChromeDevTools.Runtime;

namespace WebScrapingServices.Authenticated.Browser.BaristaLabsCdtr
{
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
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser
{
    public interface IRdpSession : IDisposable
    {
        public Task<IRdpCommandResult> ExecuteRdpCommandAsync(string commandName);
        public Task<IRdpCommandResult> ExecuteRdpCommandAsync(string commandName, WebClientCommandParam commandParam);
        public Task<JToken> ExecuteRdpCommandAsync(string commandName, JToken commandParams);
        /// <summary>
        /// Gets all cookies for current page URL and all of its subframes.
        /// https://chromedevtools.github.io/devtools-protocol/tot/Network/#method-getCookies
        /// </summary>
        /// <returns></returns>
        public Task<CookieCollection> GetCookies();
        public event EventHandler<RdpEventArgs> RdpEvent;
    }
}

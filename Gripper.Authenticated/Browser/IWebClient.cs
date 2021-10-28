using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Authenticated.Browser
{
    public interface IWebClient : IDisposable
    {
        public event EventHandler<RdpEventArgs> WebClientEvent;

        public Task<string> ExecuteScriptAsync(string script);
        public Task<IElement?> FindElementByCssSelectorAsync(string cssSelector);
        public Task<IElement?> WaitUntilElementPresentAsync(string cssSelector, CancellationToken cancellationToken, PollSettings pollSettings);
        public Task<CookieContainer> GetAllCookiesAsync();
        public CookieContainer Cookies { get; }


        public Task<string?> GetCurrentUrlAsync();
        public Task GoToUrlAsync(string address, CancellationToken cancellationToken, PollSettings pollSettings);
        public Task ReloadAsync(CancellationToken cancellationToken, PollSettings pollSettings);
        public Task EnterFullScreenAsync();
        public Task<IRdpCommandResult> ExecuteRdpCommandAsync(string commandName);
        public Task<JToken> ExecuteRdpCommandAsync(string commandName, JToken commandParams);
        /// <summary>
        /// Gets all cookies for current page URL and all of its subframes.
        /// https://chromedevtools.github.io/devtools-protocol/tot/Network/#method-getCookies
        /// </summary>
        /// <returns></returns>
    }
}

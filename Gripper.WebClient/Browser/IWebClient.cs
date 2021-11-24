using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient.Browser
{
    public interface IWebClient : IDisposable
    {
        public IContext MainContext { get; }

        public event EventHandler<RdpEventArgs> WebClientEvent;
        public Task<CookieContainer> GetAllCookiesAsync();
        public Task<IReadOnlyCollection<IContext>> GetContextsAsync();
        public Task<string?> GetCurrentUrlAsync();
        public Task GoToUrlAsync(string address, PollSettings pollSettings, CancellationToken cancellationToken);
        public Task ReloadAsync(PollSettings pollSettings, CancellationToken cancellationToken);
        public Task EnterFullScreenAsync();
        public Task<JToken> ExecuteRdpCommandAsync(string commandName, JToken commandParams);
    }
}

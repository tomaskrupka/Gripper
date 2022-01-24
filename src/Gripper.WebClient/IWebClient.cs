using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient
{
    public interface IWebClient : IDisposable
    {
        public IContext? MainContext { get; }
        public event EventHandler<RdpEventArgs> WebClientEvent;
        public Task<CookieContainer> GetAllCookiesAsync();
        public Task<IReadOnlyCollection<IContext>>? GetContextsAsync();
        public Task<string?> GetCurrentUrlAsync();
        public Task WaitUntilFramesLoadedAsync(PollSettings pollSettings, CancellationToken cancellation);
        public Task NavigateAsync(string address, PollSettings pollSettings, CancellationToken cancellationToken);
        public Task ReloadAsync(PollSettings pollSettings, CancellationToken cancellationToken);
        public Task<JToken> ExecuteRdpCommandAsync(string commandName, JToken commandParams);
    }
}

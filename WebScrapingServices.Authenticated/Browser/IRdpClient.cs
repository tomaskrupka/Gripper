using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser
{
    public interface IRdpSession : IDisposable
    {
        public Task<IRdpCommandResult> ExecuteRdpCommandAsync(string commandName);
        public Task<IRdpCommandResult> ExecuteRdpCommandAsync(string commandName, WebClientCommandParam commandParam);
        public Task<JToken> ExecuteRdpCommandAsync(string commandName, JToken commandParams);
        public event EventHandler<RdpEventArgs> RdpEvent;
    }
}

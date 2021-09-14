using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.DevTools;
using System;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser.Selenium
{
    public class SeleniumRdpSession : IRdpSession
    {
        private ILogger _logger;
        private DevToolsSession _devToolsSession;
        public event EventHandler<RdpEventArgs> RdpEvent;

        public SeleniumRdpSession(ILogger<SeleniumRdpSession> logger, DevToolsSession devToolsSession)
        {
            _logger = logger;
            _devToolsSession = devToolsSession;
            _devToolsSession.DevToolsEventReceived += DevToolsEventReceived;
        }

        public void Dispose()
        {
            _devToolsSession.Dispose();
        }


        private void DevToolsEventReceived(object? sender, DevToolsEventReceivedEventArgs e)
        {
            switch (e.EventName)
            {
                case "Network.requestWillBeSent":
                    Request request = new()
                    {

                    };

                    RdpEvent(sender, new Network_RequestWillBeSentEventArgs(request));
                    break;
                default:
                    RdpEvent(sender, new RdpEventArgs(e.DomainName, e.EventName, e.EventData));
                    break;
            }
        }

        public async Task<IRdpCommandResult> ExecuteRdpCommandAsync(string commandName)
        {
            // Try-catch, log and rethrow to prevent silent fails.
            try
            {
                switch (commandName)
                {
                    case "Network.enable":
                        await _devToolsSession.Domains.Network.EnableNetwork();
                        return SeleniumRdpCommandResult.Success;

                    default:
                        throw new NotImplementedException();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to execute command: {commandName}", commandName);
                throw;
            }
        }

        public Task<IRdpCommandResult> ExecuteRdpCommandAsync(string commandName, WebClientCommandParam commandParam)
        {
            throw new NotImplementedException();
        }

        public async Task<JToken> ExecuteRdpCommandAsync(string commandName, JToken commandParams)
        {
            return await _devToolsSession.SendCommand(commandName, commandParams);
        }
    }
}

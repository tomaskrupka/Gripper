using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser.Selenium
{
    public class SeleniumRdpSession : IRdpSession
    {
        private ILogger _logger;
        private DevToolsSession _devToolsSession;
        internal event EventHandler<RdpEventArgs> RdpEvent;

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

        internal void TriggerKeyboardCommandListener()
        {
            Task.Run(KeyboardListener);
        }

        private async Task KeyboardListener()
        {
            _logger.LogWarning("{} triggered a keyboard command listener. You can try to kill it by typing 'q'.", nameof(SeleniumRdpSession));
            while (true)
            {
                var key = Console.ReadKey();
                if (key.KeyChar == 'd')
                {
                    await ExecuteRdpCommandAsync("Network.disable");;
                }
                if (key.KeyChar == 'e')
                {
                    await ExecuteRdpCommandAsync("Network.enable");
                }
                if (key.KeyChar == 'q')
                {
                    break;
                }
            }
            _logger.LogInformation("Keyboard command listener exited.");
        }

        private void DevToolsEventReceived(object? sender, DevToolsEventReceivedEventArgs e)
        {
            _logger.LogDebug("Event received: {domainName}.{eventName}", e.DomainName, e.EventName);

            switch (e.EventName)
            {
                case "requestWillBeSent":
                    
                    var requestId = e.EventData["requestId"]?.ToString();

                    if (requestId == null)
                    {
                        throw new ApplicationException("requestId cannot be null here.");
                    }

                    var method = e.EventData["request"]?["method"]?.ToString();
                    if (method == null)
                    {
                        throw new ApplicationException("method cannot be null here.");
                    }

                    var headers = new Dictionary<string, string>();

                    var rawHeadersDictionary = e.EventData["request"]?["headers"];
                    if (rawHeadersDictionary != null && rawHeadersDictionary.HasValues)
                    {
                        foreach (JProperty header in rawHeadersDictionary.Children())
                        {
                            if (header.Value.Type == JTokenType.String)
                            {
                                headers.Add(header.Name, header.Value.ToString());
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        }

                    }

                    var url = e.EventData["request"]?["url"]?.ToString();
                    if (url == null)
                    {
                        throw new ApplicationException("request url cannot be null here");
                    }

                    Request request = new(requestId, headers, method, url);

                    RdpEvent?.Invoke(sender, new Network_RequestWillBeSentEventArgs(request));

                    break;

                default:
                    RdpEvent?.Invoke(sender, new RdpEventArgs(e.DomainName, e.EventName, e.EventData));
                    break;
            }
        }

        public async Task<IRdpCommandResult> ExecuteRdpCommandAsync(string commandName)
        {
            // Try-catch, log and rethrow to prevent silent fails.
            try
            {
                switch (commandName.ToLower())
                {
                    case "network.enable":
                        await _devToolsSession.Domains.Network.EnableNetwork();
                        _logger.LogInformation("RDP domain enabled: Network.");
                        return SeleniumRdpCommandResult.Success;

                    case "network.disable":
                        await _devToolsSession.Domains.Network.DisableNetwork();
                        _logger.LogInformation("RDP domain disabled: Network.");
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

        public Task<CookieCollection> GetCookies()
        {
            throw new NotSupportedException($"Selenium does expose cookies directly through the browser window handler. Use your {nameof(IWebClient)} implementation instance.");
        }
    }
}

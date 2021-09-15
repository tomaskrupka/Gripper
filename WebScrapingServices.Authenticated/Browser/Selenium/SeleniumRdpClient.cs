using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

                    var headersDictionary = new Dictionary<string, string>();

                    var rawHeadersDictionary = e.EventData["request"]?["headers"];
                    if (rawHeadersDictionary != null && rawHeadersDictionary.HasValues)
                    {
                        foreach (JProperty header in rawHeadersDictionary.Children())
                        {
                            if (header.Value.Type == JTokenType.String)
                            {
                                headersDictionary.Add(header.Name, header.Value.ToString());
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        }

                    }

                    var headers = new ReadOnlyDictionary<string, string>(headersDictionary);

                    var url = e.EventData["request"]?["url"]?.ToString();
                    if (url == null)
                    {
                        throw new ApplicationException("request url cannot be null here");
                    }

                    Request request = new(requestId, headers, method, url);

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

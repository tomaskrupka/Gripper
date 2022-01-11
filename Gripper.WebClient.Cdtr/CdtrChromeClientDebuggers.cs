using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient.Cdtr
{
    /// <summary>
    /// Private debugging methods.
    /// </summary>
    public partial class CdtrChromeClient
    {
        private async Task<CookieContainer> TestSession(CancellationToken token)
        {
            try
            {
                var rawCookies = await _chromeSession.Network.GetAllCookies(new BaristaLabs.ChromeDevTools.Runtime.Network.GetAllCookiesCommand { }, throwExceptionIfResponseNotReceived: false, cancellationToken: _cancellationToken);

                _logger.LogDebug("raw cookies: {rawCookies}", rawCookies?.Cookies?.Length.ToString() ?? "null");

                var cookieContainer = new CookieContainer();

                if (rawCookies?.Cookies == null)
                {
                    return cookieContainer;
                }

                foreach (var cookie in rawCookies.Cookies)
                {
                    if (cookie == null)
                    {
                        continue;
                    }

                    cookieContainer.Add(new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
                }

                return cookieContainer;
            }
            catch (Exception e)
            {
                _logger.LogError("Exception getting all cookies: {e}", e);
                throw;
            }

        }

        private async Task LoopMonitorWebSockets()
        {
            using var httpClient = new HttpClient();

            while (!_cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(500);

                var remoteSessions = await httpClient.GetStringAsync($"http://localhost:9223/json");
                var sessionInfos = JsonConvert.DeserializeObject<List<ChromeSessionInfo>>(remoteSessions);

                bool clientAlive = true;

                try
                {
                    await GetAllCookiesAsync();
                }
                catch (Exception)
                {
                    clientAlive = false;
                }

                _logger.LogDebug("Alive: {clientAlive}, sockets: {sockets}", clientAlive, string.Join(" | ", sessionInfos.Select(x => x?.WebSocketDebuggerUrl?.Replace("ws://localhost:9223/devtools/page/", ""))));
            }
        }
    }
}

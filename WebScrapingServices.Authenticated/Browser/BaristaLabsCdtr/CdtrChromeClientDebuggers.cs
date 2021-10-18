using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.Authenticated.Browser.BaristaLabsCdtr
{
    /// <summary>
    /// Private debugging methods.
    /// </summary>
    public partial class CdtrChromeClient
    {
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
                    _cookies = await this.GetAllCookiesAsync();
                }
                catch (Exception)
                {
                    clientAlive = false;
                }

                _logger.LogDebug("Alive: {clientAlive}, sockets: {sockets}", clientAlive, string.Join(" | ", sessionInfos.Select(x => x?.WebSocketDebuggerUrl?.Replace("ws://localhost:9223/devtools/page/", ""))));
            }
        }

        private async Task KeyboardListener()
        {
            _logger.LogWarning("{name} triggered a keyboard command listener. You can try to kill it by typing 'q'.", nameof(CdtrChromeClient));

            while (!_cancellationToken.IsCancellationRequested)
            {
                var key = Console.ReadKey();
                if (key.KeyChar == 'd')
                {
                    await ExecuteRdpCommandAsync("Network.disable"); ;
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

            _logger.LogWarning("Keyboard command listener exited.");
        }
    }
}

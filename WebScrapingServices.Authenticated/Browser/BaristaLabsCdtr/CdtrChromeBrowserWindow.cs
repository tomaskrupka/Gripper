using System;
using System.Threading.Tasks;
using BaristaLabs.ChromeDevTools.Runtime;
using Page = BaristaLabs.ChromeDevTools.Runtime.Page;
using Microsoft.Extensions.Logging;

namespace WebScrapingServices.Authenticated.Browser.BaristaLabsCdtr
{
    public class CdtrChromeBrowserWindow : IBrowserWindow
    {
        private ILogger _logger;
        private ChromeSession _chromeSession;
        public string Url => throw new NotImplementedException();

        public CdtrChromeBrowserWindow(ILogger<CdtrChromeBrowserWindow> logger, ChromeSession chromeSession)
        {
            _logger = logger;
            _chromeSession = chromeSession;
        }

        public void Dispose()
        {
            try
            {
                _chromeSession.Dispose();
            }
            catch (ObjectDisposedException)
            {
                _logger.LogDebug("ObjectDisposedException when disposing {chromeSession} at {this}.", nameof(ChromeSession), nameof(CdtrChromeBrowserWindow));
            }
        }

        public Task EnterFullScreenAsync()
        {
            throw new NotImplementedException();
        }

        public async Task GoToUrlAsync(string address)
        {
            await _chromeSession.Page.Navigate(new Page.NavigateCommand
            {
                Url = address
            });
        }

        public async Task ReloadAsync()
        {
            await _chromeSession.Page.Reload(new Page.ReloadCommand
            {

            });
        }
    }
}

using OpenQA.Selenium.Chrome;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser.Selenium
{
    public class SeleniumChromeClient : IWebClient
    {
        private SeleniumRdpSession _rdpSession;
        private SeleniumChromeBrowserWindow _browserWindow;

        private ILogger _logger;

        public SeleniumChromeClient(ILogger<SeleniumChromeClient> logger, WebClientSettings settings)
        {
            _logger = logger;
            (_browserWindow, _rdpSession) = LaunchAndConnect(settings);
        }

        public IRdpSession RdpClient => _rdpSession;

        public IBrowserWindow BrowserWindow => _browserWindow;

        public void Dispose()
        {
            _browserWindow.Dispose();
            _rdpSession.Dispose();
        }

        private (SeleniumChromeBrowserWindow, SeleniumRdpSession) LaunchAndConnect(WebClientSettings settings)
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument($"user-data-dir={Environment.CurrentDirectory}\\SeleniumProfiles\\" + settings.UserProfileName + "\\");

            if (settings.UseProxy)
            {
                throw new NotImplementedException();
            }
            else
            {
                _logger.LogInformation("Launching Selenium Chrome without proxy.");

                var driver = new ChromeDriver(Environment.CurrentDirectory, options);
                var session = driver.GetDevToolsSession();

                var browserWindow = new SeleniumChromeBrowserWindow(driver);
                var rdpSession = new SeleniumRdpSession(session);

                return (browserWindow, rdpSession);
            }
        }
    }
}

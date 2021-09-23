using OpenQA.Selenium.Chrome;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Net;
using System.Collections.Generic;

namespace WebScrapingServices.Authenticated.Browser.Selenium
{
    public class SeleniumChromeClient : IWebClient
    {
        private ChromeDriver _driver;
        private SeleniumRdpSession _rdpSession;
        private SeleniumChromeBrowserWindow _browserWindow;

        private ILoggerFactory _loggerFactory;
        private ILogger _logger;

        public event EventHandler WebClientEvent;
        public SeleniumChromeClient(ILoggerFactory loggerFactory, WebClientSettings settings)
        {
            WebClientEvent += WebClientEventHandler;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<SeleniumChromeClient>();
            (_driver, _browserWindow, _rdpSession) = LaunchAndConnect(settings);
        }

        private void WebClientEventHandler(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public IRdpSession RdpClient => _rdpSession;

        public IBrowserWindow BrowserWindow => _browserWindow;

        public CookieContainer Cookies
        {
            get
            {
                CookieContainer cookieContainer = new();
                foreach (var cookie in _driver.Manage().Cookies.AllCookies)
                {
                    cookieContainer.Add(new System.Net.Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
                }
                return cookieContainer;
            }
        }

        public void Dispose()
        {
            _browserWindow.Dispose();
            _rdpSession.Dispose();
            _driver.Dispose();
        }

        private (ChromeDriver, SeleniumChromeBrowserWindow, SeleniumRdpSession) LaunchAndConnect(WebClientSettings settings)
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            chromeOptions.AddArgument($"user-data-dir={Environment.CurrentDirectory}\\SeleniumProfiles\\" + settings.UserProfileName + "\\");

            if (settings.UseProxy)
            {
                var proxyAuthority = settings.Proxy?.Address?.Authority;
                if (proxyAuthority == null)
                {
                    throw new ArgumentException("Invalid settings provided, proxy authority must not be null if UseProxy set to true.");
                }

                _logger.LogInformation("Launching Selenium Chrome with proxy.");

                chromeOptions.Proxy = new Proxy
                {
                    Kind = ProxyKind.Manual,
                    IsAutoDetect = false,
                    HttpProxy = proxyAuthority,
                    SslProxy = proxyAuthority,
                };
            }
            else
            {
                _logger.LogInformation("Launching Selenium Chrome without proxy.");
            }

            if (settings.IgnoreSslCertificateErrors)
            {
                _logger.LogWarning("Launching Selenium Chrome with ignore-certificate-errors flag on.");
                chromeOptions.AddArgument("ignore-certificate-errors");
            }

            var driver = new ChromeDriver(Environment.CurrentDirectory, chromeOptions);
            var session = driver.GetDevToolsSession();
            var seleniumRdpSessionLogger = _loggerFactory.CreateLogger<SeleniumRdpSession>();

            var browserWindow = new SeleniumChromeBrowserWindow(_loggerFactory.CreateLogger<SeleniumChromeBrowserWindow>(), driver);
            var rdpSession = new SeleniumRdpSession(seleniumRdpSessionLogger, session);

            // Selenium sends some events via the rdp session. Just relay these to the central event broadcast.
            rdpSession.RdpEvent += (sender, eventArgs) => WebClientEvent(sender, eventArgs);

            var driverOptions = driver.Manage();

            driverOptions.Network.NetworkRequestSent += NetworkRequestSent;
            //driverOptions.Network.NetworkResponseReceived += NetworkResponseReceived;

            if (settings.TriggerKeyboardCommandListener)
            {
                rdpSession.TriggerKeyboardCommandListener();
            }

            return (driver, browserWindow, rdpSession);
        }

        private void NetworkResponseReceived(object? sender, NetworkResponseReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NetworkRequestSent(object? sender, NetworkRequestSentEventArgs e)
        {
            var eventArgs = new Network_RequestWillBeSentEventArgs(e.RequestId, e.RequestHeaders, e.RequestMethod, e.RequestUrl);
            WebClientEvent(sender, eventArgs);
        }
    }
}

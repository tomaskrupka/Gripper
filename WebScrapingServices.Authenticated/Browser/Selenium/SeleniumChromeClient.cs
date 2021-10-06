using OpenQA.Selenium.Chrome;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace WebScrapingServices.Authenticated.Browser.Selenium
{
    public class SeleniumChromeClient : IWebClient
    {
        private ChromeDriver _driver;
        private SeleniumRdpSession _rdpSession;
        private SeleniumChromeBrowserWindow _browserWindow;

        private ILoggerFactory _loggerFactory;
        private ILogger _logger;

        public event EventHandler<RdpEventArgs> WebClientEvent;
        public SeleniumChromeClient(ILoggerFactory loggerFactory, WebClientSettings settings)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<SeleniumChromeClient>();
            (_driver, _browserWindow, _rdpSession) = LaunchAndConnect(settings);
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
            _logger.LogInformation("NetworkRequestSent event captured.");
            var eventArgs = new Network_RequestWillBeSentEventArgs(e.RequestId, e.RequestHeaders, e.RequestMethod, e.RequestUrl);
            WebClientEvent(sender, eventArgs);
        }

        public async Task<string> ExecuteScriptAsync(string script)
        {
            try
            {
                var result = _driver.ExecuteScript(script);
                return result?.ToString() ?? "No result.";
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.ToString());
                throw;
            }
        }

        public async Task<IElement?> FindElementByCssSelectorAsync(string cssSelector)
        {
            IWebElement? element;
            try
            {
                element = _driver.FindElement(By.CssSelector(cssSelector));
            }
            catch (Exception e) when (e is NoSuchElementException || (e is AggregateException && e.InnerException is NoSuchElementException))
            {
                return null;
            }

            return new SeleniumWebElement(element);
        }

        public async Task<IElement?> WaitUntilElementPresentAsync(string cssSelector, CancellationToken cancellationToken, PollSettings pollSettings)
        {
            var stopwatch = Stopwatch.StartNew();

            while (!cancellationToken.IsCancellationRequested && stopwatch.ElapsedMilliseconds < pollSettings.TimeoutMs)
            {
                var element = await FindElementByCssSelectorAsync(cssSelector);
                if (element != null)
                {
                    return element;
                }
                else
                {
                    await Task.Delay(pollSettings.PeriodMs);
                }
            }

            return null;
        }
    }
}

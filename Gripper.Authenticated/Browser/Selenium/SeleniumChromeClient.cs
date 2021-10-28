using OpenQA.Selenium.Chrome;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using OpenQA.Selenium.DevTools;
using Newtonsoft.Json.Linq;

namespace Gripper.Authenticated.Browser.Selenium
{
    public class SeleniumChromeClient : IWebClient
    {
        private ChromeDriver _driver;
        private DevToolsSession _devToolsSession;

        private ILoggerFactory _loggerFactory;
        private ILogger _logger;
        private INavigation _navigation;

        public CookieContainer Cookies => GetAllCookiesAsync().Result;

        public event EventHandler<RdpEventArgs> WebClientEvent;
        public SeleniumChromeClient(ILoggerFactory loggerFactory, WebClientSettings settings)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<SeleniumChromeClient>();
            (_driver, _devToolsSession) = LaunchAndConnect(settings);
            _navigation = _driver.Navigate();
        }

        public void Dispose()
        {
            _driver.Dispose();
        }

        private (ChromeDriver, DevToolsSession) LaunchAndConnect(WebClientSettings settings)
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            chromeOptions.AddArgument($"user-data-dir={settings.UserDataDir.FullName}");

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
            var devToolsSession = driver.GetDevToolsSession();


            // Selenium sends some events via the rdp session. Just relay these to the central event broadcast.
            WebClientEvent += (sender, eventArgs) => WebClientEvent(sender, eventArgs);

            var driverOptions = driver.Manage();

            driverOptions.Network.NetworkRequestSent += NetworkRequestSent;
            //driverOptions.Network.NetworkResponseReceived += NetworkResponseReceived;

            if (settings.TriggerKeyboardCommandListener)
            {
                TriggerKeyboardCommandListener();
            }

            return (driver, devToolsSession);
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

        public async Task<CookieContainer> GetAllCookiesAsync()
        {
            CookieContainer cookieContainer = new();
            foreach (var cookie in _driver.Manage().Cookies.AllCookies)
            {
                cookieContainer.Add(new System.Net.Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
            }
            return cookieContainer;
        }
        public Task EnterFullScreenAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetCurrentUrlAsync()
        {
            return _driver.Url;
        }

        public async Task GoToUrlAsync(string address, CancellationToken cancellationToken, PollSettings pollSettings)
        {
            _navigation.GoToUrl(address);
        }

        public async Task ReloadAsync(CancellationToken cancellationToken, PollSettings pollSettings)
        {
            _navigation.Refresh();
        }
        internal void TriggerKeyboardCommandListener()
        {
            Task.Run(KeyboardListener);
        }

        private async Task KeyboardListener()
        {
            _logger.LogWarning("{name} triggered a keyboard command listener. You can try to kill it by typing 'q'.", nameof(SeleniumChromeClient));
            while (true)
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

                    HttpRequest request = new(requestId, headers, method, url);

                    WebClientEvent?.Invoke(sender, new Network_RequestWillBeSentEventArgs(request));

                    break;

                default:
                    WebClientEvent?.Invoke(sender, new RdpEventArgs(e.DomainName, e.EventName, e.EventData));
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


        public async Task<JToken> ExecuteRdpCommandAsync(string commandName, JToken commandParams)
        {
            return await _devToolsSession.SendCommand(commandName, commandParams);
        }
    }
}

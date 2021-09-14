using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebScrapingServices.Authenticated.Browser.Selenium;

namespace WebScrapingServices.Authenticated.Browser
{
    public interface IWebClientFactory
    {
        public Task<IWebClient> LaunchAndConnectAsync();
        public Task<IWebClient> LaunchAndConnectAsync(WebClientSettings settings);
    }
    public class WebClientFactory : IWebClientFactory
    {
        private ILogger<SeleniumChromeClient> _seleniumChromeClientLogger;
        private ILogger _logger;
        public WebClientFactory(ILogger<WebClientFactory> factoryLogger, ILogger<SeleniumChromeClient> seleniumChromeClientLogger)
        {
            _logger = factoryLogger;
            _seleniumChromeClientLogger = seleniumChromeClientLogger;
        }
        public async Task<IWebClient> LaunchAndConnectAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IWebClient> LaunchAndConnectAsync(WebClientSettings settings)
        {
            switch (settings.RdpClientImplementation)
            {
                case RdpClientImplementation.Any:
                case RdpClientImplementation.Selenium:

                    return new SeleniumChromeClient(_seleniumChromeClientLogger, settings);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}

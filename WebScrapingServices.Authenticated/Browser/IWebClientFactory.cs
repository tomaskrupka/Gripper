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
        private ILoggerFactory _loggerFactory;
        private ILogger _logger;
        public WebClientFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<WebClientFactory>();
        }
        public async Task<IWebClient> LaunchAndConnectAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IWebClient> LaunchAndConnectAsync(WebClientSettings settings)
        {
            switch (settings.WebClientImplementation)
            {
                case WebClientImplementation.BaristaLabsCdtr:
                    return new BaristaLabsCdtr.CdtrChromeClient(settings);

                case WebClientImplementation.Selenium:
                    return new SeleniumChromeClient(_loggerFactory, settings);

                case WebClientImplementation.Any:
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

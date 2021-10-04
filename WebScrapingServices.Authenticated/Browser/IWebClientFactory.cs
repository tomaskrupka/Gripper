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
        private IJsBuilder _jsBuilder;

        public WebClientFactory(ILoggerFactory loggerFactory, IJsBuilder jsBuilder)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<WebClientFactory>();
            _jsBuilder = jsBuilder;
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
                    var cdtrElementFactory = new BaristaLabsCdtr.CdtrElementFactory();
                    return new BaristaLabsCdtr.CdtrChromeClient(_loggerFactory, cdtrElementFactory, settings);

                case WebClientImplementation.Selenium:
                    return new SeleniumChromeClient(_loggerFactory, settings);

                case WebClientImplementation.Any:
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

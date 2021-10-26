using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Gripper.Authenticated.Browser.Selenium;

namespace Gripper.Authenticated.Browser
{
    public interface IWebClientFactory
    {
        public Task<IWebClient> LaunchAsync();
        public Task<IWebClient> LaunchAsync(WebClientSettings settings);
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
        public async Task<IWebClient> LaunchAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IWebClient> LaunchAsync(WebClientSettings settings)
        {
            switch (settings.WebClientImplementation)
            {
                case WebClientImplementation.BaristaLabsCdtr:
                    var cdtrElementFactory = new BaristaLabsCdtr.CdtrElementFactory(_loggerFactory);
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

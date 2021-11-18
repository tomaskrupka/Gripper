using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
        private IJsBuilder _jsBuilder;
        private ILogger _logger;

        public WebClientFactory(ILoggerFactory loggerFactory, IJsBuilder jsBuilder)
        {
            _loggerFactory = loggerFactory;
            _jsBuilder = jsBuilder;
            _logger = _loggerFactory.CreateLogger<WebClientFactory>();
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
                    return new BaristaLabsCdtr.CdtrChromeClient(_loggerFactory, cdtrElementFactory, _jsBuilder, settings);

                case WebClientImplementation.Selenium:
                //return new SeleniumChromeClient(_loggerFactory, settings);

                case WebClientImplementation.Any:
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

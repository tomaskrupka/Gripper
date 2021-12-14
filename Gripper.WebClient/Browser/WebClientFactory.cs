using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Gripper.WebClient.Browser
{
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
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IWebClient> LaunchAsync(WebClientSettings settings)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
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

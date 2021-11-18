using BaristaLabs.ChromeDevTools.Runtime;
using BaristaLabs.ChromeDevTools.Runtime.Page;
using BaristaLabs.ChromeDevTools.Runtime.Runtime;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Authenticated.Browser.BaristaLabsCdtr
{
    internal class CdtrContextFactory : ICdtrContextFactory
    {
        private ILogger _logger;
        private ILoggerFactory _loggerFactory;
        private ICdtrElementFactory _cdtrElementFactory;
        private IJsBuilder _jsBuilder;
        private ChromeSession _chromeSession;

        internal CdtrContextFactory(ILoggerFactory loggerFactory, ICdtrElementFactory cdtrElementFactory, IJsBuilder jsBuilder, ChromeSession chromeSession)
        {
            _loggerFactory = loggerFactory;
            _cdtrElementFactory = cdtrElementFactory;
            _chromeSession = chromeSession;
            _jsBuilder = jsBuilder;
            _logger = _loggerFactory.CreateLogger<CdtrContextFactory>();
        }

        public async Task<IContext> CreateContextAsync(int contextId, Frame frame)
        {
            var frameInfo = new CdtrFrameInfo(frame);

            var context = new CdtrContext(
                contextId, 
                _loggerFactory.CreateLogger<CdtrContext>(),
                frameInfo,
                _chromeSession,
                _cdtrElementFactory,
                _jsBuilder);

            return context;
        }

        public class CdtrFrameInfo : IFrameInfo
        {
            public CdtrFrameInfo(Frame frame)
            {
                FrameId = frame.Id;
                Name = frame.Name;
                Url = frame.Url;
            }
            public string FrameId { get; private set; }
            public string Name { get; private set; }
            public string Url { get; private set; }
        }
    }
}

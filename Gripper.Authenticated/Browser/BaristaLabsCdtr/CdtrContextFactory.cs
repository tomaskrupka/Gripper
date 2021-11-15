using BaristaLabs.ChromeDevTools.Runtime;
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
        private ChromeSession _chromeSession;

        internal CdtrContextFactory(ILoggerFactory loggerFactory, ICdtrElementFactory cdtrElementFactory, ChromeSession chromeSession)
        {
            _loggerFactory = loggerFactory;
            _cdtrElementFactory = cdtrElementFactory;
            _chromeSession = chromeSession;

            _logger = _loggerFactory.CreateLogger<CdtrContextFactory>();
        }

        public async Task<IContext> CreateContextAsync(ExecutionContextDescription executionContextDescription)
        {

            if (executionContextDescription.AuxData is not JObject auxData)
            {
                throw new ArgumentException("Passed {argument} did not contain valid AuxData.", nameof(executionContextDescription));
            }

            var frameId = (string?)auxData["frameId"];

            if (frameId == null)
            {
                throw new ArgumentException("Passed {argument} did not contain valid AuxData.", nameof(executionContextDescription));
            }

            try
            {
                //var frameOwnerResponse = await _chromeSession.DOM.GetFrameOwner(new BaristaLabs.ChromeDevTools.Runtime.DOM.GetFrameOwnerCommand
                //{
                //    FrameId = frameId
                //},
                //throwExceptionIfResponseNotReceived: false);
                //var frameOwner = frameOwnerResponse.NodeId;

                //_logger.LogDebug("Resolved frame {frameId} owner: {owner}", frameId, frameOwner);

                var frameInfo = new CdtrFrameInfo("0");

                var context = new CdtrContext(
                    executionContextDescription.Id,
                    0,
                    _loggerFactory.CreateLogger<CdtrContext>(),
                    frameInfo,
                    _chromeSession,
                    _cdtrElementFactory,
                    executionContextDescription);

                return context;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public class CdtrFrameInfo : IFrameInfo
        {
            public CdtrFrameInfo(string frameId)
            {
                FrameId = frameId;
            }
            public string FrameId { get; private set; }
        }
    }
}

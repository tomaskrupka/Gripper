using BaristaLabs.ChromeDevTools.Runtime;
using BaristaLabs.ChromeDevTools.Runtime.DOM;
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

namespace Gripper.WebClient.Cdtr
{
    public class CdtrContextFactory : IContextFactory
    {
        private ILoggerFactory _loggerFactory;
        private ICdpAdapter _cpAdapter;
        private IElementFactory _cdtrElementFactory;
        private IJsBuilder _jsBuilder;

        private ILogger _logger;

        /// <summary>
        /// Get root node backend node id. If context has no DOM (e.g. background worker thread) or nothing has been loaded yet, returns null.
        /// </summary>
        /// <param name="contextId">Id of context to try and find the root node id for.</param>
        /// <returns>#document backend node id or null if irrelevant.</returns>
        private async Task<long?> GetDocumentBackendNodeIdAsync(long contextId)
        {
            try
            {
                _logger.LogDebug("Looking for DOM root node, contextId: {contextId}.", contextId);

                var chromeSession = await _cpAdapter.GetChromeSessionAsync();

                var myDocument = await chromeSession.Runtime.Evaluate(
                    new EvaluateCommand
                    {
                        Expression = "document",
                        ContextId = contextId
                    },
                    throwExceptionIfResponseNotReceived: false);

                _logger.LogDebug("Document has Id: {documentId} and description: {description}.", myDocument?.Result?.ObjectId ?? "null", myDocument?.Result?.Description ?? "null");

                if (myDocument?.Result?.ObjectId == null)
                {
                    return null;
                }

                var nodeDescription = await chromeSession.DOM.DescribeNode(
                    new DescribeNodeCommand
                    {
                        ObjectId = myDocument.Result.ObjectId
                    },
                    throwExceptionIfResponseNotReceived: false);

                if (nodeDescription?.Node?.BackendNodeId == null)
                {
                    return null;
                }

                var documentBackendNodeId = nodeDescription.Node.BackendNodeId;

                _logger.LogDebug("document has node id: {nodeId}.", documentBackendNodeId);

                return documentBackendNodeId;
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to {name}: {e}", nameof(GetDocumentBackendNodeIdAsync), e);
                return null;
            }
        }

        public CdtrContextFactory(ILoggerFactory loggerFactory, ICdpAdapter cdpAdapter, IElementFactory cdtrElementFactory, IJsBuilder jsBuilder)
        {
            _loggerFactory = loggerFactory;
            _cpAdapter = cdpAdapter;
            _cdtrElementFactory = cdtrElementFactory;
            _jsBuilder = jsBuilder;

            _logger = _loggerFactory.CreateLogger<CdtrContextFactory>();
        }

        public Task<bool> TryCreateContextAsync(int contextId, Frame frame, out IContext context)
        {
            var frameInfo = new CdtrFrameInfo(frame);

            var backendNodeId = GetDocumentBackendNodeIdAsync(contextId, cdpAdapter, logger);
            if (backendNodeId == null)
            {
                context = null;
                return false;
            }
            else
            {
                context = new CdtrContext(contextId, (long)backendNodeId, logger, frameInfo, chromeSession, cdtrElementFactory, jsBuilder);
                return true;
            }

            var contextCreated = CdtrContext.TryCreate(
                contextId,
                _loggerFactory.CreateLogger<CdtrContext>(),
                frameInfo,
                _chromeSession,
                _cdtrElementFactory,
                _jsBuilder,
                out CdtrContext cdtrContext);

            context = cdtrContext;
            return contextCreated;
        }

        public Task<bool> TryCreateContextAsync(string frameId, out IContext? context)
        {
            throw new NotImplementedException();
        }
    }
}

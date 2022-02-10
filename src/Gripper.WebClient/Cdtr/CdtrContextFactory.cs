using BaristaLabs.ChromeDevTools.Runtime.DOM;
using BaristaLabs.ChromeDevTools.Runtime.Runtime;
using Gripper.WebClient.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gripper.WebClient.Cdtr
{
    internal class CdtrContextFactory : IContextFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ICdpAdapter _cdpAdapter;
        private readonly IElementFactory _cdtrElementFactory;
        private readonly IJsBuilder _jsBuilder;
        private readonly IContextManager _contextManager;
        private readonly ILogger _logger;

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

                var chromeSession = _cdpAdapter.ChromeSession;

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

        public CdtrContextFactory(ILoggerFactory loggerFactory, ICdpAdapter cdpAdapter, IElementFactory cdtrElementFactory, IJsBuilder jsBuilder, IContextManager contextManager)
        {
            _loggerFactory = loggerFactory;
            _cdpAdapter = cdpAdapter;
            _cdtrElementFactory = cdtrElementFactory;
            _jsBuilder = jsBuilder;
            _contextManager = contextManager;

            _logger = _loggerFactory.CreateLogger<CdtrContextFactory>();
        }

        public async Task<IContext?> CreateContextAsync(IFrameInfo frameInfo)
        {
            var executionContexts = _contextManager.GetContextDescriptions();
            var frameContexts = executionContexts.Where(x => ((JObject)x.AuxData)["frameId"]?.ToString() == frameInfo.FrameId).ToList();

            if (frameContexts.Count == 0)
            {
                _logger.LogInformation("{name} failed to find context by frameId {frameId}.", nameof(CreateContextAsync), frameInfo.FrameId);
                return null;
            }
            else if (frameContexts.Count == 1)
            {
                var frameContext = frameContexts[0];

                if (frameContext == null)
                {
                    _logger.LogInformation("{name} error: Context with frameId {frameId} was null.", nameof(CreateContextAsync), frameInfo.FrameId);
                    return null;
                }
                else
                {
                    var documentBackendNodeId = await GetDocumentBackendNodeIdAsync(frameContext.Id);

                    if (documentBackendNodeId == null)
                    {
                        _logger.LogInformation("{name} error: Context with frameId {frameId} had no DOM root.", nameof(CreateContextAsync), frameInfo.FrameId);
                        return null;
                    }
                    else
                    {
                        var documentBackendNodeIdValid = (long)documentBackendNodeId;

                        return new CdtrContext(
                            frameContext.Id,
                            documentBackendNodeIdValid,
                            _loggerFactory.CreateLogger<CdtrContext>(),
                            frameInfo,
                            _cdpAdapter,
                            _cdtrElementFactory,
                            _jsBuilder);
                    }
                }
            }
            else
            {
                _logger.LogInformation("{name} found more than one contexts for frame {frameId}.", nameof(CdtrContextFactory), frameInfo.FrameId);

                foreach (var frameContext in frameContexts)
                {
                    if (frameContext == null)
                    {
                        continue;
                    }
                    else
                    {
                        var documentBackendNodeId = await GetDocumentBackendNodeIdAsync(frameContext.Id);

                        if (documentBackendNodeId == null)
                        {
                            continue;
                        }
                        else
                        {
                            var documentBackendNodeIdValid = (long)documentBackendNodeId;

                            _logger.LogInformation("{name} found valid context for frame {frameId}.", nameof(CreateContextAsync), frameInfo.FrameId);

                            return new CdtrContext(
                                frameContext.Id,
                                documentBackendNodeIdValid,
                                _loggerFactory.CreateLogger<CdtrContext>(),
                                frameInfo,
                                _cdpAdapter,
                                _cdtrElementFactory,
                                _jsBuilder);
                        }
                    }
                }

                _logger.LogWarning("None of the contexts found for frame {frameId} had access to its DOM.", frameInfo.FrameId);
                return null;
            }
        }
    }
}

using BaristaLabs.ChromeDevTools.Runtime;
using BaristaLabs.ChromeDevTools.Runtime.DOM;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Authenticated.Browser.BaristaLabsCdtr
{
    internal class CdtrContext : IContext
    {
        private readonly int _contextId;
        private readonly string _documentNodeId;

        private readonly ILogger _logger;
        private readonly ChromeSession _chromeSession;
        private readonly ICdtrElementFactory _cdtrElementFactory;

        internal CdtrContext(ILogger logger, int contextId, string documentNodeId, ChromeSession chromeSession, ICdtrElementFactory cdtrElementFactory)
        {
            _contextId = contextId;
            _documentNodeId = documentNodeId;

            _logger = logger;
            _chromeSession = chromeSession;
            _cdtrElementFactory = cdtrElementFactory;
        }

        private async Task<Node> GetDocumentNodeAsync(CancellationToken cancellationToken)
        {
            var getDocumentResult = await _chromeSession.DOM.GetDocument(new GetDocumentCommand
            {
                Depth = 1,
            },
            throwExceptionIfResponseNotReceived: false,
            cancellationToken: cancellationToken);

            return getDocumentResult.Root;
        }

        public async Task<string?> ExecuteScriptAsync(string script, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _chromeSession.Runtime.Evaluate(new BaristaLabs.ChromeDevTools.Runtime.Runtime.EvaluateCommand
                {
                    Expression = script,
                    ContextId = _contextId,
                    AwaitPromise = true
                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: cancellationToken);

                if (result.Result.Type == "string")
                {
                    return result?.Result?.Value?.ToString();
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to execute script: {e}", e);
                throw;
            }
        }

        public async Task<IElement?> WaitUntilElementPresentAsync(string cssSelector, PollSettings pollSettings, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            while (!cancellationToken.IsCancellationRequested && stopwatch.ElapsedMilliseconds < pollSettings.TimeoutMs)
            {
                var element = await FindElementByCssSelectorAsync(cssSelector, cancellationToken);
                if (element != null)
                {
                    return element;
                }
                else
                {
                    await Task.Delay(pollSettings.PeriodMs, cancellationToken);
                }
            }

            return null;
        }

        public async Task<IElement?> FindElementByCssSelectorAsync(string cssSelector, CancellationToken cancellationToken)
        {
            try
            {
                // [TODO] find document node id as DOM.GetFrameOwner
                var documentNode = await GetDocumentNodeAsync(cancellationToken);

                if (documentNode == null)
                {
                    return null;
                }

                _logger.LogDebug("Resolved documentNode id: {documentNodeId}", documentNode.NodeId);

                if (documentNode.NodeId == 0)
                {
                    return null;
                }

                var querySelectorResult = await _chromeSession.DOM.QuerySelector(new QuerySelectorCommand
                {
                    Selector = cssSelector,
                    NodeId = documentNode.NodeId
                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: cancellationToken);

                _logger.LogDebug("Resolved node id: {nodeId}", querySelectorResult.NodeId);

                if (querySelectorResult.NodeId == 0)
                {
                    _logger.LogWarning("Node id resolved as 0: {cssSelector}", cssSelector);
                    return null;
                }

                return _cdtrElementFactory.CreateCdtrElement(querySelectorResult.NodeId, _chromeSession, cancellationToken);
            }
            catch (CommandResponseException e)
            {
                _logger.LogWarning("{name} while page was loading resulted in an exception: {e}.", nameof(FindElementByCssSelectorAsync), e);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to {name}: {e}.", nameof(FindElementByCssSelectorAsync), e);

                throw;
            }
        }
    }
}

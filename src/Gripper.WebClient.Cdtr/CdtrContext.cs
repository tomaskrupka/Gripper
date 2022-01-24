using BaristaLabs.ChromeDevTools.Runtime;
using BaristaLabs.ChromeDevTools.Runtime.DOM;
using BaristaLabs.ChromeDevTools.Runtime.Runtime;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient.Cdtr
{
    internal class CdtrContext : IContext
    {
        private readonly int _contextId; // for Runtime namespace
        private readonly long _documentBackendNodeId; // for DOM namespace

        private readonly ILogger _logger;
        private readonly IFrameInfo _frameInfo;
        private readonly ICdpAdapter _cdpAdapter;
        private readonly IElementFactory _cdtrElementFactory;
        private readonly IJsBuilder _jsBuilder;


        #region Ctor + pseudo ctor


        /// <summary>
        /// Ctor. Frame must be loaded when calling this ctor.
        /// </summary>
        private CdtrContext(
            int contextId,
            long documentBackendNodeId,
            ILogger logger,
            IFrameInfo frameInfo,
            ICdpAdapter cdpAdapter,
            IElementFactory cdtrElementFactory,
            IJsBuilder jsBuilder)
        {
            _logger = logger;

            _logger.LogDebug("Creating context with id: {contextId}...", contextId);

            _contextId = contextId;
            _documentBackendNodeId = documentBackendNodeId;
            _frameInfo = frameInfo;
            _cdpAdapter = cdpAdapter;
            _cdtrElementFactory = cdtrElementFactory;
            _jsBuilder = jsBuilder;
        }

        public static bool TryCreate(
            int contextId,
            ILogger logger,
            IFrameInfo frameInfo,
            ICdpAdapter cdpAdapter,
            IElementFactory cdtrElementFactory,
            IJsBuilder jsBuilder,
            out CdtrContext context)
        {
        }


        #endregion

        #region Public API

        public int Id => _contextId;

        public IFrameInfo FrameInfo => _frameInfo;

        public async Task<string> ExecuteScriptAsync(string script, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _chromeSession.Runtime.Evaluate(new EvaluateCommand
                {
                    Expression = script,
                    ContextId = _contextId,
                    AwaitPromise = true
                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: cancellationToken);

                if (result?.Result?.Type == "string")
                {
                    return result?.Result?.Value?.ToString() ?? JsonConvert.SerializeObject(result);
                }
                else
                {
                    return JsonConvert.SerializeObject(result);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to execute script: {e}", e);
                throw;
            }
        }

        public async Task<IElement> WaitUntilElementPresentAsync(string cssSelector, PollSettings pollSettings, CancellationToken cancellationToken)
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

        public async Task<IElement> FindElementByCssSelectorAsync(string cssSelector, CancellationToken cancellationToken)
        {
            try
            {
                var expression = _jsBuilder.DocumentQuerySelector(cssSelector);
                var evaluation = await _chromeSession.Runtime.Evaluate(new EvaluateCommand
                {
                    ContextId = _contextId,
                    Expression = expression
                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: cancellationToken);

                if (evaluation?.Result?.ObjectId == null)
                {
                    _logger.LogDebug("Failed to {evaluate} element by css selector: {cssSelector}. Are you in the right context?", nameof(_chromeSession.Runtime.Evaluate), cssSelector);
                    return null;
                }

                // TODO: check for correct subtype.

                if (evaluation.ExceptionDetails != null)
                {
                    _logger.LogDebug("Failed to {evaluate} element by css selector: {cssSelector}: {e}", nameof(_chromeSession.Runtime.Evaluate), cssSelector, evaluation.ExceptionDetails.Exception.Description);
                    return null;
                }

                var node = await _chromeSession.DOM.DescribeNode(new DescribeNodeCommand
                {
                    ObjectId = evaluation.Result.ObjectId
                },
                cancellationToken: cancellationToken,
                throwExceptionIfResponseNotReceived: false);

                if (node == null)
                {
                    _logger.LogWarning("Failed to {describeNode} with object id {objectId}. Are you in the right context?", nameof(_chromeSession.DOM.DescribeNode), evaluation.Result.ObjectId);
                    return null;
                }

                return _cdtrElementFactory.CreateCdtrElement(node.Node.BackendNodeId, _chromeSession, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to {name}: {e}.", nameof(FindElementByCssSelectorAsync), e);

                throw;
            }
        }
        #endregion
    }
}

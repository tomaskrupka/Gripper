using Gripper.ChromeDevTools.DOM;
using Gripper.ChromeDevTools.Page;
using Gripper.ChromeDevTools.Runtime;
using Gripper.WebClient.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient
{
    internal class Context : IContext
    {
        private readonly long _contextId; // for Runtime namespace
        private readonly long _documentBackendNodeId; // for DOM namespace

        private readonly ILogger _logger;
        private readonly Frame _frame;
        private readonly ICdpAdapter _cdpAdapter;
        private readonly IElementFactory _cdtrElementFactory;
        private readonly IJsBuilder _jsBuilder;

        /// <summary>
        /// Ctor. 
        /// </summary>
        /// <remarks>
        /// Frame must be loaded when calling this ctor.
        /// </remarks>
        internal Context(
            long contextId,
            long documentBackendNodeId,
            ILogger logger,
            Frame frame,
            ICdpAdapter cdpAdapter,
            IElementFactory cdtrElementFactory,
            IJsBuilder jsBuilder)
        {
            _logger = logger;
            _contextId = contextId;
            _documentBackendNodeId = documentBackendNodeId;
            _frame = frame;
            _cdpAdapter = cdpAdapter;
            _cdtrElementFactory = cdtrElementFactory;
            _jsBuilder = jsBuilder;
        }

        public long Id => _contextId;

        public Frame Frame => _frame;

        public async Task<EvaluateCommandResponse> ExecuteScriptAsync(string script, CancellationToken cancellationToken)
        {
            try
            {
                var chromeSession = _cdpAdapter.ChromeSession;
                var result = await chromeSession.Runtime.Evaluate(new EvaluateCommand
                {
                    Expression = script,
                    ContextId = _contextId,
                    AwaitPromise = true
                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: cancellationToken);

                return result;
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
                var element = await FindElementByCssSelectorAsync(cssSelector);
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

        public async Task<IElement?> FindElementByCssSelectorAsync(string cssSelector)
        {
            try
            {
                var chromeSession = _cdpAdapter.ChromeSession;
                var expression = _jsBuilder.DocumentQuerySelector(cssSelector);
                var evaluation = await chromeSession.Runtime.Evaluate(new EvaluateCommand
                {
                    ContextId = _contextId,
                    Expression = expression
                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: CancellationToken.None);

                if (evaluation?.Result?.ObjectId == null)
                {
                    _logger.LogDebug("Failed to {evaluate} element by css selector: {cssSelector}. Are you in the right context?", nameof(chromeSession.Runtime.Evaluate), cssSelector);
                    return null;
                }

                // TODO: check for correct subtype.

                if (evaluation.ExceptionDetails != null)
                {
                    _logger.LogDebug("Failed to {evaluate} element by css selector: {cssSelector}: {e}", nameof(chromeSession.Runtime.Evaluate), cssSelector, evaluation.ExceptionDetails.Exception.Description);
                    return null;
                }

                var node = await chromeSession.DOM.DescribeNode(new DescribeNodeCommand
                {
                    ObjectId = evaluation.Result.ObjectId
                },
                cancellationToken: CancellationToken.None,
                throwExceptionIfResponseNotReceived: false);

                if (node == null)
                {
                    _logger.LogWarning("Failed to {describeNode} with object id {objectId}. Are you in the right context?", nameof(chromeSession.DOM.DescribeNode), evaluation.Result.ObjectId);
                    return null;
                }

                return _cdtrElementFactory.CreateElement(node.Node.BackendNodeId);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to {name}: {e}.", nameof(FindElementByCssSelectorAsync), e);

                throw;
            }
        }
    }
}

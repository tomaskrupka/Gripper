using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient
{
    /// <summary>
    /// Provides a 1-1 mapping to a global execution context of an iFrame that contains a document node.
    /// </summary>
    /// <remarks>
    /// Implementations must ensure that if there are multiple contexts for an iFrame (e.g. a background worker thread and the main context),
    /// the main context is referenced. 
    /// </remarks>
    public interface IContext
    {
        /// <summary>
        /// Id of the context as defined by the browser backend.
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Information about the Frame mapped to the context.
        /// </summary>
        public IFrameInfo FrameInfo { get; }

        /// <summary>
        /// Executes a script in the global context.
        /// </summary>
        /// <param name="script">The script to execute.</param>
        /// <param name="cancellationToken">Token to cancel the <see cref="Task"/>.</param>
        /// <returns>A RemoteObject mapped to a <see cref="JToken"/> that represents the result of the operation.</returns>
        public Task<JToken> ExecuteScriptAsync(string script, CancellationToken cancellationToken);

        /// <summary>
        /// Finds an element by a CSS selector on the document node of the Frame.
        /// </summary>
        /// <param name="cssSelector">The CSS selector of the targeted element.</param>
        /// <returns>The resulting <see cref="IElement"/>, or null if no element was matched within the document of the iFrame.</returns>
        /// <remarks>
        /// This should not be implemented as a mapping of the DOM.querySelector call:
        /// <see href="https://chromedevtools.github.io/devtools-protocol/tot/DOM/#method-querySelector"/>
        /// which is unreliable as it only takes the NodeId (as opposed to the BackendNodeId) as a parameter.
        /// <see href="https://github.com/ChromeDevTools/devtools-protocol/issues/72"/>
        /// </remarks>
        public Task<IElement?> FindElementByCssSelectorAsync(string cssSelector);

        /// <summary>
        /// Polls for an element defined by a specified CSS selector.
        /// </summary>
        /// <param name="cssSelector">The CSS selector of the targeted element.</param>
        /// <param name="pollSettings">Settings that control the polling for changes to the DOM.</param>
        /// <param name="cancellationToken">Token to cancel the <see cref="Task"/>.</param>
        /// <returns>The resulting <see cref="IElement"/>,
        /// or null if no element was matched within the document of the iFrame before the <see cref="PollSettings.TimeoutMs"/> period elapsed.</returns>
        public Task<IElement?> WaitUntilElementPresentAsync(string cssSelector, PollSettings pollSettings, CancellationToken cancellationToken);
    }

}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient
{
    /// <summary>
    /// Provides methods and members to interact with an HTML element on the page.
    /// </summary>
    /// <remarks>
    /// <see cref="IElement"/> can be mapped to a <see href="https://developer.mozilla.org/en-US/docs/Web/API/Node/nodeType">Node</see> of any type, not just Element. <see cref="IElement"/> throws a <see cref="System.NotSupportedException"/> for incompatible method calls on such nodes.
    /// </remarks>
    public interface IElement
    {
        /// <summary>
        /// Dispatches a click event onto the area of the element.
        /// </summary>
        /// <param name="clickDurationMs">Delay between the MouseDown and MouseUp events.</param>
        /// <returns>A <see cref="Task"/> representing the click operation</returns>
        /// <remarks>
        /// The implementations should take advantage of the 
        /// <see href="https://chromedevtools.github.io/devtools-protocol/tot/Input/#method-dispatchMouseEvent">dispatch mouse event</see>.
        /// Do not use the DOM .click() or invoke the onclick() or other mouse events by evaluating a script.
        /// This is unreliable and easy to detect.
        /// </remarks>
        public Task ClickAsync(int clickDurationMs);

        /// <summary>
        /// Focuses the element.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the focus operation.</returns>
        public Task FocusAsync();

        /// <summary>
        /// Returns the textContent of the element.
        /// </summary>
        /// <returns>The text content of the specified element.</returns>
        public Task<string> GetTextContentAsync();

        /// <summary>
        /// Sends an array of keystrokes to the browser while keeping the element focused.
        /// </summary>
        /// <param name="keys">Keys to send to the element.</param>
        /// <param name="delayBetweenStrokes">Delay to wait between strokes.</param>
        /// <returns>A <see cref="Task"/> representing the keystrokes operation.</returns>
        public Task SendKeysAsync(string keys, TimeSpan delayBetweenStrokes);

        /// <summary>
        /// Sends a <see cref="SpecialKey"/> to the browser right after focusing the element.
        /// </summary>
        /// <param name="key">The <see cref="SpecialKey"/> to send to the element.</param>
        /// <returns>A <see cref="Task"/> representing the keystroke operation.</returns>
        public Task SendSpecialKeyAsync(SpecialKey key);

        /// <summary>
        /// Blocks until the element can receive keyboard or mouse inputs.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the wait.</param>
        /// <returns>A task representing the blocking operation.</returns>
        public Task WaitUntilInteractable(CancellationToken cancellationToken);
    }
}

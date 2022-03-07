using Gripper.ChromeDevTools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient
{
    /// <summary>
    /// Enables interaction with the hooked web browser window.
    /// </summary>
    public interface IWebClient : IDisposable
    {
        /// <summary>
        /// The execution context that corresponds to the root of the page iFrame tree DOM.
        /// </summary>
        public IContext? MainContext { get; }

        /// <summary>
        /// An aggregate event handler for events from all CDP domains, all targets.
        /// </summary>
        /// <remarks>
        /// Note that target attachment is handled automatically, see <see cref="WebClientSettings"/> for configuration.
        /// Call <see cref="ExecuteCdpCommandAsync"/> commands 'domain.Subscribe()' and 'domain.Unsubscribe()' to a specific CDP domain to start/stop receiving these events.
        /// </remarks>
        public event EventHandler<IEvent> WebClientEvent;

        /// <summary>
        /// Gets all cookies stored by the browser.
        /// </summary>
        /// <returns>A <see cref="CookieContainer"/> that can be plugged as-is into an <see cref="System.Net.Http.HttpClientHandler"/></returns>
        public Task<ICollection<Cookie>> GetAllCookiesAsync();

        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> of interactable contexts.
        /// There is no guarantee w.r.t. the lifespan of the resulting <see cref="IContext"/>s.
        /// </summary>
        /// <returns>A flattened projection of a snapshot of the current iFrame tree into an <see cref="IReadOnlyCollection{IContext}"/></returns>
        public Task<IReadOnlyCollection<IContext>> GetContextsAsync();

        /// <summary>
        /// Gets the current URL of the top page.
        /// </summary>
        /// <returns>Current URL of the top page or null if the window is not displaying a response to any Http request 
        /// (e.g. if last request was 4XX or if the top frame is in a state between the Page.frameStartedLoading and Page.frameNavigated events.</returns>
        public Task<string?> GetCurrentUrlAsync();

        /// <summary>
        /// Blocks until either<br />
        /// 1. All of the following has happened:<br />
        /// 1.1. No frame has been added to the frame tree for one <see cref="PollSettings.PeriodMs"/> period,<br />
        /// 1.2. All frames in the frame tree have received the Page.frameNavigated and Page.frameLoaded events,<br />
        /// 1.3. One <see cref="PollSettings.PeriodMs"/> period has elapsed since the last Page.frameNavigated or Page.frameLoaded event,<br />
        /// or<br />
        /// 2. <see cref="PollSettings.TimeoutMs"/> has elapsed.<br />
        /// or<br />
        /// 3. Task has been cancelled.<br />
        /// </summary>
        /// <param name="pollSettings">Settings to control the polling for changes to the frame tree.</param>
        /// <param name="cancellationToken">Token to cancel the <see cref="Task"/>.</param>
        /// <returns><see cref="Task"/> that represents the block.</returns>
        public Task WaitUntilFramesLoadedAsync(PollSettings pollSettings, CancellationToken cancellationToken);

        /// <summary>
        /// Navigates to the address and awaits the resulting <see cref="Task"/> of <see cref="WaitUntilFramesLoadedAsync(PollSettings, CancellationToken)"/>
        /// using the provided <see cref="PollSettings"/> and <see cref="CancellationToken"/>
        /// </summary>
        /// <param name="address">Address to navigate to. This can be a valid URI or a browser-specific command (e.g. about:blank, chrome://accessibility).</param>
        /// <param name="pollSettings">Settings to pass as a parameter to the <see cref="WaitUntilFramesLoadedAsync(PollSettings, CancellationToken)"/> call.</param>
        /// <param name="cancellationToken">Token to cancel the <see cref="Task"/>.</param>
        /// <returns>A <see cref="Task"/> that represents the navigation.</returns>
        public Task NavigateAsync(string address, PollSettings pollSettings, CancellationToken cancellationToken);

        /// <summary>
        /// Reloads the browser window and awaits the resulting <see cref="Task"/> of <see cref="WaitUntilFramesLoadedAsync(PollSettings, CancellationToken)"/>
        /// using the provided <see cref="PollSettings"/> and <see cref="CancellationToken"/>
        /// </summary>
        /// <param name="pollSettings">Settings to pass as a parameter to the <see cref="WaitUntilFramesLoadedAsync(PollSettings, CancellationToken)"/> call.</param>
        /// <param name="cancellationToken">Token to cancel the <see cref="Task"/>.</param>
        /// <returns>A <see cref="Task"/> that represents the reload.</returns>
        public Task ReloadAsync(PollSettings pollSettings, CancellationToken cancellationToken);

        /// <summary>
        /// Tunnels a CDP command directly to the CDP client endpoint. 
        /// </summary>
        /// <param name="commandName">Name of the command, e.g. 'Page.navigate'. <see href="https://chromedevtools.github.io/devtools-protocol/"/></param>
        /// <param name="commandParams"></param>
        /// <returns>A <see cref="Task"/> that represents the command execution.</returns>
        /// <remarks>
        /// Implementations should execute the command literally (no validation), and pass the result unmodified.
        /// </remarks>
        public Task<JToken> ExecuteCdpCommandAsync(string commandName, JToken commandParams);
    }
}

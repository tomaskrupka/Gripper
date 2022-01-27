using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient
{
    /// <summary>
    /// Provides methods and members to launch, manage, access and destroy a web browser instance.
    /// At mininum, implementations must configure connecting to the CDP endpoint of the browser and pre-startup and post-destroy cleanup.
    /// </summary>
    public interface IBrowserManager : IDisposable
    {
        /// <summary>
        /// Launches a browser instance and sets the <see cref="DebuggerUrl"/> and <see cref="BrowserProcess"/> members.
        /// </summary>
        /// <returns>A <see cref="Task"/>that completes when the <see cref="DebuggerUrl"/> and the <see cref="BrowserProcess"/> members have been initialized.</returns>
        public Task LaunchAsync(CancellationToken cancellationToken);

        /// <summary>
        /// The URL of the WebSocket listener of the browser's CDP server.
        /// </summary>
        public string DebuggerUrl { get; }

        /// <summary>
        /// The handle of the browser OS process.
        /// </summary>
        public Process BrowserProcess { get; }
    }
}

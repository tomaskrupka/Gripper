using BaristaLabs.ChromeDevTools.Runtime;
using BaristaLabs.ChromeDevTools.Runtime.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.WebClient.Cdtr
{
    /// <summary>
    /// Dependency inversion for BaristaLabs.chrome-dev-tools.
    /// Creates the ChromeSession for existing CDP client WS endpoint, then manages its lifetime.
    /// Relays incoming CDP events and provides methods to create snapshots of the current state of the browser frontend as defined by the CDP events.
    /// </summary>
    public interface ICdpAdapter
    {
        public event EventHandler<RdpEventArgs>? WebClientEvent;

        /// <summary>
        /// Gets reference to the <see cref="ChromeSession"/> singleton.
        /// </summary>
        /// <returns>The <see cref="ChromeSession"/> singleton that belongs to the CDP endpoint as configured.</returns>
        public Task<ChromeSession> GetChromeSessionAsync();

        /// <summary>
        /// Gets a collection of active execution contexts.
        /// </summary>
        /// <returns>A snapshot of the data structure tracking the CDP execution contexts.</returns>
        public Task<ICollection<ExecutionContextDescription>> GetContextDescriptionsAsync();
    }
}

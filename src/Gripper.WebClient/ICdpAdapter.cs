using Gripper.ChromeDevTools;
using System;
using System.Threading.Tasks;

namespace Gripper.WebClient
{
    /// <summary>
    /// Dependency inversion for BaristaLabs.chrome-dev-tools. Creates the ChromeSession for existing CDP client WS endpoint, then manages its lifetime. Tunnels the incoming CDP events and handles execution of CDP calls. 
    /// </summary>
    internal interface ICdpAdapter
    {
        /// <summary>
        /// Gets reference to the <see cref="ChromeSession"/> singleton.
        /// </summary>
        internal ChromeSession ChromeSession { get; }

        /// <summary>
        /// Enables subscription to any CDP event.
        /// </summary>
        internal event EventHandler<IEvent>? WebClientEvent;

        /// <summary>
        /// Binds the instance to the websocket endpoint of a running instance of an <see cref="IBrowserManager"/>.
        /// </summary>
        /// <returns></returns>
        internal Task BindAsync(IBrowserManager browserManager);
    }
}

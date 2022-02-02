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
    /// Dependency inversion for BaristaLabs.chrome-dev-tools. Creates the ChromeSession for existing CDP client WS endpoint, then manages its lifetime. Tunnels the incoming CDP events and handles execution of CDP calls. 
    /// </summary>
    internal interface ICdpAdapter
    {
        /// <summary>
        /// Enables subscription to any CDP event.
        /// </summary>
        internal event EventHandler<RdpEventArgs>? WebClientEvent;

        /// <summary>
        /// Gets reference to the <see cref="ChromeSession"/> singleton.
        /// </summary>
        /// <returns>The <see cref="ChromeSession"/> singleton that belongs to the CDP endpoint as configured.</returns>
        internal Task<ChromeSession> GetChromeSessionAsync();
    }
}

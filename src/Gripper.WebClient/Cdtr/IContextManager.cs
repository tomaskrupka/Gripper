using BaristaLabs.ChromeDevTools.Runtime.Runtime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gripper.WebClient.Cdtr
{
    /// <summary>
    /// Maintains and provides interface to the up-to-date data structure representing the execution contexts on the page.
    /// </summary>
    internal interface IContextManager
    {

        /// <summary>
        /// Gets a collection of active execution contexts.
        /// </summary>
        /// <returns>A snapshot of the data structure tracking the CDP execution contexts.</returns>
        internal ICollection<ExecutionContextDescription> GetContextDescriptions();
    }
}

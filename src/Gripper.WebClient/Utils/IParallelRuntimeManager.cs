using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.WebClient.Utils
{
    /// <summary>
    /// State container for running multiple <see cref="IWebClient"/> instances.
    /// </summary>
    /// <remarks>
    /// Instantiate as a singleton.
    /// This is a work-around the limitations of passing custom settings to transient <see cref="IWebClient"/> instances.
    /// </remarks>
    public interface IParallelRuntimeUtils
    {
        /// <summary>
        /// Gets unused TCP port to use as a CDP listener of a new browser instance.
        /// </summary>
        /// <returns>TCP port that's free to open.</returns>
        public int GetFreshTcpPort();
    }
}

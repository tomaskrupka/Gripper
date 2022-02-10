using BaristaLabs.ChromeDevTools.Runtime;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient.Cdtr
{
    /// <summary>
    /// Dependency inversion vehicle for <see cref="CdtrElement"/> implementations.
    /// </summary>
    internal interface IElementFactory
    {
        /// <summary>
        /// Create element that mirrors the backend node with given id.
        /// </summary>
        /// <param name="nodeId">The id of the backend node</param>
        /// <returns></returns>
        internal IElement CreateElement(long nodeId);
    }
}

using BaristaLabs.ChromeDevTools.Runtime;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient.Cdtr
{
    /// <summary>
    /// Dependency inversion vehicle for <see cref="CdtrElement"/> implementations.
    /// </summary>
    public interface IElementFactory
    {
        public Task<IElement> CreateElementAsync(long nodeId);
    }
}

using BaristaLabs.ChromeDevTools.Runtime;
using System.Threading;

namespace Gripper.Authenticated.Browser.BaristaLabsCdtr
{
    /// <summary>
    /// Dependency inversion vehicle for <see cref="CdtrElement"/> implementations.
    /// </summary>
    public interface ICdtrElementFactory
    {
        public IElement CreateCdtrElement(long nodeId, ChromeSession chromeSession, CancellationToken cancellationToken);
    }
}

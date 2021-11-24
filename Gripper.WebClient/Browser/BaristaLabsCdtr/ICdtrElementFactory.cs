using BaristaLabs.ChromeDevTools.Runtime;
using System.Threading;

namespace Gripper.WebClient.Browser.BaristaLabsCdtr
{
    /// <summary>
    /// Dependency inversion vehicle for <see cref="CdtrElement"/> implementations.
    /// </summary>
    public interface ICdtrElementFactory
    {
        public IElement CreateCdtrElement(long nodeId, ChromeSession chromeSession, CancellationToken cancellationToken);
    }
}

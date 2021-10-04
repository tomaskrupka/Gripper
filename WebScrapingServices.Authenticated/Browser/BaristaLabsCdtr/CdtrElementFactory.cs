using BaristaLabs.ChromeDevTools.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser.BaristaLabsCdtr
{
    internal class CdtrElementFactory : ICdtrElementFactory
    {
        IElement ICdtrElementFactory.CreateCdtrElement(long nodeId, ChromeSession chromeSession)
        {
            return new CdtrElement(nodeId, chromeSession);
        }
    }

    /// <summary>
    /// Dependency inversion vehicle for <see cref="CdtrElement"/> implementations.
    /// </summary>
    internal interface ICdtrElementFactory
    {
        internal IElement CreateCdtrElement(long nodeId, ChromeSession chromeSession);
    }
}

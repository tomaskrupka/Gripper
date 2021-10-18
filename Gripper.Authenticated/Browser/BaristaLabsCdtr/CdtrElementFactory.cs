using BaristaLabs.ChromeDevTools.Runtime;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Authenticated.Browser.BaristaLabsCdtr
{
    internal class CdtrElementFactory : ICdtrElementFactory
    {
        private ILoggerFactory _loggerFactory;
        public CdtrElementFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }
        IElement ICdtrElementFactory.CreateCdtrElement(long nodeId, ChromeSession chromeSession, CancellationToken cancellationToken)
        {
            var logger = _loggerFactory.CreateLogger<CdtrElement>();
            return new CdtrElement(logger, chromeSession, nodeId, cancellationToken);
        }
    }

    /// <summary>
    /// Dependency inversion vehicle for <see cref="CdtrElement"/> implementations.
    /// </summary>
    internal interface ICdtrElementFactory
    {
        internal IElement CreateCdtrElement(long nodeId, ChromeSession chromeSession, CancellationToken cancellationToken);
    }
}

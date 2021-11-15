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
        internal CdtrElementFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;

        }
        public IElement CreateCdtrElement(long nodeId, ChromeSession chromeSession, CancellationToken cancellationToken)
        {
            var logger = _loggerFactory.CreateLogger<CdtrElement>();

            var element = new CdtrElement(logger, chromeSession, nodeId, cancellationToken);

            return element;
        }
    }
}

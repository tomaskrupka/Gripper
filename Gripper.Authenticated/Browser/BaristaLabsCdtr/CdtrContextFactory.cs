using BaristaLabs.ChromeDevTools.Runtime;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.Authenticated.Browser.BaristaLabsCdtr
{
    internal interface ICdtrContextFactory
    {
        internal IContext CreateContext(ChromeSession chromeSession, int contextId, string documentNodeId);
    }
    internal class CdtrContextFactory : ICdtrContextFactory
    {
        private ILoggerFactory _loggerFactory;
        private ICdtrElementFactory _cdtrElementFactory;
        public CdtrContextFactory(ILoggerFactory loggerFactory, ICdtrElementFactory cdtrElementFactory)
        {
            _loggerFactory = loggerFactory;
            _cdtrElementFactory = cdtrElementFactory;
        }
        IContext ICdtrContextFactory.CreateContext(ChromeSession chromeSession, int contextId, string documentNodeId)
        {
            return new CdtrContext(_loggerFactory.CreateLogger<CdtrContext>(), contextId, documentNodeId, chromeSession, _cdtrElementFactory);
        }
    }
}

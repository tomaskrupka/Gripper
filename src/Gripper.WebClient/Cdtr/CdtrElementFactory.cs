using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient.Cdtr
{
    internal class CdtrElementFactory : IElementFactory
    {
        private ILoggerFactory _loggerFactory;
        private ICdpAdapter _cpdAdapter;

        public CdtrElementFactory(ILoggerFactory loggerFactory, ICdpAdapter cdpAdapter)
        {
            _loggerFactory = loggerFactory;
            _cpdAdapter = cdpAdapter;
        }

        public IElement CreateElement(long nodeId)
        {
            var logger = _loggerFactory.CreateLogger<CdtrElement>();
            var session = _cpdAdapter.ChromeSession;

            return new CdtrElement(logger, session, nodeId);
        }
    }
}

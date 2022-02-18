using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient
{
    internal class ElementFactory : IElementFactory
    {
        private ILoggerFactory _loggerFactory;
        private ICdpAdapter _cpdAdapter;

        public ElementFactory(ILoggerFactory loggerFactory, ICdpAdapter cdpAdapter)
        {
            _loggerFactory = loggerFactory;
            _cpdAdapter = cdpAdapter;
        }

        public IElement CreateElement(long nodeId)
        {
            var logger = _loggerFactory.CreateLogger<Element>();
            var session = _cpdAdapter.ChromeSession;

            return new Element(logger, session, nodeId);
        }
    }
}

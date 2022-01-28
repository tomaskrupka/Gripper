using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient.Cdtr
{
    public class CdtrElementFactory : IElementFactory
    {
        private ILoggerFactory _loggerFactory;
        private ICdpAdapter _cpdAdapter;
        public CdtrElementFactory(ILoggerFactory loggerFactory, ICdpAdapter cdpAdapter)
        {
            _loggerFactory = loggerFactory;
            _cpdAdapter = cdpAdapter;
        }
        public async Task<IElement> CreateElementAsync(long nodeId)
        {
            var logger = _loggerFactory.CreateLogger<CdtrElement>();
            var session = await _cpdAdapter.GetChromeSessionAsync();

            return new CdtrElement(logger, session, nodeId);
        }
    }
}

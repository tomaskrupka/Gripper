using BaristaLabs.ChromeDevTools.Runtime.Runtime;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gripper.WebClient.Cdtr
{
    internal class ContextManager : IContextManager
    {
        private readonly ILogger _logger;
        private readonly ICdpAdapter _cdpAdapter;

        private ConcurrentDictionary<long, ExecutionContextDescription> _executionContexts;

        private void HandleWebClientEvent(object? sender, RdpEventArgs e)
        {
            if (e is Events.Runtime_ExecutionContextCreatedEventArgs ceEvent)
            {
                _executionContexts[ceEvent.ContextId] = ceEvent.Description;
            }
            else if (e is Events.Runtime_ExecutionContextDestroyedEventArgs cdEvent)
            {
                _executionContexts.TryRemove(cdEvent.ContextId, out _);
            }
        }

        public ContextManager(ILogger<ContextManager> logger, ICdpAdapter cdpAdapter)
        {
            _logger = logger;
            _cdpAdapter = cdpAdapter;
            _cdpAdapter.WebClientEvent += HandleWebClientEvent;

            _executionContexts = new ConcurrentDictionary<long, ExecutionContextDescription>();
        }

        public async Task<ICollection<ExecutionContextDescription>> GetContextDescriptionsAsync()
        {
            return _executionContexts.Values;
        }
    }
}

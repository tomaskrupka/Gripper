using Gripper.ChromeDevTools;
using Gripper.ChromeDevTools.Runtime;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Gripper.WebClient
{
    internal class ContextManager : IContextManager
    {
        private readonly ILogger _logger;
        private readonly ICdpAdapter _cdpAdapter;

        private ConcurrentDictionary<long, ExecutionContextDescription> _executionContexts;

        private void HandleWebClientEvent(object? sender, IEvent e)
        {
            if (e is ExecutionContextCreatedEvent ceEvent)
            {
                _executionContexts[ceEvent.Context.Id] = ceEvent.Context;
            }
            else if (e is ExecutionContextDestroyedEvent cdEvent)
            {
                _executionContexts.TryRemove(cdEvent.ExecutionContextId, out _);
            }
        }

        public ContextManager(ILogger<ContextManager> logger, ICdpAdapter cdpAdapter)
        {
            _logger = logger;
            _cdpAdapter = cdpAdapter;
            _cdpAdapter.WebClientEvent += HandleWebClientEvent;

            _executionContexts = new ConcurrentDictionary<long, ExecutionContextDescription>();
        }

        public ICollection<ExecutionContextDescription> GetContextDescriptions()
        {
            return _executionContexts.Values;
        }
    }
}

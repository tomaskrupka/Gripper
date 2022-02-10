using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Gripper.WebClient.Utils
{
    public class ParallelRuntimeUtils : IParallelRuntimeUtils
    {
        private readonly object _lastUsedPortLock = new();
        private readonly WebClientSettings _settings;
        private readonly ILogger _logger;

        private int _lastUsedPort;

        public ParallelRuntimeUtils(ILogger<ParallelRuntimeUtils> logger, IOptions<WebClientSettings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
            _lastUsedPort = _settings.RemoteDebuggingPort;
        }

        public int GetFreshTcpPort()
        {
            if (_lastUsedPort > _settings.RemoteDebuggingPort + 100)
            {
                _logger.LogWarning(
                    "{name} used 100 fresh TCP ports, rolling back to {port}",
                    nameof(GetFreshTcpPort),
                    _settings.RemoteDebuggingPort);

                _lastUsedPort = _settings.RemoteDebuggingPort;
            }

            lock (_lastUsedPortLock)
            {
                _lastUsedPort++;
            }

            return _lastUsedPort;
        }

    }
}

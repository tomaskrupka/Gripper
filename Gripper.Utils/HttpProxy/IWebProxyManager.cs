using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Utils.HttpProxy
{
    public interface IWebProxyManager
    {
        public Task<IWebProxy> GetProxyAsync();
        public void BadProxy();
    }
    public interface IWebProxyContainer
    {
        public IWebProxy Proxy { get; }
        public Action ReportBadProxy { get; }

    }
    public class WebProxyManager : IWebProxyManager
    {
        private ILogger _logger;

        // (Proxy, is alive)
        private List<(IWebProxyContainer, bool)> _proxies;

        private AutoResetEvent _nextProxy;



        private List<IWebProxy> LoadProxies()
        {
            _logger.LogInformation("Loading proxies...");

            var proxyIps = Properties.Resources.Proxies.Split(",");
            var proxyPort = int.Parse(Properties.Resources.ProxyPort);

            var proxiesList = proxyIps.Select(x => new WebProxy(x, proxyPort) as IWebProxy).ToList();

            _logger.LogInformation("Done loading proxies. Loaded {proxiesCount} proxies.", proxiesList.Count);

            return proxiesList;
        }

        private void LoopProduceProxies()
        {
            var proxyIndex = 0;

            while (true)
            {
                var proxy = _proxies[proxyIndex];



                // Proceed to next proxy or return to beginning.
                if (proxyIndex == _proxies.Count - 1)
                {
                    proxyIndex = 0;
                }
                else
                {
                    proxyIndex++;
                }
            }

        }

        public async Task<IWebProxyContainer> GetProxyAsync()
        {
            throw new NotImplementedException();
        }
    }
}

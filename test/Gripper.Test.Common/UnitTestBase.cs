using Gripper.WebClient;
using Gripper.WebClient.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.Test.Common
{
    public abstract class UnitTestBase
    {
        private static readonly ILogger _logger;
        private static readonly IServiceProvider _serviceProvider;
        static UnitTestBase()
        {
            var services = new ServiceCollection();
            services.AddGripper();
            services.AddLogging();
            _serviceProvider = services.BuildServiceProvider();
            _logger = _serviceProvider.GetService<ILogger<UnitTestBase>>() ?? throw new NullReferenceException();
        }
        public static T GetService<T>()
        {
            return _serviceProvider.GetService<T>() ?? throw new NullReferenceException();
        }
        public static IWebClient GetWebClient()
        {

        }
    }
}

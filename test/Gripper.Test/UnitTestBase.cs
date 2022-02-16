using Bogus;
using Gripper.WebClient;
using Gripper.WebClient.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Test
{
    public abstract class UnitTestBase
    {
        private static readonly IServiceProvider _serviceProvider;
        private static readonly string _logFileName = Path.GetFullPath("testlog.txt");

        protected static readonly ILogger _logger;

        // For calls that can be tested in parallel.
        protected static readonly IWebClient _commonWebClient;

        protected static T GetRequiredService<T>() => _serviceProvider.GetRequiredService<T>();
        protected static IWebClient CreateGripper() => _serviceProvider.GetRequiredService<IWebClient>();

        static UnitTestBase()
        {
            var services = new ServiceCollection();
            var settingsAction = WebClient.Settings.WebClientSettingsGenerator.GetForUnitTesting();
            settingsAction += x => x.Homepage = Facts.MainTestSite.Path;

            services.AddGripper(settingsAction);

            services.AddLogging(x =>
            {
                x.SetMinimumLevel(LogLevel.Debug)
                .AddFile(_logFileName, LogLevel.Debug);
            });

            _serviceProvider = services.BuildServiceProvider();

            _logger = _serviceProvider.GetRequiredService<ILogger<UnitTestBase>>();
            _commonWebClient = _serviceProvider.GetRequiredService<IWebClient>();
        }
    }
}

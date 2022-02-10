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
        private const string _logFileName = "testlog.txt";

        protected static readonly ILogger _logger;

        // For calls that can be tested in parallel.
        protected static readonly IWebClient _commonWebClient;

        protected static T GetRequiredService<T>() => _serviceProvider.GetRequiredService<T>();

        static UnitTestBase()
        {
            var services = new ServiceCollection();
            var settingsAction = WebClient.Settings.WebClientSettingsGenerator.GetForUnitTesting();
            settingsAction += x => x.Homepage = Facts.GovUkTestSite.Path;

            services.AddGripper(settingsAction);

            services.AddLogging(x =>
            {
                x.SetMinimumLevel(LogLevel.Debug)
                .AddConsole()
                .AddFile(_logFileName, LogLevel.Debug);
            });

            _serviceProvider = services.BuildServiceProvider();

            _logger = _serviceProvider.GetRequiredService<ILogger<UnitTestBase>>();
            _commonWebClient = _serviceProvider.GetRequiredService<IWebClient>();
        }
    }
}

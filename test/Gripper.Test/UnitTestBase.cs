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
        private const string _logFileName = "logs/log_test_fail.txt";

        protected static readonly ILogger _logger;

        // for testing of calls that should not affect each other in parallel runtime.
        protected static readonly IWebClient _commonWebClient;

        protected static T GetService<T>()
        {
            return _serviceProvider.GetService<T>() ?? throw new NullReferenceException();
        }

        static UnitTestBase()
        {
            var services = new ServiceCollection();
            var settingsAction = WebClient.Settings.WebClientSettingsGenerator.GetForUnitTesting();
            settingsAction += x => x.Homepage = Facts.GovUkTestSite.Path;

            services.AddGripper(settingsAction);

            //services.AddGripper(new WebClientSettings
            //{
            //    Homepage = Facts.GovUkTestSite.Path,
            //    BrowserLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
            //    //BrowserLocation = "chrome",
            //    UserDataDir = ".\\UnitTestProfile",
            //    DefaultPageLoadPollSettings = (50, 500),
            //    BrowserStartupArgs = new[] { "--headless", "--disable-gpu", "--window-size=1280,1696", },
            //    //BrowserStartupArgs = Array.Empty<string>(),
            //    BrowserLaunchTimeoutMs = 30_000,


            //    TriggerKeyboardCommandListener = false,
            //    StartupCleanup = BrowserCleanupSettings.None,
            //    UseProxy = false,
            //    RemoteDebuggingPort = 9000,
            //    TargetAttachment = TargetAttachmentMode.Auto,
            //    IgnoreSslCertificateErrors = false,
            //    LaunchBrowser = true
            //});

            services.AddLogging(x =>
            {
                x.SetMinimumLevel(LogLevel.Debug)
                .AddConsole()
                .AddFile(_logFileName, LogLevel.Debug);
            });

            _serviceProvider = services.BuildServiceProvider();

            _logger = _serviceProvider.GetService<ILogger<UnitTestBase>>() ?? throw new NullReferenceException();
            _commonWebClient = _serviceProvider.GetService<IWebClient>() ?? throw new ApplicationException("I need a non-null web client for testing");
        }
    }
}

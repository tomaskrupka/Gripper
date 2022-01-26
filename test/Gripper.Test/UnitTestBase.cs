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
using System.Threading.Tasks;

namespace Gripper.Test
{
    public abstract class UnitTestBase
    {
        private const string _logFileName = "logs/log_test_fail.txt";

        protected static readonly ILogger _logger;
        private static readonly IServiceProvider _serviceProvider;

        private static string GetHomepageAbsolutePath() => Path.GetFullPath("../../../Pages/gov_uk/Welcome_to_GOV.UK.htm");

        protected static readonly IWebClient _webClient;

        protected static T GetService<T>()
        {
            return _serviceProvider.GetService<T>() ?? throw new NullReferenceException();
        }

        static UnitTestBase()
        {
            var services = new ServiceCollection();

            services.AddGripper(new WebClientSettings
            {
                Homepage = GetHomepageAbsolutePath(),
                BrowserLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
                //BrowserLocation = "chrome",
                UserDataDir = ".\\UnitTestProfile",
                DefaultPageLoadPollSettings = PollSettings.ElementDetectionDefault,
                //BrowserStartupArgs = new[] { "--headless", "--disable-gpu", "--window-size=1280,1696", },
                BrowserStartupArgs = Array.Empty<string>(),
                BrowserLaunchTimeoutMs = 30_000,


                TriggerKeyboardCommandListener = false,
                StartupCleanup = BrowserCleanupSettings.None,
                UseProxy = false,
                RemoteDebuggingPort = 9244,
                TargetAttachment = TargetAttachmentMode.Auto,
                IgnoreSslCertificateErrors = false

            }) ;

            services.AddLogging(x =>
            {
                x.SetMinimumLevel(LogLevel.Debug)
                .AddConsole()
                .AddFile(_logFileName, LogLevel.Debug);
            });

            _serviceProvider = services.BuildServiceProvider();
            _logger = _serviceProvider.GetService<ILogger<UnitTestBase>>() ?? throw new NullReferenceException();
            _webClient = _serviceProvider.GetService<IWebClient>() ?? throw new ApplicationException("I need a non-null web client for testing");
        }
    }
}

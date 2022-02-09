using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.WebClient.Settings
{
    /// <summary>
    /// Provides predefined default settings and methods for creating new ones.
    /// </summary>
    public static class WebClientSettingsGenerator
    {
        /// <summary>
        /// Out-of-the-box settings that can be used on Windows to bootstrap a singleton Gripper. Launches own Chrome automatically. Uses http://localhost:9000 for Chrome-Gripper connection.
        /// </summary>
        public static readonly WebClientSettings DefaultSettings = new()
        {
            Homepage = "about:blank",
            BrowserLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
            UserDataDir = ".\\GripperProfile_Default",
            DefaultPageLoadPollSettings = PollSettings.FrameDetectionDefault,
            BrowserStartupArgs = Array.Empty<string>(),
            BrowserLaunchTimeoutMs = 30_000,
            TriggerKeyboardCommandListener = false,
            StartupCleanup = BrowserCleanupSettings.None,
            UseProxy = false,
            RemoteDebuggingPort = 9000,
            TargetAttachment = TargetAttachmentMode.Auto,
            IgnoreSslCertificateErrors = false,
            LaunchBrowser = true
        };


        private static int _lastUsedPort = DefaultSettings.RemoteDebuggingPort;
        private static object _lastUsedPortLock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static WebClientSettings GetForTransient()
        {
            var settingsCopy = new WebClientSettings(DefaultSettings);
            lock (_lastUsedPortLock)
            {
                settingsCopy.RemoteDebuggingPort = _lastUsedPort++;
                if (_lastUsedPort > 9999)
                {
                    _lastUsedPort = 9000;
                }
            }
            settingsCopy.SetRandomUserDataDir();
            return settingsCopy;
        }

        public static WebClientSettings GetForSingleton()
        {
            return new(DefaultSettings);
        }

        /// <summary>
        /// Produces Lambda that overwrites the settings with fresh UserDataDir and Port. For bootstrapping of independent unit tests.
        /// </summary>
        /// <returns></returns>
        public static Action<WebClientSettings> GetForUnitTesting()
        {
            var freshSettings =
                GetForTransient().
                SetForLocalhost().
                SetHeadless().
                SetRandomUserDataDir();

            return x => x.RewriteWith(freshSettings);
        }
    }
}

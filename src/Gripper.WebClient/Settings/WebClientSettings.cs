using System.IO;
using System.Net;

namespace Gripper.WebClient
{
    public class WebClientSettings
    {
        public bool UseProxy;
        public WebProxy Proxy;
        public bool IgnoreSslCertificateErrors;
        public TargetAttachmentMode TargetAttachment;
        public BrowserCleanupSettings StartupCleanup;
        public string UserDataDir;
        public bool TriggerKeyboardCommandListener;
        public string Homepage;
        public PollSettings DefaultPageLoadPollSettings;
        public string BrowserLocation;
        public int RemoteDebuggingPort;
        public string[] BrowserStartupArgs;
        public bool LaunchBrowser;
        public int BrowserLaunchTimeoutMs;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public WebClientSettings()
        {

        }

        /// <summary>
        /// Shallow copy constructor
        /// </summary>
        /// <param name="settings"></param>
        public WebClientSettings(WebClientSettings settings)
        {
            UseProxy = settings.UseProxy;
            Proxy = settings.Proxy;
            IgnoreSslCertificateErrors = settings.IgnoreSslCertificateErrors;
            TargetAttachment = settings.TargetAttachment;
            StartupCleanup = settings.StartupCleanup;
            UserDataDir = settings.UserDataDir;
            TriggerKeyboardCommandListener = settings.TriggerKeyboardCommandListener;
            Homepage = settings.Homepage;
            DefaultPageLoadPollSettings = settings.DefaultPageLoadPollSettings;
            BrowserLocation = settings.BrowserLocation;
            RemoteDebuggingPort = settings.RemoteDebuggingPort;
            BrowserStartupArgs = settings.BrowserStartupArgs;
            LaunchBrowser = settings.LaunchBrowser;
            BrowserLaunchTimeoutMs = settings.BrowserLaunchTimeoutMs;
        }
    }
}

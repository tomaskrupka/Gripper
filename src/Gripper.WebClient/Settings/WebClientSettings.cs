using System.IO;
using System.Net;

namespace Gripper.WebClient
{
    public class WebClientSettings
    {
        public bool? UseProxy;
        public WebProxy? Proxy;
        public bool? IgnoreSslCertificateErrors;
        public TargetAttachmentMode? TargetAttachment;
        public BrowserCleanupSettings? StartupCleanup;
        public string? UserDataDir;
        public bool? TriggerKeyboardCommandListener;
        public string? Homepage;
        public PollSettings? DefaultPageLoadPollSettings;
        public string? BrowserLocation;
        public int? RemoteDebuggingPort;
        public string[]? BrowserStartupArgs;
        public bool? LaunchBrowser;
        public int? BrowserLaunchTimeoutMs;
    }
}

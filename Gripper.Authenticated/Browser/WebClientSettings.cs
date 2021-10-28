using System.IO;
using System.Net;

namespace Gripper.Authenticated.Browser
{
    public struct WebClientSettings
    {
        public bool UseProxy;
        public WebProxy? Proxy;
        public bool IgnoreSslCertificateErrors;
        public WebClientImplementation WebClientImplementation;
        public TargetAttachmentMode TargetAttachment;
        public BrowserCleanupSettings StartupCleanup;
        public DirectoryInfo UserDataDir;
        public bool TriggerKeyboardCommandListener;
        public string Homepage;
        public PollSettings DefaultPageLoadPollSettings;
        public string BrowserLocation;
        public int RemoteDebuggingPort;
    }
}

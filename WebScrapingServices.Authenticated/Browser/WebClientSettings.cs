using System.Net;

namespace WebScrapingServices.Authenticated.Browser
{
    public struct WebClientSettings
    {
        public string UserProfileName;
        public bool UseProxy;
        public WebProxy? Proxy;
        public bool IgnoreSslCertificateErrors;
        public WebClientImplementation WebClientImplementation;
        public TargetAttachment TargetAttachment;
        public bool TriggerKeyboardCommandListener;
        public string Homepage;

        public string BrowserLocation;
        public int RemoteDebuggingPort;
    }
}

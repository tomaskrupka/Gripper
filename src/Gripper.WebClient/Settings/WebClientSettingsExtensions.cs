using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.WebClient.Settings
{
    public static class WebClientSettingsExtensions
    {
        public static WebClientSettings SetForLocalhost(this WebClientSettings webClientSettings)
        {
            webClientSettings.DefaultPageLoadPollSettings = (50, 500);
            webClientSettings.BrowserLaunchTimeoutMs = 10_000;
            return webClientSettings;
        }

        public static WebClientSettings SetRandomUserDataDir(this WebClientSettings webClientSettings)
        {
            var profileGuid = Guid.NewGuid().ToString("N");
            webClientSettings.UserDataDir = $".\\GripperProfile_{profileGuid}";
            return webClientSettings;
        }

        public static WebClientSettings SetHeadless(this WebClientSettings webClientSettings)
        {
            webClientSettings.BrowserStartupArgs = new[]
            {
                "--headless",
                "--disable-gpu",
                "--window-size=1280,1696"
            };
            return webClientSettings;
        }

        public static void RewriteWith(this WebClientSettings oldSettings, WebClientSettings newSettings)
        {
            oldSettings.TriggerKeyboardCommandListener = newSettings.TriggerKeyboardCommandListener;
            oldSettings.UserDataDir = newSettings.UserDataDir;// ?? x.UserDataDir;
            oldSettings.StartupCleanup = newSettings.StartupCleanup;// ?? x.StartupCleanup;
            oldSettings.UseProxy = newSettings.UseProxy;// ?? x.UseProxy;
            oldSettings.Proxy = newSettings.Proxy;// ?? x.Proxy;
            oldSettings.BrowserLocation = newSettings.BrowserLocation;// ?? x.BrowserLocation;
            oldSettings.RemoteDebuggingPort = newSettings.RemoteDebuggingPort;// ?? x.RemoteDebuggingPort;
            oldSettings.Homepage = newSettings.Homepage;// ?? x.Homepage;
            oldSettings.DefaultPageLoadPollSettings = newSettings.DefaultPageLoadPollSettings;// ?? x.DefaultPageLoadPollSettings;
            oldSettings.TargetAttachment = newSettings.TargetAttachment;// ?? x.TargetAttachment;
            oldSettings.BrowserStartupArgs = newSettings.BrowserStartupArgs;// ?? x.BrowserStartupArgs;
            oldSettings.BrowserLaunchTimeoutMs = newSettings.BrowserLaunchTimeoutMs;
            oldSettings.IgnoreSslCertificateErrors = newSettings.IgnoreSslCertificateErrors;
            oldSettings.LaunchBrowser = newSettings.LaunchBrowser;
        }
    }
}

using Gripper.WebClient.Cdtr;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.WebClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // TODO: Extract this to appconfig.json
        private static readonly WebClientSettings _defaultSettings = new()
        {
            TriggerKeyboardCommandListener = true,
            UserDataDir = "C:\\GripperProfiles\\Default",
            StartupCleanup = BrowserCleanupSettings.None,
            UseProxy = false,
            BrowserLocation = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
            RemoteDebuggingPort = 9244,
            Homepage = "https://github.com/tomaskrupka/Gripper",
            DefaultPageLoadPollSettings = PollSettings.FrameDetectionDefault,
            TargetAttachment = TargetAttachmentMode.Auto
        };

        private static IServiceCollection AddSettings(this IServiceCollection services, WebClientSettings webClientSettings)
        {
            services
                .AddOptions<WebClientSettings>()
                .Configure(x =>
                {
                    x.TriggerKeyboardCommandListener = webClientSettings.TriggerKeyboardCommandListener;
                    x.UserDataDir = webClientSettings.UserDataDir;
                    x.StartupCleanup = webClientSettings.StartupCleanup;
                    x.UseProxy = webClientSettings.UseProxy;
                    x.Proxy = webClientSettings.Proxy;
                    x.BrowserLocation = webClientSettings.BrowserLocation;
                    x.RemoteDebuggingPort = webClientSettings.RemoteDebuggingPort;
                    x.Homepage = webClientSettings.Homepage;
                    x.DefaultPageLoadPollSettings = webClientSettings.DefaultPageLoadPollSettings;
                    x.TargetAttachment = webClientSettings.TargetAttachment;
                });
            return services;
        }

        private static IServiceCollection AddDefaultSettings(this IServiceCollection services)
        {
            return services.AddSettings(_defaultSettings);
        }
        public static IServiceCollection AddGripper(this IServiceCollection services)
        {
            return services
                .AddSingleton<ICdtrElementFactory, CdtrElementFactory>()
                .AddSingleton<IWebClient, CdtrChromeClient>()
                .AddDefaultSettings();
        }
        //public static IServiceCollection AddGripper(this IServiceCollection services, IConfiguration namedConfigurationSection)
        //{
        //    services
        //        .AddGripper()
        //        .Configure<WebClientSettings>(namedConfigurationSection);

        //    return services;
        //}
        public static IServiceCollection AddGripper(this IServiceCollection services, Action<WebClientSettings> configureOptions)
        {
            return services
                .AddGripper()
                .Configure(configureOptions);
        }
        public static IServiceCollection AddGripper(this IServiceCollection services, WebClientSettings settings)
        {
            return services
                .AddGripper()
                .AddSettings(settings);
        }
    }
}

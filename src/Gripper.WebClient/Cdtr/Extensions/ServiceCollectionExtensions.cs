using Gripper.WebClient.Cdtr;
using Gripper.WebClient.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Gripper.WebClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static IServiceCollection AddSettings(this IServiceCollection services, WebClientSettings webClientSettings)
        {
            services
                .AddOptions<WebClientSettings>()
                .Configure(x => x.RewriteWith(webClientSettings));

            return services;
        }

        private static IServiceCollection AddDefaultSettings(this IServiceCollection services)
        {
            return services.AddSettings(Settings.WebClientSettingsGenerator.DefaultSettings);
        }
        public static IServiceCollection AddGripper(this IServiceCollection services)
        {
            return services
                .AddTransient<IElementFactory, CdtrElementFactory>()
                .AddTransient<IWebClient, CdtrChromeClient>()
                .AddTransient<IBrowserManager, BrowserManager>()
                .AddTransient<ICdpAdapter, CdpAdapter>()
                .AddTransient<IContextFactory, CdtrContextFactory>()
                .AddTransient<IContextManager, ContextManager>()
                .AddSingleton<IJsBuilder, JsBuilder>()
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

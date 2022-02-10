using Gripper.WebClient.Cdtr;
using Gripper.WebClient.Runtime;
using Gripper.WebClient.Settings;
using Gripper.WebClient.Utils;
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
        /// <summary>
        /// Adds Gripper to service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
        public static IServiceCollection AddGripper(this IServiceCollection services)
        {

            return services
                // Create as many as you want. Each will pull settings, bootstrap its own browser, resolve scoped services and clean up at dispose.
                .AddTransient<IWebClient, CdtrChromeClient>()

                // Don't pull these directly. IWebClient will resolve these within its scope.
                .AddScoped<IElementFactory, CdtrElementFactory>()
                .AddScoped<IBrowserManager, BrowserManager>()
                .AddScoped<ICdpAdapter, CdpAdapter>()
                .AddScoped<IContextFactory, CdtrContextFactory>()
                .AddScoped<IContextManager, ContextManager>()

                // Stateless utils
                .AddSingleton<IJsBuilder, JsBuilder>()

                // Global state containers
                .AddSingleton<IParallelRuntimeUtils, ParallelRuntimeUtils>()
                .AddSingleton<IChildProcessTracker, ChildProcessTracker>()

                // Extensions only overwrite updated members.
                .AddDefaultSettings();
        }

        /// <summary>
        /// Adds Gripper to service collection.
        /// </summary>
        /// <param name="services">Service collection to load gripper into.</param>
        /// <param name="configureOptions">Lambda to run on a <see cref="WebClientSettings"/> instance before it is passed to the <see cref="IWebClient"/> as configuration object.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
        public static IServiceCollection AddGripper(this IServiceCollection services, Action<WebClientSettings> configureOptions)
        {
            return services
                .AddGripper()
                .Configure(configureOptions);
        }


        /// <summary>
        /// Adds Gripper to service collection.
        /// </summary>
        /// <param name="services">Service collection to load gripper into.</param>
        /// <param name="settings">A <see cref="WebClientSettings"/> that is passed to the <see cref="IWebClient"/> as configuration object.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
        public static IServiceCollection AddGripper(this IServiceCollection services, WebClientSettings settings)
        {
            return services
                .AddGripper()
                .AddSettings(settings);
        }
    }
}

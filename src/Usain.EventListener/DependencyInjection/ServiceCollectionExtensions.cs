// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using Usain.EventListener.Configuration;

    /// <summary>
    /// DI extension methods for adding Usain
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IEventListenerBuilder AddUsainEventListener(
            this IServiceCollection services)
        {
            var builder = services.AddUsainEventListenerBuilder();

            builder
                .AddPlatformServices()
                .AddCoreServices()
                .AddDefaultEndpoints();

            return builder;
        }

        public static IEventListenerBuilder AddUsainEventListener(
            this IServiceCollection services,
            Action<
                EventListenerOptions> configureOptions)
        {
            services.Configure(configureOptions);
            return services.AddUsainEventListener();
        }

        public static IEventListenerBuilder AddUsainEventListenerBuilder(
            this IServiceCollection services)
            => new EventListenerBuilder(services);
    }
}

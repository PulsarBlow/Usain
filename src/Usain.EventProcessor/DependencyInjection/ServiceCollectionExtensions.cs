// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IEventProcessorBuilder AddUsainEventProcessor(
            this IServiceCollection services)
        {
            var builder = services.AddUsainEventProcessorBuilder();

            builder
                .AddPlatformServices()
                .AddCoreServices()
                .AddPluggableServices();

            return builder;
        }

        public static IEventProcessorBuilder AddUsainEventProcessorBuilder(
            this IServiceCollection services)
            => new EventProcessorBuilder(services);
    }
}

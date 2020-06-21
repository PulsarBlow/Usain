// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using Extensions;
    using Options;
    using Usain.Core.Infrastructure;
    using Usain.EventProcessor.Configuration;
    using Usain.EventProcessor.EventReactions;
    using Usain.EventProcessor.HostedServices;
    using Usain.Slack.Models;

    public static class EventProcessorBuilderExtensions
    {
        public static IEventProcessorBuilder AddEventQueue<TEventQueue>(
            this IEventProcessorBuilder builder)
            where TEventQueue : class, IEventQueue<EventWrapper>
        {
            builder.Services
                .TryAddSingleton<IEventQueue<EventWrapper>, TEventQueue>();

            return builder;
        }

        public static IEventProcessorBuilder AddEventQueue<TEventQueue>(
            this IEventProcessorBuilder builder,
            Func<IServiceProvider, TEventQueue> implementationFactory)
            where TEventQueue : class, IEventQueue<EventWrapper>
        {
            builder.Services.TryAddSingleton<IEventQueue<EventWrapper>>(
                implementationFactory);

            return builder;
        }

        internal static IEventProcessorBuilder AddPlatformServices(
            this IEventProcessorBuilder builder)
        {
            builder.Services.AddLogging();
            builder.Services.AddOptions();
            builder.Services
                .AddSingleton<IConfigureOptions<EventProcessorOptions>,
                    EventProcessorOptions>();

            return builder;
        }

        // Add core services, they aren't substitutable.
        internal static IEventProcessorBuilder AddCoreServices(
            this IEventProcessorBuilder builder)
        {
            builder.Services
                .AddTransient<IEventReactionGenerator, EventReactionGenerator>();
            builder.Services.AddHostedService<EventProcessorService>();
            return builder;
        }

        // Add pluggable services, they are substitutable.
        internal static IEventProcessorBuilder AddPluggableServices(
            this IEventProcessorBuilder builder)
        {
            // IEventReactionFactory is the interface you will implement for registering your own custom EventReaction.
            builder.Services
                .TryAddEnumerable(ServiceDescriptor.Transient(typeof(IEventReactionFactory<>), typeof(DefaultEventReactionFactory<>)));
            return builder;
        }
    }
}

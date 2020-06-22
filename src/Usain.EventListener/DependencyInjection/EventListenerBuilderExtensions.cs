// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using DependencyInjection;
    using Extensions;
    using Options;
    using Usain.Core.Infrastructure;
    using Usain.EventListener;
    using Usain.EventListener.Commands;
    using Usain.EventListener.Configuration;
    using Usain.EventListener.Infrastructure.Hosting.Endpoints;
    using Usain.EventListener.Infrastructure.Hosting.Middlewares;
    using Usain.EventListener.Infrastructure.Security;
    using Usain.Slack.Models;
    using Endpoint = Usain.EventListener.Infrastructure.Hosting.Endpoints.Endpoint;

    public static class EventListenerBuilderExtensions
    {
        public static IEventListenerBuilder AddEventQueue<TEventQueue>(
            this IEventListenerBuilder builder)
            where TEventQueue : class, IEventQueue<EventWrapper>
        {
            builder.Services
                .TryAddSingleton<IEventQueue<EventWrapper>, TEventQueue>();

            return builder;
        }

        public static IEventListenerBuilder AddEventQueue<TEventQueue>(
            this IEventListenerBuilder builder,
            Func<IServiceProvider, TEventQueue> implementationFactory)
            where TEventQueue : class, IEventQueue<EventWrapper>
        {
            builder.Services.TryAddSingleton<IEventQueue<EventWrapper>>(
                implementationFactory);

            return builder;
        }

        internal static IEventListenerBuilder AddPlatformServices(
            this IEventListenerBuilder builder)
        {
            builder.Services.AddLogging();
            builder.Services.AddOptions();
            builder.Services
                .AddSingleton<IConfigureOptions<EventListenerOptions>,
                    EventListenerOptions>();
            builder.Services.AddMediatR(typeof(ICommandResult));

            return builder;
        }

        internal static IEventListenerBuilder AddCoreServices(
            this IEventListenerBuilder builder)
        {
            // Add core services, they aren't substitutable.
            builder.Services
                .AddSingleton<IRequestAuthenticator, RequestAuthenticator>();
            builder.Services.AddTransient<IEndpointRouter, EndpointRouter>();
            builder.Services.AddScoped<RequestAuthenticationMiddleware>();
            builder.Services.AddScoped<EventListenerMiddleware>();

            return builder;
        }

        internal static IEventListenerBuilder AddPluggableServices(
            this IEventListenerBuilder builder)
        {
            // Add pluggable services, they are substitutable.


            return builder;
        }

        internal static IEventListenerBuilder AddDefaultEndpoints(
            this IEventListenerBuilder builder)
        {
            builder.AddEndpoint<EventsEndpointHandler>(
                Constants.EndpointNames.EventsApi,
                Constants.ProtocolRoutePaths.Events);

            return builder;
        }

        private static IEventListenerBuilder AddEndpoint<TEndpointHandler>(
            this IEventListenerBuilder builder,
            string name,
            PathString path)
            where TEndpointHandler : class, IEndpointHandler
        {
            builder.Services.AddTransient<TEndpointHandler>();
            builder.Services.AddSingleton(
                new Endpoint(
                    name,
                    path,
                    typeof(TEndpointHandler)));

            return builder;
        }
    }
}

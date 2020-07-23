// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Diagnostics;
    using AspNetCore.Builder;
    using Logging;
    using Options;
    using Usain.EventListener.Configuration;
    using Usain.EventListener.Infrastructure.Hosting.Middlewares;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseUsainEventListener(
            this IApplicationBuilder app)
        {
            app.Validate();
            app.UseMiddleware<RequestAuthenticationMiddleware>();
            app.UseMiddleware<EventListenerMiddleware>();
            return app;
        }

        internal static void Validate(
            this IApplicationBuilder app)
        {
            var loggerFactory =
                app.ApplicationServices.GetService(typeof(ILoggerFactory)) as
                    ILoggerFactory
                ?? throw new ArgumentNullException(nameof(ILoggerFactory));

            var logger = loggerFactory.CreateLogger("UsainServer.Startup");
            logger.LogInformation(
                "Starting UsainServer version {version}",
                FileVersionInfo
                    .GetVersionInfo(
                        typeof(EventListenerMiddleware)
                            .Assembly.Location)
                    .ProductVersion);

            var scopeFactory =
                app.ApplicationServices.GetService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var optionsMonitor = serviceProvider
                .GetRequiredService<IOptions<EventListenerOptions>>();
            ValidateOptions(optionsMonitor);
        }

        internal static void ValidateOptions(
            IOptions<EventListenerOptions> serverOptions)
        {
            var options = serverOptions.Value;
            if (options == null)
            {
                throw new InvalidOperationException(
                    "Unable to read options");
            }

            if (options.IsRequestAuthenticationEnabled
                && string.IsNullOrEmpty(options.SigningKey))
            {
                throw new InvalidOperationException(
                    $"Configure {nameof(options.SigningKey)} in UsainServer options or deactivate Request Authentication");
            }
        }
    }
}

namespace Usain.EventListener.Infrastructure.Hosting.Middlewares
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class EventListenerMiddlewareLogger
    {
        private static readonly Action<ILogger, string, string, Exception?>
            InvokingEndpointHandler = LoggerMessage.Define<string, string>(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(InvokingEndpointHandler)),
                "Invoking endpoint handler: {Name} for {Url}.");

        private static readonly Action<ILogger, string, Exception?>
            InvokingEndpointResult = LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(InvokingEndpointResult)),
                "Invoking endpoint result: {Name}.");

        private static readonly Action<ILogger, string, Exception>
            UnhandledException = LoggerMessage.Define<string>(
                LogLevel.Error,
                new EventId(
                    0,
                    nameof(UnhandledException)),
                "Unhandled exception: {Message}");

        public static void LogInvokingEndpointHandler(
            this ILogger logger,
            string name,
            string url)
        {
            InvokingEndpointHandler(
                logger,
                name,
                url,
                null);
        }

        public static void LogInvokingEndpointResult(
            this ILogger logger,
            string name)
        {
            InvokingEndpointResult(
                logger,
                name,
                null);
        }

        public static void LogUnhandledException(
            this ILogger logger,
            Exception ex)
        {
            UnhandledException(
                logger,
                ex.Message,
                ex);
        }
    }
}

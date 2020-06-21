namespace Usain.EventListener.Infrastructure.Logging
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class ServerMiddlewareLogger
    {
        private static readonly Action<ILogger, string, string, Exception?>
            InvokingEndpointHandler = LoggerMessage.Define<string, string>(
                LogLevel.Information,
                new EventId(
                    EventIds.UsainServerMiddleware.InvokingEndpointHandler,
                    nameof(InvokingEndpointHandler)),
                "Invoking endpoint handler: {Name} for {Url}.");

        private static readonly Action<ILogger, string, Exception?>
            InvokingEndpointResult = LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(
                    EventIds.UsainServerMiddleware.InvokingEndpointResult,
                    nameof(InvokingEndpointResult)),
                "Invoking endpoint result: {Name}.");

        private static readonly Action<ILogger, string, Exception>
            UnhandledException = LoggerMessage.Define<string>(
                LogLevel.Error,
                new EventId(
                    EventIds.UsainServerMiddleware.UnhandledException,
                    nameof(UnhandledException)),
                "Unhandled exception: {Message}");

        public static void LogUsainServerMiddlewareInvokingEndpointHandler(
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

        public static void LogUsainServerMiddlewareInvokingEndpointResult(
            this ILogger logger,
            string name)
        {
            InvokingEndpointResult(
                logger,
                name,
                null);
        }

        public static void LogUsainServerMiddlewareUnhandledException(
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
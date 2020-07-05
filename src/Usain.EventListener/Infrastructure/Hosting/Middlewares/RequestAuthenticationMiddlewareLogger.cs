namespace Usain.EventListener.Infrastructure.Hosting.Middlewares
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class RequestAuthenticationMiddlewareLogger
    {
        private static readonly Action<ILogger, Exception?>
            AuthenticationFailed =
                LoggerMessage.Define(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(
                            AuthenticationFailed
                        )),
                    "Request authentication failed. Responding `401 Unauthorized`.");

        private static readonly Action<ILogger, string, Exception?>
            UnhandledException =
                LoggerMessage.Define<string>(
                    LogLevel.Critical,
                    new EventId(
                        0,
                        nameof(
                            UnhandledException
                        )),
                    "Unhandled exception while authenticating request : {Message}.");

        public static void
            LogRequestAuthenticationMiddlewareAuthenticationFailed(
                this ILogger logger)
        {
            AuthenticationFailed(
                logger,
                null);
        }

        public static void
            LogRequestAuthenticationMiddlewareUnhandledException(
                this ILogger logger,
                Exception exception)
        {
            UnhandledException(
                logger,
                exception.Message,
                exception);
        }
    }
}

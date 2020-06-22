namespace Usain.EventListener.Infrastructure.Logging
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
                        EventIds.RequestAuthenticationMiddleware
                            .AuthenticationFailed,
                        nameof(
                            AuthenticationFailed
                        )),
                    "Request authentication failed. Responding `401 Unauthorize`.");

        private static readonly Action<ILogger, string, Exception?>
            UnhandledException =
                LoggerMessage.Define<string>(
                    LogLevel.Critical,
                    new EventId(
                        EventIds.RequestAuthenticationMiddleware
                            .UnhandledException,
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

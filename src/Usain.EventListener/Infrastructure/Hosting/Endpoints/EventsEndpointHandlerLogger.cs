namespace Usain.EventListener.Infrastructure.Hosting.Endpoints
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class EventsEndpointHandlerLogger
    {
        private static readonly Action<ILogger, Exception?> ProcessingEvent =
            LoggerMessage.Define(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(ProcessingEvent)),
                "Processing event request");

        private static readonly Action<ILogger, string, Exception?>
            MethodNotAllowed =
                LoggerMessage.Define<string>(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(MethodNotAllowed)),
                    "Events endpoint only support POST request. Was `{HttpMethod}`");

        private static readonly Action<ILogger, string?, Exception?>
            ProcessingEventOfType = LoggerMessage.Define<string?>(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(ProcessingEventOfType)),
                "Processing event of type `{EventType}`");

        private static readonly Action<ILogger, Exception?>
            JsonDeserializationReturnNull =
                LoggerMessage.Define(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(JsonDeserializationReturnNull)),
                    "Json deserialization returned null. Unprocessable entity is returned.");

        public static void LogProcessingEvent(
            this ILogger logger)
        {
            ProcessingEvent(
                logger,
                null);
        }

        public static void LogMethodNotAllowed(
            this ILogger logger,
            string httpMethod)
        {
            MethodNotAllowed(
                logger,
                httpMethod,
                null);
        }

        public static void LogProcessingEventOfType(
            this ILogger logger,
            string? type)
        {
            ProcessingEventOfType(
                logger,
                type,
                null);
        }

        public static void LogJsonDeserializationReturnNull(
            this ILogger logger)
        {
            JsonDeserializationReturnNull(
                logger,
                null);
        }
    }
}

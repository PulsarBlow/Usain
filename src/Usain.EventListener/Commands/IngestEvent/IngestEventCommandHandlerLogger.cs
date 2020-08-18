namespace Usain.EventListener.Commands.IngestEvent
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class IngestEventCommandHandlerLogger
    {
        private static readonly Action<ILogger, string?, Exception?> IngestingEventOfType
            = LoggerMessage.Define<string?>(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(IngestingEventOfType)),
                "Ingesting event of type `{EventType}`");

        public static void LogIngestingEventOfType(
            this ILogger logger,
            string? eventType)
        {
            IngestingEventOfType(
                logger,
                eventType,
                null);
        }
    }
}

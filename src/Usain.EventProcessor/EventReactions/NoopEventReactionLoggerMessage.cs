namespace Usain.EventProcessor.EventReactions
{
    using System;
    using Microsoft.Extensions.Logging;

    internal static class LoggerMessageExtensions
    {
        private static readonly Action<ILogger, string, Exception?> NoReaction =
            LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(NoReaction)),
                "Noop event reaction for callback event of type `{EventType}`");

        public static void LogNoReaction(
            this ILogger logger,
            string? eventType = null)
        {
            NoReaction(
                logger,
                eventType ?? "unknown type",
                null);
        }
    }
}

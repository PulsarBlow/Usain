namespace Usain.EventProcessor.HostedServices
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class EventQueueProcessorLogger
    {
        private static readonly Action<ILogger, Exception?> ProcessingQueue
            = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(
                    0,
                    nameof(ProcessingQueue)),
                "Queue processor start processing queue");

        private static readonly Action<ILogger, Exception?> ProcessedQueue
            = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(
                    0,
                    nameof(ProcessedQueue)),
                "Queue processor processed queue");

        private static readonly Action<ILogger, string, Exception?> EventHasBeenDequeued
            = LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(EventHasBeenDequeued)),
                "Queue processor has dequeued an event of type `{EventTypeName}`.");

        public static void LogProcessingQueue(
            this ILogger logger)
            => ProcessingQueue(
                logger,
                null);

        public static void LogProcessedQueue(
            this ILogger logger)
            => ProcessedQueue(
                logger,
                null);

        public static void LogEventHasBeenDequeued(
            this ILogger logger,
            string eventTypeName)
            => EventHasBeenDequeued(
                logger,
                eventTypeName,
                null);
    }
}

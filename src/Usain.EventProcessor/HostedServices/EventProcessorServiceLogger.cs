namespace Usain.EventProcessor.HostedServices
{
    using System;
    using Microsoft.Extensions.Logging;

    internal static class EventProcessorServiceLogger
    {
        private static readonly Action<ILogger, string, Exception?>
            ServiceStateChanged = LoggerMessage.Define<string>(
                LogLevel.Debug,
                new EventId(
                    0,
                    nameof(ServiceStateChanged)),
                "{message}");

        private static readonly Action<ILogger, Exception>
            BackgroundWorkFailed = LoggerMessage.Define(
                LogLevel.Critical,
                new EventId(
                    0,
                    nameof(BackgroundWorkFailed)),
                "Background work has failed");

        private static void LogServiceStateChanged(
            this ILogger logger,
            string stateChangedMessage)
            => ServiceStateChanged(
                logger,
                stateChangedMessage,
                null);

        public static void LogServiceIsStarting(
            this ILogger logger)
            => LogServiceStateChanged(
                logger,
                "Service is starting.");

        public static void LogServiceIsStopping(
            this ILogger logger)
            => LogServiceStateChanged(
                logger,
                "Service is stopping.");

        public static void LogServiceIsDoingBackgroundWork(
            this ILogger logger)
            => LogServiceStateChanged(
                logger,
                "Service is doing background work.");



        public static void LogBackgroundWorkHasFailed(
            this ILogger logger,
            Exception exception)
            => BackgroundWorkFailed(
                logger,
                exception);
    }
}

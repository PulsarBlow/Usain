namespace Usain.EventListener.Commands
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class CommandHandlerLogger
    {
        private static readonly Action<ILogger, string, Exception?>
            CommandHandling = LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(CommandHandling)),
                "Handling command : `{Command}`.");

        private static readonly Action<ILogger, string, Exception?>
            CommandCancelling =
                LoggerMessage.Define<string>(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(CommandCancelling)),
                    "Cancelling command : `{Command}`.");

        private static readonly Action<ILogger, string, Exception?>
            CommandFailed = LoggerMessage.Define<string>(
                LogLevel.Warning,
                new EventId(
                    0,
                    nameof(CommandFailed)),
                "Command has failed : `{Command}`.");

        private static readonly Action<ILogger, string, Exception?>
            CommandHandled =
                LoggerMessage.Define<string>(
                    LogLevel.Information,
                    new EventId(
                        0,
                        nameof(CommandHandled)),
                    "Command successfully handled : `{Command}`.");

        public static void LogCommandHandling(
            this ILogger logger,
            string command)
        {
            CommandHandling(
                logger,
                command,
                null);
        }

        public static void LogCommandCancelling(
            this ILogger logger,
            string command)
        {
            CommandCancelling(
                logger,
                command,
                null);
        }

        public static void LogCommandFailed(
            this ILogger logger,
            string command,
            Exception? exception = null)
        {
            CommandFailed(
                logger,
                command,
                exception);
        }

        public static void LogCommandHandled(
            this ILogger logger,
            string command)
        {
            CommandHandled(
                logger,
                command,
                null);
        }
    }
}

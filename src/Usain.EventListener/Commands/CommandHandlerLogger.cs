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
                "Handling command `{CommandName}`.");

        private static readonly Action<ILogger, string, Exception?>
            CommandCancelling =
                LoggerMessage.Define<string>(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(CommandCancelling)),
                    "Cancelling command `{CommandName}`.");

        private static readonly Action<ILogger, string, Exception?>
            CommandHandled =
                LoggerMessage.Define<string>(
                    LogLevel.Information,
                    new EventId(
                        0,
                        nameof(CommandHandled)),
                    "Command `{CommandName}` successfully handled.");

        public static void LogCommandHandling(
            this ILogger logger,
            string commandName)
        {
            CommandHandling(
                logger,
                commandName,
                null);
        }

        public static void LogCommandCancelling(
            this ILogger logger,
            string commandName)
        {
            CommandCancelling(
                logger,
                commandName,
                null);
        }

        public static void LogCommandHandled(
            this ILogger logger,
            string commandName)
        {
            CommandHandled(
                logger,
                commandName,
                null);
        }
    }
}

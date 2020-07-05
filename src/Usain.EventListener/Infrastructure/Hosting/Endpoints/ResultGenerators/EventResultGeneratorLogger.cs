namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.ResultGenerators
{
    using System;
    using Commands;
    using Microsoft.Extensions.Logging;

    public static class EventResultGeneratorLogger
    {
        private static readonly Action<ILogger, string, string, Exception?>
            CommandFailed =
                LoggerMessage.Define<string, string>(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(CommandFailed)),
                    "Command failed [Type={CommandType}, Id={CommandId}] - Returning 422 UnprocessableEntity.");

        private static readonly Action<ILogger, Exception?>
            UrlVerificationEventMissingChallenge =
                LoggerMessage.Define(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(UrlVerificationEventMissingChallenge)),
                    "UrlVerification event has no challenge. Bad request is returned.");

        public static void LogCommandFailed(
            this ILogger logger,
            ICommandResult commandResult)
        {
            CommandFailed(
                logger,
                commandResult.GetType()
                    .Name,
                commandResult.CommandId.ToString(),
                null);
        }

        public static void LogUrlVerificationEventMissingChallenge(
            this ILogger logger)
        {
            UrlVerificationEventMissingChallenge(
                logger,
                null);
        }
    }
}

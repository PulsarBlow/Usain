namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.ResultGenerators
{
    using System;
    using Commands;
    using Microsoft.Extensions.Logging;

    internal static class EventResultGeneratorLogger
    {
        private static readonly Action<ILogger, string, Exception?>
            UnsuccessfulCommandResult =
                LoggerMessage.Define<string>(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(UnsuccessfulCommandResult)),
                    "Unsuccessful command result `{CommandResult}` - Generating 422 Unprocessable Entity.");

        private static readonly Action<ILogger, Exception?>
            UrlVerificationEventMissingChallenge =
                LoggerMessage.Define(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(UrlVerificationEventMissingChallenge)),
                    "UrlVerification event has no challenge. Generating 400 Bad request.");

        public static void LogUnsuccessfulCommandResult(
            this ILogger logger,
            CommandResult commandResult)
        {
            UnsuccessfulCommandResult(
                logger,
                commandResult.ToString(),
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

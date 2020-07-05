namespace Usain.EventListener.Infrastructure.Security
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class RequestAuthenticatorLogger
    {
        private static readonly Action<ILogger, Exception?>
            RequestAuthenticationIsDisabled =
                LoggerMessage.Define(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(
                            RequestAuthenticationIsDisabled
                        )),
                    "Request authentication is disabled in options. Skipping request authentication.");

        private static readonly
            Action<ILogger, string, string, string, Exception?>
            InvokingSignatureVerification =
                LoggerMessage.Define<string, string, string>(
                    LogLevel.Debug,
                    new EventId(
                        0,
                        nameof(InvokingSignatureVerification)),
                    "Invoking SignatureVerifier: timestamp={Timestamp}, signature={Signature}, message={Message}");

        public static void
            LogRequestAuthenticatorRequestAuthenticationIsDisabled(
                this ILogger logger)
        {
            RequestAuthenticationIsDisabled(
                logger,
                null);
        }

        public static void LogInvokingSignatureVerification(
            this ILogger logger,
            long timestamp,
            string signature,
            string message)
        {
            InvokingSignatureVerification(
                logger,
                timestamp.ToString(),
                signature,
                message,
                null);
        }
    }
}

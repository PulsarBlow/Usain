namespace Usain.EventListener.Infrastructure.Security
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Configuration;
    using Extensions;
    using Logging;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Slack.Security;

    internal class RequestAuthenticator : IRequestAuthenticator
    {
        private const string TimestampHeaderName = "X-Slack-Request-Timestamp";
        private const string SignatureHeaderName = "X-Slack-Signature";

        private readonly ILogger _logger;
        private readonly IOptionsMonitor<EventListenerOptions>
            _optionsMonitorMonitor;

        public RequestAuthenticator(
            ILogger<RequestAuthenticator> logger,
            IOptionsMonitor<EventListenerOptions> optionsMonitor)
        {
            _logger = logger;
            _optionsMonitorMonitor = optionsMonitor;
        }

        public async Task<bool> IsAuthenticAsync(HttpRequest request, CancellationToken cancellationToken)
        {
            var options = _optionsMonitorMonitor.CurrentValue;
            if (!options.IsRequestAuthenticationEnabled)
            {
                _logger
                    .LogRequestAuthenticatorRequestAuthenticationIsDisabled();
                return true;
            }

            var timestamp = GetTimestamp(request);
            var signature = GetSignature(request);
            var message = await request.ReadAsync(cancellationToken) ?? string.Empty;

            _logger.LogInvokingSignatureVerification(
                timestamp,
                signature,
                message);
            var verifier = new SignatureVerifier(
                options.SigningKey,
                TimeSpan.FromSeconds(options.DeltaTimeToleranceSeconds));

            return verifier.Verify(signature, timestamp, message);
        }

        private static long GetTimestamp(HttpRequest request)
        {
            request.Headers.TryGetValue(TimestampHeaderName,
                out var timestampValues);
            long.TryParse(timestampValues.ToString(), out var timestamp);
            return timestamp;
        }

        private static string GetSignature(HttpRequest request)
        {
            request.Headers.TryGetValue(SignatureHeaderName,
                out var signatureValues);
            return signatureValues.ToString();
        }
    }
}

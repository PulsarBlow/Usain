namespace Usain.EventListener.Infrastructure.Security
{
    using System;
    using System.Threading.Tasks;
    using Configuration;
    using Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Slack.Security;

    internal class RequestAuthenticator : IRequestAuthenticator
    {
        private readonly ILogger _logger;
        private readonly IOptionsMonitor<EventListenerOptions>
            _optionsMonitorMonitor;
        private readonly ISignatureVerifier _signatureVerifier;

        public RequestAuthenticator(
            ILogger<RequestAuthenticator> logger,
            ISignatureVerifier signatureVerifier,
            IOptionsMonitor<EventListenerOptions> optionsMonitor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _signatureVerifier = signatureVerifier
                ?? throw new ArgumentNullException(nameof(signatureVerifier));
            _optionsMonitorMonitor = optionsMonitor
                ?? throw new ArgumentNullException(nameof(optionsMonitor));
        }

        public async Task<bool> IsAuthenticAsync(
            HttpRequest request)
        {
            var options = _optionsMonitorMonitor.CurrentValue;
            if (!options.IsRequestAuthenticationEnabled)
            {
                _logger
                    .LogRequestAuthenticatorRequestAuthenticationIsDisabled();
                return true;
            }

            var timestamp = request.GetSlackTimestampHeaderValue();
            var signature = request.GetSlackSignatureHeaderValue();
            var message = await request.ReadAsync() ?? string.Empty;

            _logger.LogInvokingSignatureVerification(
                timestamp,
                signature,
                message);

            return _signatureVerifier.Verify(
                signature,
                timestamp,
                message);
        }
    }
}

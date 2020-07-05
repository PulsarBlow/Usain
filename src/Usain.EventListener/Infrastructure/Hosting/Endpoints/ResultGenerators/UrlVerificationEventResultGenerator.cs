namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.ResultGenerators
{
    using System.Threading;
    using System.Threading.Tasks;
    using Commands.VerifyUrl;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Results;
    using Results.Responses;
    using Slack.Models;

    internal class UrlVerificationEventResultGenerator
        : IEventsEndpointResultGenerator<UrlVerificationEvent>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public UrlVerificationEventResultGenerator(
            ILogger<UrlVerificationEventResultGenerator> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IEndpointResult> GenerateResult(
            UrlVerificationEvent @event,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(@event.Challenge))
            {
                _logger.LogUrlVerificationEventMissingChallenge();
                return new StatusCodeEndpointResult(
                    StatusCodes.Status400BadRequest);
            }

            var commandResult =
                await _mediator.Send(
                    new VerifyUrlCommand(@event.Challenge),
                    cancellationToken);
            if (!commandResult.IsSuccess)
            {
                _logger.LogCommandFailed(commandResult);
                return new StatusCodeEndpointResult(
                    StatusCodes.Status422UnprocessableEntity);
            }

            return new OkEndpointResult<UrlVerificationResponse>(
                new UrlVerificationResponse(commandResult.Challenge));
        }
    }
}

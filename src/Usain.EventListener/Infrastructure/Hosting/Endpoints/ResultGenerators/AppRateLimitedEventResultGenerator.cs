namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.ResultGenerators
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands.AcknowledgeAppRateLimit;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Results;
    using Slack.Models.Events;

    internal class AppRateLimitedEventResultGenerator
        : IEventsEndpointResultGenerator<AppRateLimitedEvent>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public AppRateLimitedEventResultGenerator(
            ILogger<AppRateLimitedEventResultGenerator> logger,
            IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator
                ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<IEndpointResult> GenerateResult(
            AppRateLimitedEvent @event,
            CancellationToken cancellationToken)
        {
            var commandResult =
                await _mediator.Send(
                    new AcknowledgeAppRateLimitCommand(),
                    cancellationToken);
            if (!commandResult.IsSuccess)
            {
                _logger.LogUnsuccessfulCommandResult(commandResult);
                return new StatusCodeEndpointResult(
                    StatusCodes.Status422UnprocessableEntity);
            }

            return new OkEndpointResult();
        }
    }
}

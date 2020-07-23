namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.ResultGenerators
{
    using System.Threading;
    using System.Threading.Tasks;
    using Commands.IngestEvent;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Results;
    using Slack.Models;

    internal class CallbackEventResultGenerator
        : IEventsEndpointResultGenerator<EventWrapper>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public CallbackEventResultGenerator(
            ILogger<CallbackEventResultGenerator> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IEndpointResult> GenerateResult(
            EventWrapper @event,
            CancellationToken cancellationToken)
        {
            var commandResult =
                await _mediator.Send(
                    new IngestEventCommand(@event),
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

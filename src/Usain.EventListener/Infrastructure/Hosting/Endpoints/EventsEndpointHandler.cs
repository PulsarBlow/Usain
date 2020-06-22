namespace Usain.EventListener.Infrastructure.Hosting.Endpoints
{
    using System.Threading;
    using System.Threading.Tasks;
    using Commands.AcknowledgeAppRateLimit;
    using Commands.IngestEvent;
    using Commands.VerifyUrl;
    using Extensions;
    using Logging;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Results;
    using Slack.Models;

    public class EventsEndpointHandler : IEndpointHandler
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public EventsEndpointHandler(
            ILogger<EventsEndpointHandler> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IEndpointResult> ProcessAsync(
            HttpContext context,
            CancellationToken cancellationToken)
        {
            _logger.LogProcessingEvent();

            if (!HttpMethods.IsPost(context.Request.Method))
            {
                _logger.LogMethodNotAllowed(context.Request.Method);
                return new StatusCodeResult(
                    StatusCodes
                        .Status405MethodNotAllowed);
            }

            var incomingEvent =
                await context.Request.ReadJsonAsync<Event>(cancellationToken)!;
            if (incomingEvent == null)
            {
                _logger.LogJsonDeserializationReturnNull();
                return new StatusCodeResult(
                    StatusCodes.Status422UnprocessableEntity);
            }

            return incomingEvent switch
            {
                UrlVerificationEvent @event =>
                await ProcessUrlVerificationEventAsync(
                    @event,
                    cancellationToken),
                AppRateLimitedEvent @event =>
                await ProcessAppRateLimitedEventAsync(
                    @event,
                    cancellationToken),
                EventWrapper @event => await ProcessCallbackEventAsync(
                    @event,
                    cancellationToken),
                _ => new StatusCodeResult(
                    StatusCodes.Status422UnprocessableEntity),
            };
        }

        private async Task<IEndpointResult> ProcessUrlVerificationEventAsync(
            UrlVerificationEvent @event,
            CancellationToken cancellationToken)
        {
            var commandResult = await _mediator.Send(
                new VerifyUrlCommand
                {
                    Challenge = @event.Challenge
                },
                cancellationToken);
            if (!commandResult.IsSuccess)
            {
                return new StatusCodeResult(
                    StatusCodes.Status422UnprocessableEntity);
            }

            return new OkResult(new { challenge = commandResult.Challenge });
        }

        private async Task<IEndpointResult> ProcessAppRateLimitedEventAsync(
            AppRateLimitedEvent @event,
            CancellationToken cancellationToken)
        {
            var commandResult =
                await _mediator.Send(
                    new AcknowledgeAppRateLimitCommand(@event),
                    cancellationToken);
            if (!commandResult.IsSuccess)
            {
                return new StatusCodeResult(
                    StatusCodes.Status422UnprocessableEntity);
            }

            return new OkResult();
        }

        private async Task<IEndpointResult> ProcessCallbackEventAsync(
            EventWrapper @event,
            CancellationToken cancellationToken)
        {
            var commandResult =
                await _mediator.Send(
                    new IngestEventCommand(@event),
                    cancellationToken);
            return new EventsEndpointResult(commandResult);
        }
    }
}

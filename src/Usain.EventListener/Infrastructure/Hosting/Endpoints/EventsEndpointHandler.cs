namespace Usain.EventListener.Infrastructure.Hosting.Endpoints
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using ResultGenerators;
    using Results;
    using Slack.Models.Events;

    internal class EventsEndpointHandler : IEndpointHandler
    {
        private readonly ILogger _logger;
        private readonly IEventsEndpointResultGenerator<UrlVerificationEvent>
            _urlVerificationEventResultGenerator;
        private readonly IEventsEndpointResultGenerator<AppRateLimitedEvent>
            _appRateLimitedEventResultGenerator;
        private readonly IEventsEndpointResultGenerator<EventWrapper>
            _callbackEventResultGenerator;

        public const string ProtocolRoutePath = "/events";
        public const string EndpointName = "EventsApi";

        public EventsEndpointHandler(
            ILogger<EventsEndpointHandler> logger,
            IEventsEndpointResultGenerator<UrlVerificationEvent>
                urlVerificationEventResultGenerator,
            IEventsEndpointResultGenerator<AppRateLimitedEvent>
                appRateLimitedEventResultGenerator,
            IEventsEndpointResultGenerator<EventWrapper>
                callbackEventResultGenerator)
        {
            _logger = logger;
            _urlVerificationEventResultGenerator =
                urlVerificationEventResultGenerator
                ?? throw new ArgumentNullException(
                    nameof(_urlVerificationEventResultGenerator));
            _appRateLimitedEventResultGenerator =
                appRateLimitedEventResultGenerator
                ?? throw new ArgumentNullException(
                    nameof(_appRateLimitedEventResultGenerator));
            _callbackEventResultGenerator = callbackEventResultGenerator
                ?? throw new ArgumentNullException(
                    nameof(_callbackEventResultGenerator));
        }

        public async Task<IEndpointResult> ProcessAsync(
            HttpContext context,
            CancellationToken cancellationToken)
        {
            _logger.LogProcessingEvent();

            if (!HttpMethods.IsPost(context.Request.Method))
            {
                _logger.LogMethodNotAllowed(context.Request.Method);
                return new StatusCodeEndpointResult(
                    StatusCodes
                        .Status405MethodNotAllowed);
            }

            var incomingEvent =
                await context.Request.ReadJsonAsync<Event>()!;
            if (incomingEvent == null)
            {
                _logger.LogJsonDeserializationReturnNull();
                return new StatusCodeEndpointResult(
                    StatusCodes.Status422UnprocessableEntity);
            }

            _logger.LogProcessingEventOfType(incomingEvent.EventType);

            return incomingEvent switch
            {
                UrlVerificationEvent @event =>
                await _urlVerificationEventResultGenerator.GenerateResult(
                    @event,
                    cancellationToken),
                AppRateLimitedEvent @event =>
                await _appRateLimitedEventResultGenerator.GenerateResult(
                    @event,
                    cancellationToken),
                EventWrapper @event => await _callbackEventResultGenerator
                    .GenerateResult(
                        @event,
                        cancellationToken),
                _ => new StatusCodeEndpointResult(
                    StatusCodes.Status422UnprocessableEntity),
            };
        }
    }
}

namespace Usain.EventProcessor.HostedServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using EventReactions;
    using Microsoft.Extensions.Logging;
    using Slack.Models;

    internal class EventQueueProcessor : IEventQueueProcessor
    {
        private readonly ILogger _logger;
        private readonly IEventQueue<EventWrapper> _eventQueue;
        private readonly IEventReactionGenerator _eventReactionGenerator;

        public EventQueueProcessor(
            ILogger<EventQueueProcessor> logger,
            IEventQueue<EventWrapper> eventQueue,
            IEventReactionGenerator eventReactionGenerator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventQueue = eventQueue
                ?? throw new ArgumentNullException(nameof(eventQueue));
            _eventReactionGenerator = eventReactionGenerator
                ?? throw new ArgumentNullException(
                    nameof(eventReactionGenerator));
        }

        public async Task ProcessQueueAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogProcessingQueue();
            if (await _eventQueue.TryDequeueAsync(
                out var @event,
                cancellationToken))
            {
                _logger.LogEventHasBeenDequeued(@event?.Type ?? string.Empty);
                await ReactToEventAsync(@event);
            }

            _logger.LogProcessedQueue();
        }

        private async Task ReactToEventAsync(
            EventWrapper? eventWrapper)
        {
            if (eventWrapper?.Event == null) { return; }

            var reaction = _eventReactionGenerator.Generate(eventWrapper);
            await reaction.ReactAsync();
        }
    }
}

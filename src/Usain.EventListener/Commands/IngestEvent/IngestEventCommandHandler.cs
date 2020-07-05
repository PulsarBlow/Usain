namespace Usain.EventListener.Commands.IngestEvent
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Microsoft.Extensions.Logging;
    using Slack.Models;

    internal class IngestEventCommandHandler
        : ICommandHandler<IngestEventCommand, IngestEventCommandResult>
    {
        private readonly ILogger _logger;
        private readonly IEventQueue<EventWrapper> _eventQueue;

        public IngestEventCommandHandler(
            ILogger<IngestEventCommandHandler> logger,
            IEventQueue<EventWrapper> eventQueue)
        {
            _logger = logger;
            _eventQueue = eventQueue;
        }

        public async Task<IngestEventCommandResult> Handle(
            IngestEventCommand command,
            CancellationToken cancellationToken)
        {
            _logger.LogCommandHandling(command.ToString());
            if (cancellationToken
                .IsCancellationRequested)
            {
                _logger.LogCommandCancelling(command.ToString());
                return new IngestEventCommandResult(
                    Guid.Empty,
                    CommandResultType.Aborted);
            }

            var @event = command.Event;
            await _eventQueue.EnqueueAsync(
                @event,
                cancellationToken);
            _logger.LogCommandHandled(command.ToString());

            return new IngestEventCommandResult(@event.InternalId);
        }
    }
}

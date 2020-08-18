namespace Usain.EventListener.Commands.IngestEvent
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Microsoft.Extensions.Logging;
    using Slack.Models.Events;

    internal class IngestEventCommandHandler
        : ICommandHandler<IngestEventCommand, CommandResult>
    {
        private readonly ILogger _logger;
        private readonly IEventQueue<EventWrapper> _eventQueue;

        public IngestEventCommandHandler(
            ILogger<IngestEventCommandHandler> logger,
            IEventQueue<EventWrapper> eventQueue)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventQueue = eventQueue
                ?? throw new ArgumentNullException(nameof(eventQueue));
        }

        public async Task<CommandResult> Handle(
            IngestEventCommand command,
            CancellationToken cancellationToken)
        {
            _logger.LogCommandHandling(command.ToString());
            if (cancellationToken
                .IsCancellationRequested)
            {
                _logger.LogCommandCancelling(command.ToString());
                return new CommandResult(
                    command.Id,
                    CommandResultType.Aborted);
            }

            var @event = command.Event;
            _logger.LogIngestingEventOfType(@event.EventType);
            try
            {
                await _eventQueue.EnqueueAsync(
                    @event,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogCommandFailed(
                    command.ToString(),
                    ex);
                return new CommandResult(
                    command.Id,
                    CommandResultType.Failure);
            }

            _logger.LogCommandHandled(command.ToString());
            return new CommandResult(command.Id);
        }
    }
}

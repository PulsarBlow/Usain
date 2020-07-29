namespace Usain.EventProcessor.EventReactions
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Slack.Models.Events;
    using Slack.Models.Events.CallbackEvents;

    internal class NoopEventReaction<TCallbackEvent>
        : IEventReaction<
            TCallbackEvent>
        where TCallbackEvent : CallbackEvent, new()
    {
        private readonly ILogger _logger;
        private readonly EventWrapper _eventWrapper;

        public TCallbackEvent Event { get; }

        public NoopEventReaction(
            ILogger<NoopEventReaction<TCallbackEvent>> logger,
            EventWrapper eventWrapper)
        {
            _logger = logger;
            _eventWrapper = eventWrapper;
            Event = _eventWrapper.Event as TCallbackEvent
                ?? new TCallbackEvent();
        }

        public Task ReactAsync()
        {
            string eventType = _eventWrapper.Type ?? "unknown_type";
            _logger.LogNoReaction(eventType);
            return Task.CompletedTask;
        }
    }
}

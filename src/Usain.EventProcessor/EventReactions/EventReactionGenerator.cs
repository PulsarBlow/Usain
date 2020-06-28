namespace Usain.EventProcessor.EventReactions
{
    using System;
    using Slack.Models;
    using Slack.Models.CallbackEvents;

    internal sealed class EventReactionGenerator
        : IEventReactionGenerator
    {
        private readonly IEventReactionFactory<CallbackEvent>
            _noopEventReactionFactory;
        private readonly IEventReactionFactory<AppMentionEvent>
            _appMentionEventReactionFactory;

        public EventReactionGenerator(
            IEventReactionFactory<CallbackEvent> noopEventReactionFactory,
            IEventReactionFactory<AppMentionEvent>
                appMentionEventReactionFactory)
        {
            _noopEventReactionFactory = noopEventReactionFactory
                ?? throw new ArgumentNullException(
                    nameof(noopEventReactionFactory));
            _appMentionEventReactionFactory = appMentionEventReactionFactory
                ?? throw new ArgumentNullException(
                    nameof(appMentionEventReactionFactory));
        }

        public IEventReaction Generate(
            EventWrapper eventWrapper)
        {
            if (eventWrapper.Event == null)
            {
                throw new InvalidOperationException(
                    "EventWrapper does not hold any callback event");
            }

            return eventWrapper.Event switch
            {
                AppMentionEvent _ => _appMentionEventReactionFactory.Create(
                    eventWrapper),
                _ => _noopEventReactionFactory.Create(eventWrapper),
            };
        }
    }
}

namespace Usain.EventProcessor.EventReactions
{
    using System;
    using Microsoft.Extensions.Logging;
    using Slack.Models;
    using Slack.Models.CallbackEvents;

    internal sealed class EventReactionGenerator
        : IEventReactionGenerator
    {
        private readonly ILoggerFactory _loggerFactory;

        private readonly IEventReactionFactory<AppMentionEvent>
            _appMentionEventReactionFactory;

        public EventReactionGenerator(
            ILoggerFactory loggerFactory,
            IEventReactionFactory<AppMentionEvent>
                appMentionEventReactionFactory)
        {
            _loggerFactory = loggerFactory;
            _appMentionEventReactionFactory = appMentionEventReactionFactory;
        }

        public IEventReaction Generate(
            EventWrapper eventWrapper)
        {
            if (eventWrapper.Event == null)
            {
                throw new InvalidOperationException("Callback event is null");
            }

            return eventWrapper.Event switch
            {
                AppMentionEvent _ => _appMentionEventReactionFactory.Create(
                    eventWrapper),
                _ => new NoopEventReaction(
                    _loggerFactory.CreateLogger<NoopEventReaction>(),
                    eventWrapper),
            };
        }
    }
}

namespace Usain.EventProcessor.EventReactions
{
    using System;
    using Microsoft.Extensions.Logging;
    using Slack.Models;
    using Slack.Models.CallbackEvents;

    public class DefaultEventReactionFactory<TCallbackEvent>
        : IEventReactionFactory<TCallbackEvent>
        where TCallbackEvent : CallbackEvent, new()
    {
        private readonly ILogger<NoopEventReaction<TCallbackEvent>> _logger;

        public DefaultEventReactionFactory(
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory
                    .CreateLogger<NoopEventReaction<TCallbackEvent>>()
                ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public IEventReaction<TCallbackEvent> Create(
            EventWrapper eventWrapper)
        {
            return new NoopEventReaction<TCallbackEvent>(
                _logger,
                eventWrapper);
        }
    }
}

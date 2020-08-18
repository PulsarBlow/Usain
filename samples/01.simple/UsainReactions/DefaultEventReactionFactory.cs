namespace Usain.Samples.Simple.UsainReactions
{
    using EventProcessor.EventReactions;
    using global::Slack.NetStandard;
    using Microsoft.Extensions.Logging;
    using Slack.Models.Events;
    using Slack.Models.Events.CallbackEvents;

    public class DefaultEventReactionFactory<TEvent>
        : IEventReactionFactory<TEvent>
        where TEvent : CallbackEvent, new()
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ISlackApiClient _slackApiClient;

        public DefaultEventReactionFactory(
            ILoggerFactory loggerFactory,
            ISlackApiClient slackApiClient)
        {
            _loggerFactory = loggerFactory;
            _slackApiClient = slackApiClient;
        }

        public IEventReaction<TEvent> Create(
            EventWrapper eventWrapper)
        {
            var reaction =
                new EventReaction<TEvent>(
                    _loggerFactory.CreateLogger
                        <EventReaction<TEvent>>(),
                    _slackApiClient,
                    eventWrapper);
            return reaction;
        }
    }
}

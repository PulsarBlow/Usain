namespace Usain.Samples.Simple.UsainReactions
{
    using global::Slack.NetStandard;
    using Microsoft.Extensions.Logging;
    using Usain.EventProcessor.EventReactions;
    using Usain.Slack.Models;
    using Usain.Slack.Models.CallbackEvents;

    public class CustomReactionFactory<TEvent> : IEventReactionFactory<TEvent>
        where TEvent : CallbackEvent
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ISlackApiClient _slackApiClient;

        public CustomReactionFactory(
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

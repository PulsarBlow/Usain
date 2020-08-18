namespace Usain.Samples.Simple.UsainReactions
{
    using System;
    using EventProcessor.EventReactions;
    using global::Slack.NetStandard;
    using Microsoft.Extensions.Logging;
    using Slack.Models.Events;
    using Slack.Models.Events.CallbackEvents;

    public class AppMentionEventReactionFactory
        : IEventReactionFactory<AppMentionEvent>
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ISlackApiClient _slackApiClient;

        public AppMentionEventReactionFactory(
            ILoggerFactory loggerFactory,
            ISlackApiClient slackApiClient)
        {
            _loggerFactory = loggerFactory;
            _slackApiClient = slackApiClient;
        }

        public IEventReaction<AppMentionEvent> Create(
            EventWrapper eventWrapper)
        {
            if (eventWrapper?.Event is AppMentionEvent)
            {
                return new AppMentionEventReaction(
                    _loggerFactory
                        .CreateLogger<EventReaction<AppMentionEvent>>(),
                    _slackApiClient,
                    eventWrapper);
            }

            throw new InvalidOperationException(
                "EventWrapper has no AppMentionEvent");
        }
    }
}

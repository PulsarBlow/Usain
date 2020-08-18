namespace Usain.Samples.Simple.UsainReactions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventProcessor.EventReactions;
    using global::Slack.NetStandard;
    using global::Slack.NetStandard.Messages.Blocks;
    using global::Slack.NetStandard.WebApi.Chat;
    using Microsoft.Extensions.Logging;
    using Slack.Models.Events;
    using Slack.Models.Events.CallbackEvents;

    public class EventReaction<TEvent> : IEventReaction<TEvent>
        where TEvent : CallbackEvent
    {
        private readonly ISlackApiClient _apiClient;

        protected readonly ILogger<EventReaction<TEvent>> Logger;
        protected readonly EventWrapper EventWrapper;

        public TEvent Event { get; }

        public EventReaction(
            ILogger<EventReaction<TEvent>> logger,
            ISlackApiClient apiClient,
            EventWrapper eventWrapper)
        {
            Logger = logger;
            _apiClient = apiClient;
            EventWrapper = eventWrapper;
            Event = eventWrapper.Event as TEvent;
        }

        public async Task ReactAsync()
        {
            if (EventWrapper.Event is AppMentionEvent appMentionEvent)
            {
                var message = CreatePostMessageRequest(appMentionEvent);
                await _apiClient.Chat.Post(message);
            }

            DefaultEventReaction();
        }

        private void DefaultEventReaction()
        {
            Logger.LogInformation(
                "Noop reaction for event `{EventType}`",
                EventWrapper.Event?.CallbackEventType);
            if (EventWrapper.Event is MessageEvent messageEvent)
            {
                Logger.LogInformation(
                    "Message event subtype is `{EventSubType}`",
                    messageEvent.MessageSubType);
            }
        }

        protected PostMessageRequest CreateDefaultMessage()
            => new PostMessageRequest
            {
                Text = "Usain Reaction : <null>",
            };

        protected virtual PostMessageRequest CreatePostMessageRequest(
            IChannelEvent channelEvent)
        {
            if (EventWrapper?.Event == null) { return CreateDefaultMessage(); }

            var @event = EventWrapper.Event;
            var titleSection = new Section(new MarkdownText("Usain Reaction"))
            {
                Fields = new List<TextObject>
                {
                    new MarkdownText($"*Type:*\n{@event.CallbackEventType}"),
                    new MarkdownText(
                        $"*When:*\n{DateTimeOffset.FromUnixTimeSeconds(@event.EventTimestamp.Seconds)}"),
                },
            };

            return new PostMessageRequest
            {
                Channel = channelEvent.ChannelId,
                Blocks = new List<IMessageBlock>
                {
                    titleSection,
                },
            };
        }
    }
}

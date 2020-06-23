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
    using Slack.Models;
    using Slack.Models.CallbackEvents;

    public class EventReaction<TEvent> : IEventReaction<TEvent>
        where TEvent : CallbackEvent
    {
        private readonly ISlackApiClient _apiClient;
        protected readonly ILogger<EventReaction<TEvent>> _logger;

        protected readonly EventWrapper _eventWrapper;

        public TEvent Event { get; }

        public EventReaction(
            ILogger<EventReaction<TEvent>> logger,
            ISlackApiClient apiClient,
            EventWrapper eventWrapper)
        {
            _logger = logger;
            _apiClient = apiClient;
            _eventWrapper = eventWrapper;
            Event = eventWrapper.Event as TEvent;
        }

        public async Task ReactAsync()
        {
            if (_eventWrapper.Event is IChannelEvent channelEvent)
            {
                var message = CreatePostMessageRequest(channelEvent);
                await _apiClient.Chat.Post(message);
            }

            DefaultEventReaction();
        }

        private void DefaultEventReaction()
        {
            _logger.LogInformation(
                "Noop reaction for event `{EventType}`",
                _eventWrapper.Event?.Type);
        }

        protected PostMessageRequest CreateDefaultMessage()
        {
            return new PostMessageRequest
            {
                Text = "Usain Reaction : <null>",
            };
        }

        protected virtual PostMessageRequest CreatePostMessageRequest(IChannelEvent channelEvent)
        {
            if (_eventWrapper?.Event == null)
            {
                return CreateDefaultMessage();
            }

            var @event = _eventWrapper.Event;
            var titleSection = new Section(new MarkdownText("Usain Reaction"))
            {
                Fields = new List<TextObject>
                {
                    new MarkdownText($"*Type:*\n{@event.Type}"),
                    new MarkdownText(
                        $"*When:*\n{DateTimeOffset.FromUnixTimeSeconds(@event.EventTimestamp.Timestamp)}"),
                }
            };

            return new PostMessageRequest
            {
                Channel = channelEvent.Channel,
                Blocks = new List<IMessageBlock>
                {
                    titleSection
                }
            };
        }
    }
}

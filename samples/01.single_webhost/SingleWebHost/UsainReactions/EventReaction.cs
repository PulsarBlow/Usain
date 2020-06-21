namespace SingleWebHost.UsainReactions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::Slack.NetStandard;
    using global::Slack.NetStandard.Messages.Blocks;
    using global::Slack.NetStandard.WebApi.Chat;
    using Microsoft.Extensions.Logging;
    using Usain.EventProcessor.EventReactions;
    using Usain.Slack.Models;
    using Usain.Slack.Models.CallbackEvents;

    public class EventReaction<TEvent> : IEventReaction<TEvent>
        where TEvent : CallbackEvent
    {
        private readonly ILogger<EventReaction<TEvent>> _logger;
        private readonly ISlackApiClient _apiClient;
        private readonly EventWrapper _eventWrapper;

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
            if (_eventWrapper.Event is AppMentionEvent @event)
            {
                await AppMentionEventReaction(@event);
            }

            DefaultEventReaction(_eventWrapper);
        }

        private async Task AppMentionEventReaction(
            AppMentionEvent appMentionEvent)
        {
            var titleSection = new Section(new MarkdownText("Usain Reaction"))
            {
                Fields = new List<TextObject>
                {
                    new MarkdownText($"*Type:*\n{appMentionEvent.Type}"),
                    new MarkdownText(
                        $"*When:*\n{DateTimeOffset.FromUnixTimeSeconds(appMentionEvent.Timestamp.Timestamp)}"),
                    new MarkdownText(
                        $"*Text:*\n{appMentionEvent.Text}"),
                    new MarkdownText($"*User:*\n{appMentionEvent.User}"),
                }
            };

            var message = new PostMessageRequest
            {
                Channel = appMentionEvent.Channel,
                Blocks = new List<IMessageBlock>
                {
                    titleSection
                }
            };

            await _apiClient.Chat.Post(message);
        }

        private void DefaultEventReaction(
            EventWrapper eventWrapper)
        {
            _logger.LogInformation(
                "Noop reaction for event `{EventType}`",
                eventWrapper.Event?.Type);
        }
    }
}

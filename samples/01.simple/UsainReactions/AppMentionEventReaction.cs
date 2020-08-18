namespace Usain.Samples.Simple.UsainReactions
{
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using global::Slack.NetStandard;
    using global::Slack.NetStandard.Messages.Blocks;
    using global::Slack.NetStandard.WebApi.Chat;
    using Microsoft.Extensions.Logging;
    using Slack.Models.Events;
    using Slack.Models.Events.CallbackEvents;

    public class AppMentionEventReaction : EventReaction<AppMentionEvent>
    {
        private readonly string[] _greetings =
        {
            "Hi <@{0}>! What's going on ?",
            "How's it going <@{0}> ?",
            "Good to see you <@{0}>, it's been a while !",
            "How can i help you <@{0}> ?",
        };

        public AppMentionEventReaction(
            ILogger<EventReaction<AppMentionEvent>> logger,
            ISlackApiClient slackApiClient,
            EventWrapper eventWrapper)
            : base(
                logger,
                slackApiClient,
                eventWrapper)
        {
        }

        protected override PostMessageRequest CreatePostMessageRequest(
            IChannelEvent channelEvent)
        {
            if (!(EventWrapper.Event is AppMentionEvent appMentionEvent))
            {
                return CreateDefaultMessage();
            }

            var titleSection =
                new Section(
                    new MarkdownText(GetGreetings(appMentionEvent.UserId)));

            return new PostMessageRequest
            {
                Channel = appMentionEvent.ChannelId,
                Blocks = new List<IMessageBlock>
                {
                    titleSection,
                },
                ThreadId = appMentionEvent.ParentMessageId.IsEmpty
                    ? appMentionEvent.MessageId.ToString()
                    : appMentionEvent.ParentMessageId.ToString(),
            };
        }

        private string GetGreetings(
            string user)
            => string.Format(
                _greetings[
                    RandomNumberGenerator.GetInt32(_greetings.Length - 1)],
                user);
    }
}

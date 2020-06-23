namespace Usain.Samples.Simple.UsainReactions
{
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using global::Slack.NetStandard;
    using global::Slack.NetStandard.Messages.Blocks;
    using global::Slack.NetStandard.WebApi.Chat;
    using Microsoft.Extensions.Logging;
    using Slack.Models;
    using Slack.Models.CallbackEvents;

    public class AppMentionEventReaction : EventReaction<AppMentionEvent>
    {
        private readonly string[] _greetings = {
            "Hi <@{0}>! What's going on ?",
            "How's it going <@{0}>?",
            "Good to see you <@{0}>?",
            "It’s been a while <@{0}>!",
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
            if (!(_eventWrapper.Event is AppMentionEvent appMentionEvent))
            {
                return CreateDefaultMessage();
            }

            var titleSection =
                new Section(
                    new MarkdownText(GetGreetings(appMentionEvent.User)));

            return new PostMessageRequest
            {
                Channel = appMentionEvent.Channel,
                Blocks = new List<IMessageBlock>
                {
                    titleSection
                }
            };
        }

        private string GetGreetings(
            string user)
        {
            return string.Format(
                _greetings[
                    RandomNumberGenerator.GetInt32(_greetings.Length - 1)],
                user);
        }
    }
}

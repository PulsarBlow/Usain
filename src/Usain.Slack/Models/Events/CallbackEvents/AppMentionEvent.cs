namespace Usain.Slack.Models.Events.CallbackEvents
{
    using System.Text.Json.Serialization;

    public class AppMentionEvent : CallbackEvent, IChannelEvent
    {
        internal const string TextJsonName = "text";
        internal const string UserIdJsonName = "user";
        internal const string MessageIdJsonName = "ts";
        internal const string ChannelIdJsonName = "channel";
        internal const string ParentMessageIdJsonName = "thread_ts";
        internal const string ParentUserIdJsonName = "parent_user_id";

        /// <summary>
        /// Callback event type value for the <see cref="AppMentionEvent"/> event.
        /// </summary>
        public const string CallbackEventTypeValue = "app_mention";

        /// <summary>
        /// The text spoken.
        /// </summary>
        /// <example>&lt;@U015SRNU3HR&gt; Hi Usain!</example>
        [JsonPropertyName(TextJsonName)]
        public string? Text { get; set; }

        /// <summary>
        /// The unique identifier of this mention message.
        /// </summary>
        /// <example>1596669197.006400</example>
        [JsonPropertyName(MessageIdJsonName)]
        public Timestamp MessageId { get; set; } = Timestamp.Empty;

        /// <summary>
        /// The identifier of the channel,
        /// private group or DM channel this mention message is posted in.
        /// </summary>
        /// <example>G015SS2E0D9</example>
        [JsonPropertyName(ChannelIdJsonName)]
        public string? ChannelId { get; set; }

        /// <summary>
        /// The identifier of the user speaking.
        /// </summary>
        /// <example>T014D2ARLD8</example>
        [JsonPropertyName(UserIdJsonName)]
        public string? UserId { get; set; }

        /// <summary>
        /// The unique identifier of the parent message if this message
        /// is part of a threaded message
        /// </summary>
        /// <example>1596669197.006400</example>
        [JsonPropertyName(ParentMessageIdJsonName)]
        public Timestamp ParentMessageId { get; set; } = Timestamp.Empty;

        /// <summary>
        /// This unique identifier of the user of the parent message if the message
        /// is part of a threaded message
        /// </summary>
        /// <example>U0146A68TAS</example>
        [JsonPropertyName(ParentUserIdJsonName)]
        public string? ParentUserId { get; set; }

        public AppMentionEvent()
            => CallbackEventType = CallbackEventTypeValue;

    }
}

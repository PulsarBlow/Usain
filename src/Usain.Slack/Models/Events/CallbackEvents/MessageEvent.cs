namespace Usain.Slack.Models.Events.CallbackEvents
{
    using System.Text.Json.Serialization;
    using Blocks;
    using Messages;

    /// <summary>
    /// A message was sent to a channel
    /// </summary>
    public class MessageEvent : CallbackEvent, IChannelEvent
    {
        internal const string SubTypeJsonName = "subtype";
        internal const string TextJsonName = "text";
        internal const string MessageIdJsonName = "ts";
        internal const string ChannelIdJsonName = "channel";
        internal const string ChannelTypeJsonName = "channel_type";
        internal const string UserIdJsonName = "user";
        internal const string EditedJsonName = "edited";
        internal const string IsHiddenJsonName = "hidden";
        internal const string IsStarredJsonName = "is_starred";
        internal const string PinnedToJsonName = "pinned_to";
        internal const string ParentMessageIdJsonName = "thread_ts";
        internal const string ParentUserIdJsonName = "parent_user_id";
        internal const string ReplyCountJsonName = "reply_count";
        internal const string RepliesJsonName = "replies";
        internal const string BlocksJsonName = "blocks";
        internal const string ReactionsJsonName = "reactions";

        /// <summary>
        /// Callback event type value for the <see cref="MessageEvent"/> event.
        /// </summary>
        public const string CallbackEventTypeValue = "message";

        /// <summary>
        /// The subtype of the message
        /// </summary>
        [JsonPropertyName(SubTypeJsonName)]
        public string? MessageSubType { get; set; }

        /// <summary>
        /// The text spoken.
        /// </summary>
        [JsonPropertyName(TextJsonName)]
        public string? Text { get; set; }

        /// <summary>
        /// The unique identifier of this message.
        /// </summary>
        [JsonPropertyName(MessageIdJsonName)]
        public Timestamp MessageId { get; set; } = Timestamp.Empty;

        /// <summary>
        /// The identifier of the channel,
        /// private group or DM channel this message is posted in.
        /// </summary>
        [JsonPropertyName(ChannelIdJsonName)]
        public string? ChannelId { get; set; }

        /// <summary>
        /// The type of the channel the message is posted in.
        /// </summary>
        [JsonPropertyName(ChannelTypeJsonName)]
        public string? ChannelType { get; set; }

        /// <summary>
        /// The identifier of the user speaking.
        /// </summary>
        /// <example>T014D2ARLD8</example>
        [JsonPropertyName(UserIdJsonName)]
        public string? UserId { get; set; }

        /// <summary>
        /// Edit details
        /// </summary>
        [JsonPropertyName(EditedJsonName)]
        public Edited? Edited { get; set; }

        /// <summary>
        /// Whether or not this message is part of the history of a channel
        /// but should not be displayed to users.
        /// Examples include records of message edits or deletes.
        /// </summary>
        [JsonPropertyName(IsHiddenJsonName)]
        public bool? IsHidden { get; set; }

        /// <summary>
        /// True if the calling user has starred the message.
        /// </summary>
        [JsonPropertyName(IsStarredJsonName)]
        public bool? IsStarred { get; set; }

        /// <summary>
        /// Contains the IDs of any channels to which the message is currently pinned.
        /// </summary>
        [JsonPropertyName(PinnedToJsonName)]
        public string[]? PinnedTo { get; set; }

        /// <summary>
        /// The unique identifier of the parent message if this message
        /// is part of a threaded message
        /// </summary>
        [JsonPropertyName(ParentMessageIdJsonName)]
        public Timestamp? ParentMessageId { get; set; }

        /// <summary>
        /// This unique identifier of the user of the parent message if the message
        /// is part of a threaded message
        /// </summary>
        [JsonPropertyName(ParentUserIdJsonName)]
        public string? ParentUserId { get; set; }

        /// <summary>
        /// The number of reply to this message
        /// </summary>
        [JsonPropertyName(ReplyCountJsonName)]
        public long? ReplyCount { get; set; }

        /// <summary>
        /// Replies to this message
        /// </summary>
        [JsonPropertyName(RepliesJsonName)]
        public MessageEvent[]? Replies { get; set; }

        /// <summary>
        /// Blocks
        /// </summary>
        [JsonPropertyName(BlocksJsonName)]
        public Block[]? Blocks { get; set; }

        /// <summary>
        /// The reactions property, if present, contains any reactions that have been added to the message and gives information about the type of reaction,
        /// the total number of users who added that reaction and a (possibly incomplete) list of users who have added that reaction to the message.
        /// </summary>
        [JsonPropertyName(ReactionsJsonName)]
        public Reaction[]? Reactions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEvent"/> class.
        /// </summary>
        public MessageEvent()
            => CallbackEventType = CallbackEventTypeValue;
    }
}

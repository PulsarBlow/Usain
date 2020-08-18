namespace Usain.Slack.Models.Events.CallbackEvents
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A message_replied message is sent when a channel's message is a reply to another message (its parent).
    /// The message's thread_ts value indicates the parent message.
    /// </summary>
    /// <remarks>
    /// Bug alert!
    /// This event is missing the subtype field when dispatched over the Events API.
    /// Until it is fixed, examine message events' thread_ts value.
    /// When present, it's a reply.
    /// To be doubly sure, compare a thread_ts to the top-level ts value,
    /// when they differ the latter is a reply to the former.
    /// see: https://api.slack.com/events/message/message_replied
    /// </remarks>
    public class MessageRepliedEvent : MessageEvent
    {
        internal const string ParentMessageJsonName = "message";

        /// <summary>
        /// Message subtype value for the <see cref="MessageRepliedEvent"/> event.
        /// </summary>
        public const string MessageSubTypeValue = "message_replied";

        /// <summary>
        /// The message replied to
        /// </summary>
        [JsonPropertyName(ParentMessageJsonName)]
        public MessageEvent? ParentMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRepliedEvent"/> class.
        /// </summary>
        public MessageRepliedEvent()
            => MessageSubType = MessageSubTypeValue;
    }
}

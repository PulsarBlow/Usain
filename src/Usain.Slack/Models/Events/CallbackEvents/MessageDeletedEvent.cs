namespace Usain.Slack.Models.Events.CallbackEvents
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A message_deleted message is sent when a message in a channel is deleted.
    /// </summary>
    public class MessageDeletedEvent : MessageEvent
    {
        internal const string DeletedMessageIdJsonName = "deleted_ts";

        /// <summary>
        /// Message subtype value for the <see cref="MessageDeletedEvent"/> event.
        /// </summary>
        public const string MessageSubTypeValue = "message_deleted";

        /// <summary>
        /// The unique identifier of the deleted message (its timestamp).
        /// </summary>
        [JsonPropertyName(DeletedMessageIdJsonName)]
        public Timestamp DeletedMessageId { get; set; } = Timestamp.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageDeletedEvent"/> class.
        /// </summary>
        public MessageDeletedEvent()
            => MessageSubType = MessageSubTypeValue;
    }
}

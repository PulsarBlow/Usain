namespace Usain.Slack.Models.Events.CallbackEvents
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A message_changed message is sent when a message in a channel is edited.
    /// </summary>
    public class MessageChangedEvent : MessageEvent
    {
        internal const string PreviousMessageJsonName = "previous_message";

        /// <summary>
        /// Message subtype value for the <see cref="MessageChangedEvent"/> event.
        /// </summary>
        public const string MessageSubTypeValue = "message_changed";

        /// <summary>
        /// Previous state of the message.
        /// </summary>
        [JsonPropertyName(PreviousMessageJsonName)]
        public MessageEvent? PreviousMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageChangedEvent"/> class.
        /// </summary>
        public MessageChangedEvent()
            => MessageSubType = MessageSubTypeValue;
    }
}

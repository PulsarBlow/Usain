namespace Usain.Slack.Models.Events.CallbackEvents
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using JsonConverters;

    /// <summary>
    /// The actual event, an object, that happened
    /// </summary>
    /// <example>
    /// {
    ///   "channel": "D0PNCRP9N",
    ///   "channel_type": "app_home",
    ///   "event_ts": "1525215129.000001",
    ///   "text": "How many cats did we herd yesterday?",
    ///   "ts": "1525215129.000001",
    ///   "type": "message",
    ///   "user": "U061F7AUR"
    ///  }
    /// </example>
    [JsonConverter(typeof(CallbackEventJsonConverter))]
    public class CallbackEvent
    {
        internal const string EventTypeJsonName = "type";
        internal const string WorkspaceIdJsonName = "team";
        internal const string EventTimestampJsonName = "event_ts";
        internal const string DefaultCallbackEventTypeValue = "unknown";

        /// <summary>
        /// The specific type of the callback event.
        /// </summary>
        [JsonPropertyName(EventTypeJsonName)]
        public string? CallbackEventType { get; set; }

        /// <summary>
        /// The unique identifier of the workspace where the event occurred.
        /// </summary>
        [JsonPropertyName(WorkspaceIdJsonName)]
        public string? WorkspaceId { get; set; }

        /// <summary>
        /// When the event was dispatched
        /// </summary>
        [JsonPropertyName(EventTimestampJsonName)]
        public Timestamp EventTimestamp { get; set; } =
            Timestamp.Empty;

        /// <summary>
        /// Extra json properties not directly mapped to this type definition
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, object> ExtraFields { get; set; } =
            new Dictionary<string, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CallbackEvent"/> class.
        /// </summary>
        public CallbackEvent()
            => CallbackEventType = DefaultCallbackEventTypeValue;
    }
}

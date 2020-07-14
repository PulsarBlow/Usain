namespace Usain.Slack.Models.CallbackEvents
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
    [JsonConverter(typeof(CallbackEventConverter))]
    public class CallbackEvent
    {
        internal const string TypePropertyName = "type";
        internal const string EventTimestampPropertyName = "event_ts";

        /// <summary>
        /// The specific name of the event
        /// </summary>
        [JsonPropertyName(TypePropertyName)]
        public string? Type { get; set; }

        /// <summary>
        /// When the event was dispatched
        /// </summary>
        [JsonPropertyName(EventTimestampPropertyName)]
        public EventTimestamp EventTimestamp { get; set; } =
            EventTimestamp.Empty;

        [JsonExtensionData]
        public Dictionary<string, object> OtherFields { get; set; } =
            new Dictionary<string, object>();
    }
}

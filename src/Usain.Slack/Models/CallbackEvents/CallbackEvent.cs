namespace Usain.Slack.Models.CallbackEvents
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using JsonConverters;

    [JsonConverter(typeof(CallbackEventConverter))]
    public class CallbackEvent
    {
        internal const string TypePropertyName = "type";
        internal const string EventTimestampPropertyName = "event_ts";

        [JsonPropertyName(TypePropertyName)]
        public string? Type { get; set; }

        [JsonPropertyName(EventTimestampPropertyName)]
        public EventTimestamp EventTimestamp { get; set; } =
            EventTimestamp.Empty;

        [JsonExtensionData]
        public Dictionary<string, object> OtherFields { get; set; } =
            new Dictionary<string, object>();
    }
}

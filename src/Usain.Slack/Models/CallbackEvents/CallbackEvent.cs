namespace Usain.Slack.Models.CallbackEvents
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using JsonConverters;

    [JsonConverter(typeof(CallbackEventConverter))]
    public class CallbackEvent
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("event_ts")]
        public EventTimestamp EventTimestamp { get; set; } =
            EventTimestamp.Empty;

        [JsonExtensionData]
        public Dictionary<string, object> OtherFields { get; set; } =
            new Dictionary<string, object>();
    }
}

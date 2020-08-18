namespace Usain.Slack.Models.Events
{
    using System.Text.Json.Serialization;
    using JsonConverters;

    [JsonConverter(typeof(EventJsonConverter))]
    public class Event
    {
        internal const string EventTypeJsonName = "type";
        internal const string DefaultEventTypeValue = "unknown";

        /// <summary>
        /// Indicates which kind of event dispatch this is, usually `event_callback`
        /// </summary>
        /// <example>event_callback</example>
        [JsonPropertyName(EventTypeJsonName)]
        public string EventType { get; set; } = DefaultEventTypeValue;
    }
}

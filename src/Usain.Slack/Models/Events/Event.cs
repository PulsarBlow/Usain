namespace Usain.Slack.Models.Events
{
    using System.Text.Json.Serialization;
    using JsonConverters;

    [JsonConverter(typeof(EventBaseConverter))]
    public class Event
    {
        /// <summary>
        /// Indicates which kind of event dispatch this is, usually `event_callback`
        /// </summary>
        /// <example>event_callback</example>
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}

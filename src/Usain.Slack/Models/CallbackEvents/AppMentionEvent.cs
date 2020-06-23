namespace Usain.Slack.Models.CallbackEvents
{
    using System.Text.Json.Serialization;

    public class AppMentionEvent : CallbackEvent, IChannelEvent
    {
        public const string EventType = "app_mention";

        [JsonPropertyName("user")]
        public string? User { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("ts")]
        public EventTimestamp Timestamp { get; set; } = EventTimestamp.Empty;

        [JsonPropertyName("channel")]
        public string? Channel { get; set; }
    }
}

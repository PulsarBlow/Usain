namespace Usain.Slack.Models.CallbackEvents
{
    using System.Text.Json.Serialization;

    public class AppMentionEvent : CallbackEvent, IChannelEvent
    {
        internal const string UserJsonName = "user";
        internal const string TextJsonName = "text";
        internal const string TimestampJsonName = "ts";
        internal const string ChannelJsonName = "channel";
        public const string EventType = "app_mention";

        [JsonPropertyName(UserJsonName)]
        public string? User { get; set; }

        [JsonPropertyName(TextJsonName)]
        public string? Text { get; set; }

        [JsonPropertyName(TimestampJsonName)]
        public EventTimestamp Timestamp { get; set; } = EventTimestamp.Empty;

        [JsonPropertyName(ChannelJsonName)]
        public string? Channel { get; set; }
    }
}

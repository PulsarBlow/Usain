namespace Usain.Slack.Models
{
    using System.Text.Json.Serialization;

    public class UrlVerificationEvent : Event
    {
        public const string EventType = "url_verification";

        [JsonPropertyName("challenge")]
        public string? Challenge { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}

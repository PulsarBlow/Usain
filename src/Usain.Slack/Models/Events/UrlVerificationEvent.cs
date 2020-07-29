namespace Usain.Slack.Models.Events
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Verifies ownership of an Events API Request URL
    /// This event does not require a specific OAuth scope or subscription.
    /// You'll automatically receive it whenever configuring an Events API Request URL.
    /// Once you receive the event, verify the request's authenticity and then
    /// respond in plaintext with the challenge attribute value
    /// </summary>
    public class UrlVerificationEvent : Event
    {
        public const string EventType = "url_verification";

        [JsonPropertyName("challenge")]
        public string? Challenge { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}

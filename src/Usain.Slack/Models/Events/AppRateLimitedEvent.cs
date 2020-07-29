namespace Usain.Slack.Models.Events
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// This event type is only dispatched when your app is rate limited on the Events API.
    /// Rate limiting currently occurs when your app would receive more than 30,000 events in an hour from a single workspace.
    /// This event does not require a specific OAuth scope or subscription.
    /// You'll automatically receive it when your app event subscriptions are rate limited or disabled.
    /// Event subscriptions may be limited and disabled when your app does not
    /// respond with a HTTP 200 OK to at 5% of event deliveries in the past 60 minutes.
    /// </summary>
    public class AppRateLimitedEvent : Event
    {
        public const string EventType = "app_rate_limited";

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("team_id")]
        public string? TeamId { get; set; }

        [JsonPropertyName("api_app_id")]
        public string? ApiAppId { get; set; }

        [JsonPropertyName("minute_rate_limited")]
        public long MinuteRateLimited { get; set; }
    }
}

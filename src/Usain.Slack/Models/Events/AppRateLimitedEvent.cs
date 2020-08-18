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
        internal const string TokenJsonName = "token";
        internal const string WorkspaceIdJsonName = "team_id";
        internal const string ApplicationIdJsonName = "api_app_id";
        internal const string MinuteRateLimitedJsonName = "minute_rate_limited";

        /// <summary>
        /// Event type value for the <see cref="AppRateLimitedEvent"/> event.
        /// </summary>
        public const string EventTypeValue = "app_rate_limited";

        /// <summary>
        /// This deprecated verification token is proof that the request
        /// is coming from Slack on behalf of your application.
        /// </summary>
        /// <example>Jhj5dZrVaK7ZwHHjRyZWjbDl</example>
        [JsonPropertyName(TokenJsonName)]
        public string? Token { get; set; }

        /// <summary>
        /// The unique identifier of the workspace where the event occurred
        /// </summary>
        /// <example>T014D2ARLD8</example>
        [JsonPropertyName(WorkspaceIdJsonName)]
        public string? WorkspaceId { get; set; }

        /// <summary>
        /// The unique identifier your installed Slack application.
        /// Use this to distinguish which app the event belongs to if you
        /// use multiple apps with the same Request URL.
        /// </summary>
        /// <example>A2H9RFS1A</example>
        [JsonPropertyName(ApplicationIdJsonName)]
        public string? ApplicationId { get; set; }

        /// <summary>
        /// A rounded epoch time value indicating the minute your application
        /// became rate limited for this workspace.
        /// </summary>
        /// <example>1518467820 is at 2018-02-12 20:37:00 UTC</example>
        [JsonPropertyName(MinuteRateLimitedJsonName)]
        public long MinuteRateLimited { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppRateLimitedEvent"/> class.
        /// </summary>
        public AppRateLimitedEvent()
            => EventType = EventTypeValue;
    }
}

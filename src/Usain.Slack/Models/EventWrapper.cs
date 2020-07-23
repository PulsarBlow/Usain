namespace Usain.Slack.Models
{
    using System.Text.Json.Serialization;
    using CallbackEvents;

    public class EventWrapper : Event
    {
        public const string EventType = "event_callback";

        /// <summary>
        /// A verification token to validate the event originated from Slack
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        /// <summary>
        /// The unique identifier of the workspace where the event occurred
        /// </summary>
        /// <example>T1H9RESGL</example>
        [JsonPropertyName("team_id")]
        public string? TeamId { get; set; }

        /// <summary>
        /// The unique identifier your installed Slack application.
        /// Use this to distinguish which app the event belongs to if you
        /// use multiple apps with the same Request URL.
        /// </summary>
        /// <example>A2H9RFS1A</example>
        [JsonPropertyName("api_app_id")]
        public string? ApiAppId { get; set; }

        /// <summary>
        /// An array of string-based User IDs.
        /// Each member of the collection represents a user that has installed your application/bot
        /// and indicates the described event would be visible to those users.
        /// </summary>
        [JsonPropertyName("authed_users")]
        public string[]? AuthedUsers { get; set; }

        /// <summary>
        /// A unique identifier for this specific event, globally unique across all workspaces.
        /// </summary>
        /// <example>Ev0PV52K25</example>
        [JsonPropertyName("event_id")]
        public string? EventId { get; set; }

        /// <summary>
        /// The epoch timestamp in seconds indicating when this event was dispatched.
        /// </summary>
        /// <example>1525215129</example>
        [JsonPropertyName("event_time")]
        public long? EventTime { get; set; }

        [JsonPropertyName("event")]
        public CallbackEvent? Event { get; set; }

        public EventWrapper()
            : this(EventType)
        {
        }

        protected EventWrapper(
            string eventType)
            => Type = eventType;
    }
}

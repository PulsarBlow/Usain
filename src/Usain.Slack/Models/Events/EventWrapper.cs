namespace Usain.Slack.Models.Events
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using CallbackEvents;

    public class EventWrapper : Event
    {
        internal const string TokenJsonName = "token";
        internal const string WorkspaceIdJsonName = "team_id";
        internal const string ApplicationIdJsonName = "api_app_id";
        internal const string AuthenticatedUserIdsJsonName = "authed_users";
        internal const string EventIdJsonName = "event_id";
        internal const string EventTimeJsonName = "event_time";
        internal const string EventJsonName = "event";

        /// <summary>
        /// Event type value for the <see cref="EventWrapper"/> event.
        /// </summary>
        public const string EventTypeValue = "event_callback";

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
        /// An array of string-based User IDs.
        /// Each member of the collection represents a user that has installed your application/bot
        /// and indicates the described event would be visible to those users.
        /// </summary>
        [JsonPropertyName(AuthenticatedUserIdsJsonName)]
        public string[]? AuthenticatedUserIds { get; set; }

        /// <summary>
        /// A unique identifier for this specific event, globally unique across all workspaces.
        /// </summary>
        /// <example>Ev0PV52K25</example>
        [JsonPropertyName(EventIdJsonName)]
        public string? EventId { get; set; }

        /// <summary>
        /// The epoch timestamp in seconds indicating when this event was dispatched.
        /// </summary>
        /// <example>1525215129</example>
        [JsonPropertyName(EventTimeJsonName)]
        public long? EventTime { get; set; }

        [JsonPropertyName(EventJsonName)]
        public CallbackEvent? Event { get; set; }

        /// <summary>
        /// Extra json properties not directly mapped to this type definition.
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, object> ExtraFields { get; set; } =
            new Dictionary<string, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventWrapper"/> class.
        /// </summary>
        public EventWrapper()
            => EventType = EventTypeValue;
    }
}

namespace Usain.Slack.Models.Messages
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Message edition details
    /// </summary>
    public class Edited
    {
        internal const string UserIdJsonName = "user";
        internal const string TimestampJsonName = "ts";

        /// <summary>
        /// User id of the editor
        /// </summary>
        /// <example>U2147483697</example>
        [JsonPropertyName(UserIdJsonName)]
        public string? UserId { get; set; }

        /// <summary>
        /// Time of the edition
        /// </summary>
        /// <see cref="1355517536.001"/>
        [JsonPropertyName(TimestampJsonName)]
        public Timestamp Timestamp { get; set; } = Timestamp.Empty;
    }
}

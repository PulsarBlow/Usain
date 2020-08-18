namespace Usain.Slack.Models.Messages
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A reaction that have been added to a message.
    /// </summary>
    public class Reaction
    {
        internal const string NameJsonName = "name";
        internal const string CountJsonName = "count";
        internal const string UsersJsonName = "users";

        /// <summary>
        /// The name of the reaction
        /// </summary>
        /// <example>astonished</example>
        [JsonPropertyName(NameJsonName)]
        public string? Name { get; set; }

        /// <summary>
        /// The number of user associated to this reaction
        /// </summary>
        [JsonPropertyName(CountJsonName)]
        public long Count { get; set; }

        /// <summary>
        /// The list of users who have reacted.
        /// </summary>
        [JsonPropertyName(UsersJsonName)]
        public string[]? Users { get; set; }
    }
}

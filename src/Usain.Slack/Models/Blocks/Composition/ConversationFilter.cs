namespace Usain.Slack.Models.Blocks.Composition
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Provides a way to filter the list of options in a conversations select menu
    /// or conversations multi-select menu.
    /// </summary>
    /// <remarks>https://api.slack.com/reference/block-kit/composition-objects#filter_conversations</remarks>
    public class ConversationFilter
    {
        internal const string IncludeJsonName = "include";
        internal const string ExcludeExternalSharedChannelsJsonName =
            "exclude_external_shared_channels";
        internal const string ExcludeBotUsersJsonName = "exclude_bot_users";

        /// <summary>
        /// Indicates which type of conversations should be included in the list.
        /// When this field is provided, any conversations that do not match will be excluded.
        /// You should provide an array of strings from the following options: im, mpim, private, and public.
        /// </summary>
        /// <remarks>The array cannot be empty.</remarks>
        [JsonPropertyName(IncludeJsonName)]
        public ConversationFilterOption[]? Include { get; set; }

        /// <summary>
        /// Indicates whether to exclude external shared channels from conversation lists.
        /// </summary>
        [JsonPropertyName(ExcludeExternalSharedChannelsJsonName)]
        public bool ExcludeExternalSharedChannels { get; set; }

        /// <summary>
        /// Indicates whether to exclude bot users from conversation lists.
        /// </summary>
        [JsonPropertyName(ExcludeBotUsersJsonName)]
        public bool ExcludeBotUsers { get; set; }
    }
}

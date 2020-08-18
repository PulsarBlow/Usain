namespace Usain.Slack.Models.Blocks.Elements.Menus
{
    using System.Text.Json.Serialization;
    using Composition;

    /// <summary>
    /// Works with block types: Section, Input.
    /// This multi-select menu will populate its options with a list
    /// of public and private channels, DMs, and MPIMs visible to the current user
    /// in the active workspace.
    /// </summary>
    /// <remarks>https://api.slack.com/reference/block-kit/block-elements#conversation_multi_select</remarks>
    public class ConversationMultiSelectMenu : MultiSelectMenu
    {
        internal const string SelectedConversationsJsonName =
            "initial_conversations";
        internal const string DefaultCurrentConversationJsonName =
            "default_current_conversation";
        internal const string ConversationFilterJsonName = "filter";

        /// <summary>
        /// Element type value for <see cref="ConversationMultiSelectMenu"/> element.
        /// </summary>
        public const string ElementTypeValue = "multi_conversations_select";

        /// <summary>
        /// An array of one or more IDs of any valid conversations to be pre-selected when the menu loads.
        /// If default_to_current_conversation is also supplied, initial_conversations will be ignored.
        /// </summary>
        [JsonPropertyName(SelectedConversationsJsonName)]
        public string[]? SelectedConversations { get; set; }

        /// <summary>
        /// Pre-populates the select menu with the conversation that the user
        /// was viewing when they opened the modal, if available.
        /// </summary>
        /// <remarks>Default is false.</remarks>
        [JsonPropertyName(DefaultCurrentConversationJsonName)]
        public bool DefaultToCurrentConversation { get; set; }

        /// <summary>
        /// A filter object that reduces the list of available conversations using the specified criteria.
        /// </summary>
        [JsonPropertyName(ConversationFilterJsonName)]
        public ConversationFilter? ConversationFilter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationMultiSelectMenu"/> class.
        /// </summary>
        public ConversationMultiSelectMenu()
            => ElementType = ElementTypeValue;
    }
}

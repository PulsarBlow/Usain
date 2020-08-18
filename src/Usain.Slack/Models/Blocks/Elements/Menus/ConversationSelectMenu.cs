namespace Usain.Slack.Models.Blocks.Elements.Menus
{
    using System.Text.Json.Serialization;
    using Composition;

    /// <summary>
    /// Works with block types: Section, Actions, Input.
    /// This select menu will populate its options with a list
    /// of public and private channels, DMs, and MPIMs visible to the current user
    /// in the active workspace.
    /// </summary>
    /// <remarks>https://api.slack.com/reference/block-kit/block-elements#conversation_select</remarks>
    public class ConversationSelectMenu : SelectMenu
    {
        internal const string SelectedConversationJsonName =
            "initial_conversation";
        internal const string DefaultToCurrentConversationJsonName =
            "default_to_current_conversation";
        internal const string ResponseUrlEnabledJsonName =
            "response_url_enabled";
        internal const string ConversationFilterJsonName = "filter";

        /// <summary>
        /// Element type value for the <see cref="ConversationSelectMenu"/> element.
        /// </summary>
        public const string ElementTypeValue = "conversations_select";

        /// <summary>
        /// The ID of any valid conversation to be pre-selected when the menu loads.
        /// If default_to_current_conversation is also supplied, initial_conversation will be ignored.
        /// </summary>
        [JsonPropertyName(SelectedConversationJsonName)]
        public string? SelectedConversation { get; set; }

        /// <summary>
        /// Pre-populates the select menu with the conversation that the user
        /// was viewing when they opened the modal, if available.
        /// </summary>
        /// <remarks>Default is false.</remarks>
        [JsonPropertyName(DefaultToCurrentConversationJsonName)]
        public bool DefaultToCurrentConversation { get; set; }

        /// <summary>
        /// This field only works with menus in input blocks in modals.
        /// When set to true, the view_submission payload from the menu's parent
        /// view will contain a response_url.This response_url can be
        /// used for message responses.The target conversation for the
        /// message will be determined by the value of this select menu.
        /// </summary>
        [JsonPropertyName(ResponseUrlEnabledJsonName)]
        public bool ResponseUrlEnabled { get; set; }

        /// <summary>
        /// A filter object that reduces the list of available conversations using the specified criteria.
        /// </summary>
        [JsonPropertyName(ConversationFilterJsonName)]
        public ConversationFilter? ConversationFilter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationSelectMenu"/> class.
        /// </summary>
        public ConversationSelectMenu()
            => ElementType = ElementTypeValue;
    }
}

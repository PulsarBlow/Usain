namespace Usain.Slack.Models.Blocks.Elements.Menus
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Works with block types: Section, Input
    /// This multi-select menu will populate its options with a list of Slack users
    /// visible to the current user in the active workspace.
    /// </summary>
    /// <remarks>https://api.slack.com/reference/block-kit/block-elements#users_multi_select</remarks>
    public class UserMultiSelectMenu : MultiSelectMenu
    {
        internal const string SelectedUsersJsonName = "initial_users";

        /// <summary>
        /// Element type value for the <see cref="UserMultiSelectMenu"/> class.
        /// </summary>
        public const string ElementTypeValue = "multi_users_select";

        /// <summary>
        /// An array of user IDs of any valid users to be pre-selected when the menu loads.
        /// </summary>
        [JsonPropertyName(SelectedUsersJsonName)]
        public string[]? SelectedUsers { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMultiSelectMenu"/> class.
        /// </summary>
        public UserMultiSelectMenu()
            => ElementType = ElementTypeValue;
    }
}

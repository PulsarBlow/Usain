namespace Usain.Slack.Models.Blocks.Elements.Menus
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Works with block types: Section, Actions, Input.
    /// This select menu will populate its options with a list of Slack users
    /// visible to the current user in the active workspace.
    /// </summary>
    /// <remarks>https://api.slack.com/reference/block-kit/block-elements#users_select</remarks>
    public class UserSelectMenu : SelectMenu
    {
        internal const string SelectedUserJsonName = "initial_user";

        /// <summary>
        /// Element type value for the <see cref="UserSelectMenu"/> element.
        /// </summary>
        public const string ElementTypeValue = "users_select";

        /// <summary>
        /// The user ID of any valid user to be pre-selected when the menu loads.
        /// </summary>
        [JsonPropertyName(SelectedUserJsonName)]
        public string? SelectedUser { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSelectMenu"/> class.
        /// </summary>
        public UserSelectMenu()
            => ElementType = ElementTypeValue;
    }
}

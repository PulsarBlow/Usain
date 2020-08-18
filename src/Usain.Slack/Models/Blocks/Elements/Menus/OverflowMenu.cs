namespace Usain.Slack.Models.Blocks.Elements.Menus
{
    using System.Text.Json.Serialization;
    using Composition;

    /// <summary>
    /// Works with block types: Section, Actions
    /// This is like a cross between a button and a select menu - when a user clicks on this overflow button,
    /// they will be presented with a list of options to choose from
    /// </summary>
    /// <remarks>https://api.slack.com/reference/block-kit/block-elements#overflow</remarks>
    public class OverflowMenu : ActionElement
    {
        internal const string OptionsJsonName = "options";
        internal const string ConfirmDialogJsonName = "confirm";

        /// <summary>
        /// Element type value for <see cref="OverflowMenu"/> element.
        /// </summary>
        public const string ElementTypeValue = "overflow";

        /// <summary>
        /// An array of option objects to display in the menu.
        /// </summary>
        /// <remarks>Maximum number of options is 5, minimum is 2.</remarks>
        [JsonPropertyName(OptionsJsonName)]
        public Option[]? Options { get; set; }

        /// <summary>
        /// A confirm object that defines an optional confirmation dialog that appears after a menu item is selected.
        /// </summary>
        [JsonPropertyName(ConfirmDialogJsonName)]
        public ConfirmDialog? ConfirmDialog { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OverflowMenu"/> class.
        /// </summary>
        public OverflowMenu()
            => ElementType = ElementTypeValue;
    }
}

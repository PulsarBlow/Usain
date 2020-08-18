namespace Usain.Slack.Models.Blocks.Elements.Menus
{
    using System.Text.Json.Serialization;
    using Composition;

    /// <summary>
    /// A select menu, just as with a standard HTML &lt;select&gt; tag,
    /// creates a drop down menu with a list of options for a user to choose.
    /// The select menu also includes type-ahead functionality,
    /// where a user can type a part or all of an option string to filter the list.
    /// </summary>
    /// <remarks>https://api.slack.com/reference/block-kit/block-elements#select</remarks>
    public class SelectMenu : ActionElement
    {
        internal const string PlaceholderJsonName = "placeholder";
        internal const string ConfirmDialogJsonName = "confirm";

        /// <summary>
        /// A plain_text only text object that defines the placeholder text shown on the menu.
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 150 characters.</remarks>
        [JsonPropertyName(PlaceholderJsonName)]
        public PlainText? Placeholder { get; set; }

        /// <summary>
        /// A <see cref="ConfirmDialog"/> object that defines an optional confirmation dialog
        /// that appears after a menu item is selected.
        /// </summary>
        [JsonPropertyName(ConfirmDialogJsonName)]
        public ConfirmDialog? ConfirmDialog { get; set; }
    }
}

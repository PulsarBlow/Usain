namespace Usain.Slack.Models.Blocks.Composition
{
    using System.Text.Json.Serialization;
    using Elements;

    /// <summary>
    /// An object that represents a single selectable item in a select menu,
    /// multi-select menu, checkbox group, radio button group, or overflow menu.
    /// </summary>
    public class Option
    {
        internal const string TextJsonName = "text";
        internal const string ValueJsonName = "value";
        internal const string DescriptionJsonName = "description";
        internal const string UrlJsonName = "url";

        /// <summary>
        /// A text object that defines the text shown in the option on the menu.
        /// Overflow, select, and multi-select menus can only use plain_text objects,
        /// while radio buttons and checkboxes can use mrkdwn text objects.
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 75 characters.</remarks>
        [JsonPropertyName(TextJsonName)]
        public TextElement? Text { get; set; }

        /// <summary>
        /// The string value that will be passed to your app when this option is chosen.
        /// </summary>
        /// <remarks>Maximum length for this field is 75 characters.</remarks>
        [JsonPropertyName(ValueJsonName)]
        public string? Value { get; set; }

        /// <summary>
        /// A plain_text only text object that defines a line of descriptive
        /// text shown below the text field beside the radio button.
        /// </summary>
        /// <remarks>Maximum length for the text object within this field is 75 characters.</remarks>
        [JsonPropertyName(DescriptionJsonName)]
        public PlainText? Description { get; set; }

        /// <summary>
        /// A URL to load in the user's browser when the option is clicked.
        /// The url attribute is only available in overflow menus.
        /// </summary>
        /// <remarks>Maximum length for this field is 3000 characters.</remarks>
        [JsonPropertyName(UrlJsonName)]
        public string? Url { get; set; }
    }
}

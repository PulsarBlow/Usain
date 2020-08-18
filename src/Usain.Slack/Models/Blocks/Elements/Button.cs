namespace Usain.Slack.Models.Blocks.Elements
{
    using System.Text.Json.Serialization;
    using Composition;

    /// <summary>
    /// An interactive component that inserts a button.
    /// The button can be a trigger for anything from opening a simple link to starting a complex workflow.
    /// </summary>
    /// <remarks>Works with block types: Section,Actions</remarks>
    public class Button : ActionElement
    {
        internal const string TextJsonName = "text";
        internal const string UrlJsonName = "url";
        internal const string ValueJsonName = "value";
        internal const string StyleJsonName = "style";
        internal const string ConfirmDialogJsonName = "confirm";

        /// <summary>
        /// Element type value for the <see cref="Button"/> element.
        /// </summary>
        public const string ElementTypeValue = "button";

        /// <summary>
        /// A text object that defines the button's text.
        /// Can only be of type: plain_text.
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 75 characters.</remarks>
        [JsonPropertyName(TextJsonName)]
        public PlainText? Text { get; set; }

        /// <summary>
        /// A URL to load in the user's browser when the button is clicked
        /// </summary>
        /// <remarks>Maximum length for this field is 3000 characters.</remarks>
        [JsonPropertyName(UrlJsonName)]
        public string? Url { get; set; }

        /// <summary>
        /// The value to send along with the interaction payload.
        /// </summary>
        /// <remarks>Maximum length for this field is 2000 characters.</remarks>
        [JsonPropertyName(ValueJsonName)]
        public string? Value { get; set; }

        /// <summary>
        /// Decorates buttons with alternative visual color schemes.
        /// </summary>
        /// <remarks>If you don't include this field, the default button style will be used.</remarks>
        [JsonPropertyName(StyleJsonName)]
        public ElementStyle Style { get; set; }

        /// <summary>
        /// A confirm object that defines an optional confirmation dialog after the button is clicked.
        /// </summary>
        [JsonPropertyName(ConfirmDialogJsonName)]
        public ConfirmDialog? ConfirmDialog { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        public Button()
            => ElementType = ElementTypeValue;
    }
}

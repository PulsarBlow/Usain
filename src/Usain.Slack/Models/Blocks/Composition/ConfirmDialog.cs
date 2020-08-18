namespace Usain.Slack.Models.Blocks.Composition
{
    using System.Text.Json.Serialization;
    using Elements;

    /// <summary>
    /// An object that defines a dialog that provides a confirmation step to any interactive element.
    /// This dialog will ask the user to confirm their action by offering a confirm and deny buttons.
    /// </summary>
    public class ConfirmDialog
    {
        internal const string TitleJsonName = "title";
        internal const string TextJsonName = "text";
        internal const string ConfirmJsonName = "confirm";
        internal const string CancelJsonName = "deny";
        internal const string StyleJsonName = "style";

        /// <summary>
        /// A plain_text-only text object that defines the dialog's title.
        /// </summary>
        /// <remarks>Maximum length for this field is 100 characters</remarks>
        [JsonPropertyName(TitleJsonName)]
        public PlainText? Title { get; set; }

        /// <summary>
        /// A text object that defines the explanatory text that appears in the confirm dialog.
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 300 characters.</remarks>
        [JsonPropertyName(TextJsonName)]
        public TextElement? Text { get; set; }

        /// <summary>
        /// A plain_text-only text object to define the text of the button that confirms the action.
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 30 characters.</remarks>
        [JsonPropertyName(ConfirmJsonName)]
        public PlainText? Confirm { get; set; }

        /// <summary>
        /// A plain_text-only text object to define the text of the button that cancels the action.
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 30 characters.</remarks>
        [JsonPropertyName(CancelJsonName)]
        public PlainText? Cancel { get; set; }

        /// <summary>
        ///	Defines the color scheme applied to the confirm button.
        /// A value of danger will display the button with a red background on desktop, or red text on mobile.
        /// A value of primary will display the button with a green background on desktop, or blue text on mobile.
        /// </summary>
        /// <remarks>f this field is not provided, the default value will be primary.</remarks>
        [JsonPropertyName(StyleJsonName)]
        public ElementStyle Style { get; set; }
    }
}

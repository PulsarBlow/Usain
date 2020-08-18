namespace Usain.Slack.Models.Blocks.Elements
{
    using System.Text.Json.Serialization;
    using Composition;

    /// <summary>
    /// Works with block types: Section, Actions, Input
    /// An element which lets users easily select a date from a calendar style UI.
    /// </summary>
    /// <example>https://api.slack.com/reference/block-kit/block-elements#datepicker</example>
    public class DatePicker: ActionElement
    {
        internal const string PlaceholderJsonName = "placeholder";
        internal const string SelectedDateJsonName = "initial_date";
        internal const string ConfirmDialogJsonName = "confirm";

        /// <summary>
        /// Element type value for the <see cref="DatePicker"/> element.
        /// </summary>
        public const string ElementTypeValue = "datepicker";

        /// <summary>
        /// A plain_text only text object that defines the placeholder text shown on the datepicker.
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 150 characters.</remarks>
        [JsonPropertyName(PlaceholderJsonName)]
        public PlainText? Placeholder { get; set; }

        /// <summary>
        /// The initial date that is selected when the element is loaded.
        /// </summary>
        /// <remarks>This should be in the format: YYYY-MM-DD</remarks>
        [JsonPropertyName(SelectedDateJsonName)]
        public string? SelectedDate { get; set; }

        /// <summary>
        /// A confirm object that defines an optional confirmation dialog that appears after a date is selected.
        /// </summary>
        [JsonPropertyName(ConfirmDialogJsonName)]
        public ConfirmDialog? ConfirmDialog { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DatePicker"/> class.
        /// </summary>
        public DatePicker()
            => ElementType = ElementTypeValue;
    }
}

namespace Usain.Slack.Models.Blocks.Elements
{
    using System.Text.Json.Serialization;
    using Composition;

    /// <summary>
    /// Works with block types: Section, Actions, Input
    /// A radio button group that allows a user to choose one item from a list of
    /// possible options.
    /// </summary>
    /// <remarks>https://api.slack.com/reference/block-kit/block-elements#input</remarks>
    public class RadioButtonGroup : ActionElement
    {
        internal const string OptionsJsonName = "options";
        internal const string SelectedOptionJsonName = "initial_option";
        internal const string ConfirmDialogJsonName = "confirm";

        /// <summary>
        /// Element type value for the <see cref="RadioButtonGroup"/> element.
        /// </summary>
        public const string ElementTypeValue = "radio_buttons";

        /// <summary>
        /// An array of option objects.
        /// </summary>
        [JsonPropertyName(OptionsJsonName)]
        public Option[]? Options { get; set; }

        /// <summary>
        /// An option object that exactly matches one of the options within options. This option will be selected when the radio button group initially loads.
        /// </summary>
        [JsonPropertyName(SelectedOptionJsonName)]
        public Option? SelectedOption { get; set; }

        /// <summary>
        /// A confirm object that defines an optional confirmation dialog that appears after clicking one of the radio buttons in this element.
        /// </summary>
        [JsonPropertyName(ConfirmDialogJsonName)]
        public ConfirmDialog? ConfirmDialog { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonGroup"/> class.
        /// </summary>
        public RadioButtonGroup()
            => ElementType = ElementTypeValue;
    }
}

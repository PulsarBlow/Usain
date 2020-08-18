namespace Usain.Slack.Models.Blocks
{
    using System.Text.Json.Serialization;
    using Elements;

    /// <summary>
    /// A block that collects information from users.
    /// It can hold a plain-text input element, a select menu element, a multi-select menu element, or a datepicker.
    /// </summary>
    /// <remarks>Available in surfaces: Modals</remarks>
    /// <remarks>https://api.slack.com/reference/block-kit/blocks#input</remarks>
    public class InputBlock : Block
    {
        internal const string LabelJsonName = "label";
        internal const string ElementJsonName = "element";
        internal const string HintJsonName = "hint";
        internal const string OptionalJsonName = "optional";

        /// <summary>
        /// Block type value for the <see cref="InputBlock"/> class.
        /// </summary>
        public const string BlockTypeValue = "input";

        /// <summary>
        /// A label that appears above an input element in the form of a text object that must have type of plain_text.
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 2000 characters.</remarks>
        [JsonPropertyName(LabelJsonName)]
        public PlainText? Label { get; set; }

        /// <summary>
        /// An optional hint that appears below an input element in a lighter grey.
        /// It must be a a text object with a type of plain_text
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 2000 characters.</remarks>
        [JsonPropertyName(HintJsonName)]
        public PlainText? Hint { get; set; }

        /// <summary>
        /// A boolean that indicates whether the input element may be empty when a user submits the modal.
        /// </summary>
        /// <remarks>Defaults to false.</remarks>
        [JsonPropertyName(OptionalJsonName)]
        public bool Optional { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputBlock"/> class.
        /// </summary>
        public InputBlock()
            => BlockType = BlockTypeValue;
    }
}

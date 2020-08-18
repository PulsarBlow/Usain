namespace Usain.Slack.Models.Blocks.Elements
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Works with block types: Section, Actions, Input
    /// A plain-text input, similar to the HTML &lt;input&gt; tag, creates a field where a
    /// user can enter freeform data.It can appear as a single-line field or a
    /// larger textarea using the multiline flag.
    /// </summary>
    /// <remarks>https://api.slack.com/reference/block-kit/block-elements#input</remarks>
    public class PlainTextInput : ActionElement
    {
        internal const string PlaceholderJsonName = "placeholder";
        internal const string InitialValueJsonName = "initial_value";
        internal const string MultilineJsonName = "multiline";
        internal const string MinLengthJsonName = "min_length";
        internal const string MaxLengthJsonName = "max_length";

        /// <summary>
        /// Element type value of the <see cref="PlainTextInput"/> element.
        /// </summary>
        public const string ElementTypeValue = "plain_text_input";

        /// <summary>
        /// A plain_text only text object that defines the placeholder text shown in the plain-text input.
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 150 characters.</remarks>
        [JsonPropertyName(PlaceholderJsonName)]
        public PlainText? Placeholder { get; set; }

        /// <summary>
        /// The initial value in the plain-text input when it is loaded.
        /// </summary>
        [JsonPropertyName(InitialValueJsonName)]
        public string? InitialValue { get; set; }

        /// <summary>
        /// Indicates whether the input will be a single line (false) or a larger textarea (true).
        /// </summary>
        /// <remarks>Defaults to false.</remarks>
        [JsonPropertyName(MultilineJsonName)]
        public bool Multiline { get; set; }

        /// <summary>
        /// The minimum length of input that the user must provide.
        /// If the user provides less, they will receive an error.
        /// </summary>
        /// <remarks>Maximum value is 3000.</remarks>
        [JsonPropertyName(MinLengthJsonName)]
        public int MinLength { get; set; }

        /// <summary>
        /// The maximum length of input that the user can provide.
        /// If the user provides more, they will receive an error.
        /// </summary>
        [JsonPropertyName(MaxLengthJsonName)]
        public int MaxLength { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlainTextInput"/> class.
        /// </summary>
        public PlainTextInput()
            => ElementType = ElementTypeValue;
    }
}

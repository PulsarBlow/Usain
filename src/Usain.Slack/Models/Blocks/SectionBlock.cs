namespace Usain.Slack.Models.Blocks
{
    using System.Text.Json.Serialization;
    using Elements;

    /// <summary>
    /// A section is one of the most flexible blocks available.
    /// It can be used as a simple text block, in combination with text fields,
    /// or side-by-side with any of the available block elements.
    /// </summary>
    /// <remarks>Available in surfaces: Modals Messages Home tabs</remarks>
    /// <remarks>https://api.slack.com/reference/block-kit/blocks#section</remarks>
    public class SectionBlock : Block
    {
        internal const string TextJsonName = "text";
        internal const string FieldsJsonName = "fields";
        internal const string AccessoryJsonName = "accessory";

        /// <summary>
        /// Block type value for the <see cref="SectionBlock"/> block.
        /// </summary>
        public const string BlockTypeValue = "section";

        /// <summary>
        /// The text for the block, in the form of a text object.
        /// This field is not required if a valid array of fields objects is provided instead.
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 3000 characters.</remarks>
        [JsonPropertyName(TextJsonName)]
        public TextElement? Text { get; set; }

        /// <summary>
        /// An array of text objects. Any text objects included with fields
        /// will be rendered in a compact format that allows for 2 columns of side-by-side text.
        /// </summary>
        /// <remarks>Maximum number of items is 10.</remarks>
        /// <remarks>Maximum length for the text in each item is 2000 characters.</remarks>
        [JsonPropertyName(FieldsJsonName)]
        public TextElement[]? Fields { get; set; }

        /// <summary>
        /// One of the available element objects.
        /// </summary>
        [JsonPropertyName(AccessoryJsonName)]
        public Element? Accessory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionBlock"/> class.
        /// </summary>
        public SectionBlock()
            => BlockType = BlockTypeValue;
    }
}

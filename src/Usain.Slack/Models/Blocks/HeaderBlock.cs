namespace Usain.Slack.Models.Blocks
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A header is a plain-text block that displays in a larger, bold font.
    /// Use it to delineate between different groups of content in your app's surfaces.
    /// </summary>
    /// <remarks>Available in surfaces: Modals Messages Home tabs</remarks>
    /// <remarks>https://api.slack.com/reference/block-kit/blocks#header</remarks>
    public class HeaderBlock : Block
    {
        internal const string TextJsonName = "text";

        /// <summary>
        /// Block type value for the <see cref="HeaderBlock"/> block.
        /// </summary>
        public const string BlockTypeValue = "header";

        /// <summary>
        /// The text for the block, in the form of a plain_text text object.
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 3000 characters.</remarks>
        [JsonPropertyName(TextJsonName)]
        public string? Text { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderBlock"/> class.
        /// </summary>
        public HeaderBlock()
            => BlockType = BlockTypeValue;
    }
}

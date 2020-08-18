namespace Usain.Slack.Models.Blocks
{
    using System.Text.Json.Serialization;
    using Elements;

    /// <summary>
    /// A simple image block, designed to make photos really pop.
    /// </summary>
    /// <remarks>Available in surfaces: Modals Messages Home tabs</remarks>
    /// <remarks>https://api.slack.com/reference/block-kit/blocks#image</remarks>
    public class ImageBlock : Block
    {
        internal const string ImageUrlJsonName = "image_url";
        internal const string AltTextJsonName = "alt_text";
        internal const string TitleJsonName = "title";

        /// <summary>
        /// Block type value for the <see cref="ImageBlock"/> block.
        /// </summary>
        public const string BlockTypeValue = "image";

        /// <summary>
        /// The URL of the image to be displayed.
        /// </summary>
        /// <remarks>Maximum length for this field is 3000 characters.</remarks>
        [JsonPropertyName(ImageUrlJsonName)]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// A plain-text summary of the image.
        /// This should not contain any markup.
        /// </summary>
        /// <remarks>Maximum length for this field is 2000 characters.</remarks>
        [JsonPropertyName(AltTextJsonName)]
        public string? AltText { get; set; }

        /// <summary>
        /// An optional title for the image in the form of a <see cref="PlainText"/>
        /// that can only be of type: plain_text.
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 2000 characters.</remarks>
        [JsonPropertyName(TitleJsonName)]
        public PlainText? Title { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageBlock"/> class.
        /// </summary>
        public ImageBlock()
            => BlockType = BlockTypeValue;
    }
}

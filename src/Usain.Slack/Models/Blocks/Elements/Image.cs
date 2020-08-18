namespace Usain.Slack.Models.Blocks.Elements
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// An element to insert an image as part of a larger block of content.
    /// Works with block types: Section, Context
    /// </summary>
    public class Image : Element
    {
        internal const string ImageUrlJsonName = "image_url";
        internal const string AltTextJsonName = "alt_text";

        /// <summary>
        /// Element type value for the <see cref="Image"/> element.
        /// </summary>
        public const string ElementTypeValue = "image";

        /// <summary>
        /// The URL of the image to be displayed.
        /// </summary>
        /// <example>http://placekitten.com/700/500</example>
        [JsonPropertyName(ImageUrlJsonName)]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// A plain-text summary of the image. This should not contain any markup.
        /// </summary>
        /// <example>Multiple cute kittens</example>
        [JsonPropertyName(AltTextJsonName)]
        public string? AltText { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image()
            => ElementType = ElementTypeValue;
    }
}

namespace Usain.Slack.Models.Blocks.Elements
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// An object containing some text, formatted as plain text.
    /// </summary>
    public class PlainText : TextElement
    {
        internal const string EmojiJsonName = "emoji";

        /// <summary>
        /// Element type value for the <see cref="PlainText"/> element.
        /// </summary>
        public const string ElementTypeValue = "plain_text";

        /// <summary>
        /// Indicates whether emojis in a text field should be escaped into the colon emoji format.
        /// </summary>
        [JsonPropertyName(EmojiJsonName)]
        public bool Emoji { get; set; }

        /// <summary>
        /// Initializes a new <see cref="PlainText"/> class.
        /// </summary>
        public PlainText()
            => ElementType = ElementTypeValue;
    }
}

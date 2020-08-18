namespace Usain.Slack.Models.Blocks.Elements
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// An object containing some text, formatted as plain text.
    /// </summary>
    public class MarkdownText : TextElement
    {
        internal const string VerbatimJsonName = "verbatim";

        /// <summary>
        /// Element type value for the <see cref="MarkdownText"/> element.
        /// </summary>
        public const string ElementTypeValue = "mrkdwn";

        /// <summary>
        /// When set to false (as is default) URLs will be auto-converted into links,
        /// conversation names will be link-ified, and certain mentions will be automatically parsed.
        /// Using a value of true will skip any preprocessing of this nature,
        /// although you can still include manual parsing strings.
        /// </summary>
        [JsonPropertyName(VerbatimJsonName)]
        public bool Verbatim { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownText"/> class.
        /// </summary>
        public MarkdownText()
            => ElementType = ElementTypeValue;
    }
}

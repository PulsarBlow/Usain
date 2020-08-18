namespace Usain.Slack.Models.Blocks.Elements
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// An object containing some text, formatted either as plain_text or using mrkdwn
    /// </summary>
    public class TextElement : Element
    {
        internal const string TextJsonName = "text";

        /// <summary>
        /// The text for the element.
        /// This field accepts any of the standard text formatting markup when type is mrkdwn.
        /// </summary>
        /// <remarks>https://api.slack.com/reference/surfaces/formatting</remarks>
        [JsonPropertyName(TextJsonName)]
        public string? Text { get; set; }
    }
}

namespace Usain.Slack.Models.Blocks.Composition
{
    using System.Text.Json.Serialization;
    using Elements;

    /// <summary>
    /// Provides a way to group options in a select menu or multi-select menu.
    /// </summary>
    public class OptionGroup
    {
        internal const string LabelJsonName = "label";
        internal const string OptionsJsonName = "options";

        /// <summary>
        /// A plain_text only text object that defines the label shown above this group of options.
        /// </summary>
        /// <remarks>Maximum length for the text in this field is 75 characters.</remarks>
        [JsonPropertyName(LabelJsonName)]
        public PlainText? Label { get; set; }

        /// <summary>
        /// An array of option objects that belong to this specific group.
        /// </summary>
        /// <remarks>Maximum of 100 items.</remarks>
        [JsonPropertyName(OptionsJsonName)]
        public Option[]? Options { get; set; }
    }
}

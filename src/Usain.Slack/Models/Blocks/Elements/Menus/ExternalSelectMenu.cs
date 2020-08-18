namespace Usain.Slack.Models.Blocks.Elements.Menus
{
    using System.Text.Json.Serialization;
    using Composition;

    /// <summary>
    /// Works with block types: Section, Actions, Input.
    /// This select menu will load its options from an external data source,
    /// allowing for a dynamic list of options.
    /// </summary>
    /// <remarks>https://api.slack.com/reference/block-kit/block-elements#external_select</remarks>
    public class ExternalSelectMenu : SelectMenu
    {
        internal const string SelectedOptionJsonName = "initial_option";
        internal const string MinQueryLengthJsonName = "min_query_length";

        /// <summary>
        /// Element type value for the <see cref="ExternalSelectMenu"/> element.
        /// </summary>
        public const string ElementTypeValue = "external_select";

        /// <summary>
        /// A single option that exactly matches one of the options within the options or option_groups
        /// loaded from the external data source.
        /// This option will be selected when the menu initially loads.
        /// </summary>
        [JsonPropertyName(SelectedOptionJsonName)]
        public Option? SelectedOption { get; set; }

        /// <summary>
        /// When the typeahead field is used, a request will be sent on every character change.
        /// If you prefer fewer requests or more fully ideated queries,
        /// use the min_query_length attribute to tell Slack the fewest number of typed characters required before dispatch.
        /// </summary>
        /// <remarks>The default value is 3.</remarks>
        [JsonPropertyName(MinQueryLengthJsonName)]
        public int MinQueryLength { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalSelectMenu"/> class.
        /// </summary>
        public ExternalSelectMenu()
            => ElementType = ElementTypeValue;
    }
}

namespace Usain.Slack.Models.Blocks.Elements.Menus
{
    using System.Text.Json.Serialization;
    using Composition;

    /// <summary>
    /// Works with block types: Section, Input
    /// This menu will load its options from an external data source, allowing for a
    /// dynamic list of options.
    /// </summary>
    /// <remarks>https://api.slack.com/reference/block-kit/block-elements#external_multi_select</remarks>
    public class ExternalMultiSelectMenu : MultiSelectMenu
    {
        internal const string MinQueryLengthJsonName = "min_query_length";
        internal const string SelectedOptionsJsonName = "initial_options";

        /// <summary>
        /// Element type value for the <see cref="ExternalMultiSelectMenu"/> element.
        /// </summary>
        public const string ElementTypeValue = "multi_external_select";

        /// <summary>
        /// When the typeahead field is used, a request will be sent on every character change.
        /// If you prefer fewer requests or more fully ideated queries,
        /// use the min_query_length attribute to tell Slack the fewest number
        /// of typed characters required before dispatch.
        /// </summary>
        /// <remarks>The default value is 3.</remarks>
        [JsonPropertyName(MinQueryLengthJsonName)]
        public int MinQueryLength { get; set; } = 3;

        /// <summary>
        /// An array of option objects that exactly match one or more
        /// of the options within options or option_groups.
        /// These options will be selected when the menu initially loads.
        /// </summary>
        [JsonPropertyName(SelectedOptionsJsonName)]
        public Option[]? SelectedOptions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalMultiSelectMenu"/> class.
        /// </summary>
        public ExternalMultiSelectMenu()
            => ElementType = ElementTypeValue;
    }
}

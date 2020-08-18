namespace Usain.Slack.Models.Blocks.Elements.Menus
{
    using System.Text.Json.Serialization;
    using Composition;

    /// <summary>
    /// Works with block types: Section, Actions, Input
    /// This is the simplest form of select menu, with a static list of options
    /// passed in when defining the element.
    /// </summary>
    /// <remarks>https://api.slack.com/reference/block-kit/block-elements#static_select</remarks>
    public class StaticSelectMenu : SelectMenu
    {
        internal const string OptionsJsonName = "options";
        internal const string OptionGroupsJsonName = "option_groups";
        internal const string SelectedOptionJsonName = "initial_option";

        /// <summary>
        /// Element type value for <see cref="StaticSelectMenu"/> element.
        /// </summary>
        public const string ElementTypeValue = "static_select";

        /// <summary>
        /// An array of option objects.
        /// If option_groups is specified, this field should not be.
        /// </summary>
        /// <remarks>Maximum number of options is 100.</remarks>
        [JsonPropertyName(OptionsJsonName)]
        public Option[]? Options { get; set; }

        /// <summary>
        /// An array of option group objects.
        /// If options is specified, this field should not be.
        /// </summary>
        /// <remarks>Maximum number of option groups is 100. </remarks>
        [JsonPropertyName(OptionGroupsJsonName)]
        public OptionGroup[]? OptionGroups { get; set; }

        /// <summary>
        /// A single option that exactly matches one of the options within options or option_groups.
        /// This option will be selected when the menu initially loads.
        /// </summary>
        [JsonPropertyName(SelectedOptionJsonName)]
        public Option? SelectedOption { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticSelectMenu"/> class.
        /// </summary>
        public StaticSelectMenu()
            => ElementType = ElementTypeValue;
    }
}

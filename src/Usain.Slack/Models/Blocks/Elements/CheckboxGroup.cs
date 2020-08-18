namespace Usain.Slack.Models.Blocks.Elements
{
    using System.Text.Json.Serialization;
    using Composition;

    /// <summary>
    /// Works with block types: Section, Actions, Input
    /// A checkbox group that allows a user to choose multiple items from a list of
    /// possible options.
    /// </summary>
    /// <remarks>Checkboxes are only supported in the following app surfaces: Home, tabs, Modals</remarks>
    /// <example>https://api.slack.com/reference/block-kit/block-elements#checkboxes</example>
    public class CheckboxGroup : ActionElement
    {
        internal const string OptionsJsonName = "options";
        internal const string SelectedOptionsJsonName = "initial_options";
        internal const string ConfirmJsonName = "confirm";

        /// <summary>
        /// Element type value for <see cref="CheckboxGroup"/>
        /// </summary>
        public const string ElementTypeValue = "checkboxes";

        /// <summary>
        /// An array of <see cref="Option"/> objects.
        /// </summary>
        [JsonPropertyName(OptionsJsonName)]
        public Option[]? Options { get; set; }

        /// <summary>
        /// An array of option objects that exactly matches one or more of the options within.
        /// These options will be selected when the checkbox group initially loads.
        /// </summary>
        [JsonPropertyName(SelectedOptionsJsonName)]
        public Option[]? SelectedOptions { get; set; }

        /// <summary>
        /// A confirm object that defines an optional confirmation dialog
        /// that appears after clicking one of the checkboxes in this element.
        /// </summary>
        [JsonPropertyName(ConfirmJsonName)]
        public ConfirmDialog? ConfirmDialog { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckboxGroup"/> class.
        /// </summary>
        public CheckboxGroup()
            => ElementType = ElementTypeValue;
    }
}

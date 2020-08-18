namespace Usain.Slack.Models.Blocks.Elements.Menus
{
    using System.Text.Json.Serialization;
    using Composition;

    public class MultiSelectMenu : ActionElement
    {
        internal const string PlaceholderJsonName = "placeholder";
        internal const string ConfirmDialogJsonName = "confirm";
        internal const string MaxSelectedItemsJsonName =
            "max_selected_items";

        /// <summary>
        /// A plain_text only text object that defines the placeholder text shown on the menu.
        /// </summary>
        [JsonPropertyName(PlaceholderJsonName)]
        public PlainText? Placeholder { get; set; }

        /// <summary>
        /// A confirm object that defines an optional confirmation dialog
        /// that appears before the multi-select choices are submitted.
        /// </summary>
        [JsonPropertyName(ConfirmDialogJsonName)]
        public ConfirmDialog? ConfirmDialog { get; set; }

        /// <summary>
        /// Specifies the maximum number of items that can be selected in the menu.
        /// </summary>
        /// <remarks>Minimum number is 1.</remarks>
        [JsonPropertyName(MaxSelectedItemsJsonName)]
        public int MaxSelectedItems { get; set; }
    }
}

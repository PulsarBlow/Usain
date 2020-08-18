namespace Usain.Slack.Models.Blocks.Elements.Menus
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Works with block types: Section, Input
    /// This multi-select menu will populate its options with a list
    /// of public channels visible to the current user in the active workspace.
    /// </summary>
    /// <remarks>https://api.slack.com/reference/block-kit/block-elements#channel_multi_select</remarks>
    public class ChannelMultiSelectMenu : MultiSelectMenu
    {
        internal const string SelectedChannelsJsonName = "initial_channels";

        /// <summary>
        /// Element type value for <see cref="ChannelMultiSelectMenu"/> element.
        /// </summary>
        public const string ElementTypeValue = "multi_channels_select";

        /// <summary>
        /// An array of one or more IDs of any valid public channel to be pre-selected when the menu loads.
        /// </summary>
        [JsonPropertyName(SelectedChannelsJsonName)]
        public string[]? SelectedChannels { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelMultiSelectMenu"/> class.
        /// </summary>
        public ChannelMultiSelectMenu()
            => ElementType = ElementTypeValue;
    }
}

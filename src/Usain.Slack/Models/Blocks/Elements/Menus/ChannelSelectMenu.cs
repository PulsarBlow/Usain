namespace Usain.Slack.Models.Blocks.Elements.Menus
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Works with block types: Section, Actions, Input.
    /// This select menu will populate its options with a list of public channels
    /// visible to the current user in the active workspace.
    /// </summary>
    public class ChannelSelectMenu : SelectMenu
    {
        internal const string SelectedChannelJsonName = "initial_channel";
        internal const string ResponseUrlEnabledJsonName =
            "response_url_enabled";

        /// <summary>
        /// Element type value for the <see cref="ChannelSelectMenu"/> element.
        /// </summary>
        public const string ElementTypeValue = "channels_select";

        /// <summary>
        /// The ID of any valid public channel to be pre-selected when the menu loads.
        /// </summary>
        [JsonPropertyName(SelectedChannelJsonName)]
        public string? SelectedChannel { get; set; }

        /// <summary>
        /// When set to true, the view_submission payload from the menu's parent
        /// view will contain a response_url.This response_url can be
        /// used for message responses.The target channel for the message will
        /// be determined by the value of this select menu.
        /// </summary>
        /// <remarks>This field only works with menus in input blocks in modals.</remarks>
        [JsonPropertyName(ResponseUrlEnabledJsonName)]
        public bool ResponseUrlEnabled { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelSelectMenu"/> class.
        /// </summary>
        public ChannelSelectMenu()
            => ElementType = ElementTypeValue;
    }
}

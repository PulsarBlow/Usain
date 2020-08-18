namespace Usain.Slack.Models.Blocks
{
    using System.Text.Json.Serialization;
    using Elements;

    /// <summary>
    /// A block that is used to hold interactive elements.
    /// </summary>
    /// <remarks>Available in surfaces: Modals, Messages, Home tabs.</remarks>
    /// <remarks>https://api.slack.com/reference/block-kit/blocks#actions</remarks>
    public class ActionsBlock : Block
    {
        internal const string ActionElementsJsonName = "elements";

        /// <summary>
        /// Block type name for <see cref="ActionsBlock"/> block.
        /// </summary>
        public const string BlockTypeValue = "actions";

        /// <summary>
        /// An array of interactive element objects - buttons, select menus, overflow menus, or date pickers.
        /// </summary>
        /// <remarks>There is a maximum of 5 elements in each action block.</remarks>
        [JsonPropertyName(ActionElementsJsonName)]
        public ActionElement[]? ActionElements { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionsBlock"/> class.
        /// </summary>
        public ActionsBlock()
            => BlockType = BlockTypeValue;
    }
}

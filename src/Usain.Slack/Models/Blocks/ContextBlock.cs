namespace Usain.Slack.Models.Blocks
{
    using System.Text.Json.Serialization;
    using Elements;

    /// <summary>
    /// Displays message context, which can include both images and text.
    /// </summary>
    /// <remarks>Available in surfaces: Modals Messages Home tabs</remarks>
    /// <remarks>https://api.slack.com/reference/block-kit/blocks#context</remarks>
    public class ContextBlock : Block
    {
        internal const string ElementsJsonName = "elements";

        /// <summary>
        /// The type of block. For a context block, type is always context.
        /// </summary>
        public const string BlockTypeValue = "context";

        /// <summary>
        /// An array of <see cref="Image"/> elements and <see cref="TextElement"/> objects.
        /// </summary>
        /// <remarks>Maximum number of items is 10.</remarks>
        // TODO: We should introduce a type constraint there, in order to avoid
        // being able to add an element not supported by the contextblock.
        // Using an interface (IContextBlockElement) would have done the trick,
        // but infortunately, the jsonconverter attribute does not support interface type.
        // Thus, this requires a more deep rework, like a converter for the collection,
        // or a converter added directly on the jsonserializeroption.
        [JsonPropertyName(ElementsJsonName)]
        public Element[]? Elements { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextBlock"/> class.
        /// </summary>
        public ContextBlock()
            => BlockType = BlockTypeValue;
    }
}

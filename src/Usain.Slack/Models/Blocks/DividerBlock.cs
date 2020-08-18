namespace Usain.Slack.Models.Blocks
{
    /// <summary>
    /// A content divider, like an &lt;hr&gt;, to split up different blocks inside of a message.
    /// The divider block is nice and neat, requiring only a type.
    /// </summary>
    /// <remarks>Available in surfaces: Modals Messages Home tabs</remarks>
    /// <remarks>https://api.slack.com/reference/block-kit/blocks#divider</remarks>
    public class DividerBlock : Block
    {
        /// <summary>
        /// Block type value for the <see cref="DividerBlock"/> block.
        /// </summary>
        public const string BlockTypeValue = "divider";

        /// <summary>
        /// Initializes a new instance of the <see cref="DividerBlock"/> class.
        /// </summary>
        public DividerBlock()
            => BlockType = BlockTypeValue;
    }
}

namespace Usain.Slack.Models.Blocks
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using JsonConverters;

    [JsonConverter(typeof(BlockJsonConverter))]
    public class Block
    {
        internal const string BlockTypeJsonName = "type";
        internal const string BlockIdJsonName = "block_id";
        internal const string DefaultBlockTypeValue = "unknown";

        /// <summary>
        /// The type of block.
        /// </summary>
        [JsonPropertyName(BlockTypeJsonName)]
        public string BlockType { get; set; } = DefaultBlockTypeValue;

        /// <summary>
        /// A string acting as a unique identifier for a block.
        /// If not specified, one will be generated.
        /// block_id should be unique for each message and each iteration of a message.
        /// If a message is updated, use a new block_id
        /// </summary>
        /// <remarks>Maximum length for this field is 255 characters. </remarks>
        [JsonPropertyName(BlockIdJsonName)]
        public string? BlockId { get; set; }

        /// <summary>
        /// Extra json properties not directly mapped to this type definition
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, object> ExtraFields { get; set; } =
            new Dictionary<string, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Block"/> class.
        /// </summary>
        public Block()
            => BlockType = DefaultBlockTypeValue;
    }
}

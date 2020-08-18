namespace Usain.Slack.Models.Blocks
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Displays a remote file. You can't add this block to app surfaces directly,
    /// but it will show up when retrieving messages that contain remote files.
    /// </summary>
    /// <remarks>Appears in surfaces: Messages</remarks>
    /// <remarks>https://api.slack.com/reference/block-kit/blocks#file</remarks>
    public class FileBlock : Block
    {
        internal const string ExternalIdJsonName = "external_id";
        internal const string SourceJsonName = "source";

        /// <summary>
        /// Block type value for the <see cref="FileBlock"/> block.
        /// </summary>
        public const string BlockTypeValue = "file";

        /// <summary>
        /// The external unique ID for this file.
        /// </summary>
        [JsonPropertyName(ExternalIdJsonName)]
        public string? ExternalId { get; set; }

        /// <summary>
        /// At the moment, source will always be remote for a remote file.
        /// </summary>
        [JsonPropertyName(SourceJsonName)]
        public string? Source { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileBlock"/> class.
        /// </summary>
        public FileBlock()
            => BlockType = BlockTypeValue;
    }
}

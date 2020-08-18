namespace Usain.Slack.Models.Blocks.Elements
{
    using System.Text.Json.Serialization;
    using JsonConverters;

    [JsonConverter(typeof(ElementJsonConverter))]
    public abstract class Element
    {
        internal const string ElementTypeJsonName = "type";
        internal const string DefaultElementTypeValue = "unknown";

        /// <summary>
        /// The element type.
        /// </summary>
        [JsonPropertyName(ElementTypeJsonName)]
        public string ElementType { get; set; } = DefaultElementTypeValue;
    }
}

namespace Usain.Slack.Models.Blocks.Elements
{
    using System.Text.Json.Serialization;
    using JsonConverters;

    [JsonConverter(typeof(ElementStyleJsonConverter))]
    public enum ElementStyle
    {
        None,
        Primary,
        Danger,
    }
}

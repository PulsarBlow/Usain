namespace Usain.Slack.Models.Blocks.Composition
{
    using System.Text.Json.Serialization;
    using JsonConverters;

    /// <summary>
    /// Indicates type of conversations which should be included in the filter
    /// </summary>
    [JsonConverter(typeof(ConversationFilterOptionJsonConverter))]
    public enum ConversationFilterOption
    {
        DirectMessage,
        MultiPartyDirectMessage,
        Private,
        Public,
    }
}

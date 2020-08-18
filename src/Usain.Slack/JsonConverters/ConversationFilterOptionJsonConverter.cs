namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Models.Blocks.Composition;

    public class ConversationFilterOptionJsonConverter
        : JsonConverter<ConversationFilterOption>
    {
        internal const string DirectMessageStringValue = "im";
        internal const string MultiPartyDirectMessageStringValue = "mpim";
        internal const string PrivateStringValue = "private";
        internal const string PublicStringValue = "public";

        // case insensitivity is important
        private readonly Dictionary<string, ConversationFilterOption>
            _stringToTypeMap = new Dictionary<string, ConversationFilterOption>(
                StringComparer.OrdinalIgnoreCase)
            {
                [DirectMessageStringValue] =
                    ConversationFilterOption.DirectMessage,
                [MultiPartyDirectMessageStringValue] = ConversationFilterOption
                    .MultiPartyDirectMessage,
                [PrivateStringValue] = ConversationFilterOption.Private,
                [PublicStringValue] = ConversationFilterOption.Public,
            };

        public override ConversationFilterOption Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            string enumValue = reader.GetString();
            if (_stringToTypeMap.ContainsKey(enumValue))
            {
                return _stringToTypeMap[enumValue];
            }

            throw new JsonException($"Conversation filter option value `{enumValue}` is not supported.");
        }

        public override void Write(
            Utf8JsonWriter writer,
            ConversationFilterOption value,
            JsonSerializerOptions options)
        {
            switch (value)
            {
                case ConversationFilterOption.DirectMessage:
                    writer.WriteStringValue(DirectMessageStringValue);
                    return;
                case ConversationFilterOption.MultiPartyDirectMessage:
                    writer.WriteStringValue(MultiPartyDirectMessageStringValue);
                    return;
                case ConversationFilterOption.Private:
                    writer.WriteStringValue(PrivateStringValue);
                    return;
                case ConversationFilterOption.Public:
                    writer.WriteStringValue(PublicStringValue);
                    return;
                default:
                    writer.WriteNullValue();
                    return;
            }
        }
    }
}

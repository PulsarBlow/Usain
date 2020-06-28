namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Models;

    public class EventTimestampConverter : JsonConverter<EventTimestamp>
    {
        public override EventTimestamp Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            EventTimestamp.TryParse(reader.GetString(), out var eventTimestamp);
            return eventTimestamp;
        }

        public override void Write(
            Utf8JsonWriter writer,
            EventTimestamp value,
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

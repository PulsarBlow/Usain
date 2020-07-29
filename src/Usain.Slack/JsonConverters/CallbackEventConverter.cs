namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Models.Events.CallbackEvents;

    public class CallbackEventConverter : JsonConverter<CallbackEvent>
    {
        public override bool CanConvert(
            Type typeToConvert)
            => typeof(CallbackEvent).IsAssignableFrom(typeToConvert);

        public override CallbackEvent Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            var root = document.RootElement;
            if (!root.TryGetProperty(
                "type",
                out var property)) { throw new JsonException(); }

            var eventType = property.GetString();
            Type type = GetEventType(eventType);

            // Avoid infinite recursive behavior of the JsonSerializer
            // when returning CallbackEvent type (default case).
            // There is probably a better way.
            if (type == typeof(CallbackEvent))
            {
                return new CallbackEvent { Type = "not_supported" };
            }

            return (CallbackEvent) JsonSerializer.Deserialize(
                root.GetRawText(),
                type,
                options);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CallbackEvent value,
            JsonSerializerOptions options)
        {
            if (value is AppMentionEvent @event)
            {
                JsonSerializer.Serialize(
                    writer,
                    @event);
                return;
            }

            writer.WriteStartObject();
            writer.WriteString(CallbackEvent.TypePropertyName, value.Type);
            if (!value.EventTimestamp.IsEmpty)
            {
                writer.WriteString(
                    CallbackEvent.EventTimestampPropertyName,
                    value.EventTimestamp.ToString());
            }
            writer.WriteEndObject();
        }

        private static Type GetEventType(
            string eventType)
        {
            return eventType switch
            {
                AppMentionEvent.EventType => typeof(AppMentionEvent),
                _ => typeof(CallbackEvent),
            };
        }
    }
}

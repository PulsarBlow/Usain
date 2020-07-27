namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Models;

    public class EventBaseConverter : JsonConverter<Event>
    {
        public override bool CanConvert(
            Type typeToConvert)
            => typeof(Event).IsAssignableFrom(typeToConvert);

        public override Event Read(
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
            // when returning Event type (default case).
            // There is probably a better way.
            if (type == typeof(Event))
            {
                return new Event { Type = "not_supported" };
            }

            return (Event) JsonSerializer.Deserialize(
                root.GetRawText(),
                type,
                options);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Event value,
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private static Type GetEventType(
            string type)
        {
            return type switch
            {
                UrlVerificationEvent.EventType => typeof(UrlVerificationEvent),
                EventWrapper.EventType => typeof(EventWrapper),
                AppRateLimitedEvent.EventType => typeof(AppRateLimitedEvent),
                _ => typeof(Event),
            };
        }
    }
}

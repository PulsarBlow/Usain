namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Models;

    public class EventBaseConverter : JsonConverter<Event>
    {
        public override Event Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (typeToConvert != typeof(Event))
            {
                return (Event) JsonSerializer.Deserialize(ref reader,
                    typeToConvert, options);
            }

            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            var root = document.RootElement;
            var eventType = root.GetProperty("type").GetString();
            Type type = GetEventType(eventType);
            return (Event) JsonSerializer.Deserialize(root.GetRawText(),
                type, options);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Event value,
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private static Type GetEventType(string type)
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

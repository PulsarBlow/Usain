namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Models.Events;

    public class EventJsonConverter : JsonConverter<Event>
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
            var typeProvider = new EventJsonTypeResolver(root);
            var type = typeProvider.ResolveType();

            // Avoid infinite recursive behavior of the JsonSerializer
            // when returning Event type (default case of the type resolver).
            if (type == typeof(Event))
            {
                return new Event();
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
            var jsonWriter = new EventJsonWriter(
                writer,
                options);
            jsonWriter.Write(value);
        }
    }
}

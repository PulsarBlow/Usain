namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Models.Events.CallbackEvents;

    public class CallbackEventJsonConverter : JsonConverter<CallbackEvent>
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

            var typeProvider = new CallbackEventJsonTypeResolver(root);
            Type type = typeProvider.ResolveType();

            // Avoid infinite recursive behavior of the JsonSerializer
            // when returning CallbackEvent type (default case of the type resolver).
            if (type == typeof(CallbackEvent))
            {
                return new CallbackEvent { CallbackEventType = "unknown" };
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
            var jsonWriter = new CallbackEventJsonWriter(
                writer,
                options);
            jsonWriter.Write(value);
        }
    }
}

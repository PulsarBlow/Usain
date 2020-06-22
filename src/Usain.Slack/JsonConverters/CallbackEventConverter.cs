namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Models.CallbackEvents;

    public class CallbackEventConverter : JsonConverter<CallbackEvent>
    {
        public override CallbackEvent Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (typeToConvert != typeof(CallbackEvent)
                && typeToConvert != typeof(CallbackEvent))
            {
                return (CallbackEvent) JsonSerializer.Deserialize(ref reader,
                    typeToConvert, options);
            }

            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            var root = document.RootElement;
            var eventType = root.GetProperty("type").GetString();
            Type type = GetEventType(eventType);
            return (CallbackEvent) JsonSerializer.Deserialize(root.GetRawText(),
                type, options);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CallbackEvent value,
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private static Type GetEventType(string eventType)
        {
            return eventType switch
            {
                AppMentionEvent.EventType => typeof(AppMentionEvent),
                _ => typeof(CallbackEvent),
            };
        }
    }
}

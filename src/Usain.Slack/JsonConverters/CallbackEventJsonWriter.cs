namespace Usain.Slack.JsonConverters
{
    using System.Text.Json;
    using Models.Events.CallbackEvents;

    internal class CallbackEventJsonWriter
    {
        private readonly Utf8JsonWriter _jsonWriter;
        private readonly JsonSerializerOptions _options;

        public CallbackEventJsonWriter(
            Utf8JsonWriter jsonWriter,
            JsonSerializerOptions options)
        {
            _jsonWriter = jsonWriter;
            _options = options;
        }

        public void Write(
            CallbackEvent value)
        {
            switch (value)
            {
                case AppMentionEvent appMentionEvent:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        appMentionEvent,
                        _options);
                    return;
                case MeMessageEvent meMessageEvent:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        meMessageEvent,
                        _options);
                    return;
                case MessageChangedEvent messageChangedEvent:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        messageChangedEvent,
                        _options);
                    return;
                case MessageDeletedEvent messageDeletedEvent:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        messageDeletedEvent,
                        _options);
                    return;
                case MessageRepliedEvent messageRepliedEvent:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        messageRepliedEvent,
                        _options);
                    return;
                case MessageEvent messageEvent:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        messageEvent,
                        _options);
                    return;
            }

            WriteDefault(value);
        }

        private void WriteDefault(
            CallbackEvent value)
        {
            _jsonWriter.WriteStartObject();
            _jsonWriter.WriteString(
                CallbackEvent.EventTypeJsonName,
                value.CallbackEventType);
            if (!value.EventTimestamp.IsEmpty)
            {
                _jsonWriter.WriteString(
                    CallbackEvent.EventTimestampJsonName,
                    value.EventTimestamp.ToString());
            }

            _jsonWriter.WriteEndObject();
        }
    }
}

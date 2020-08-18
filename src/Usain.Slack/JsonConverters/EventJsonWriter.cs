namespace Usain.Slack.JsonConverters
{
    using System.Text.Json;
    using Models.Events;

    internal class EventJsonWriter
    {
        private readonly Utf8JsonWriter _jsonWriter;
        private readonly JsonSerializerOptions _options;

        public EventJsonWriter(
            Utf8JsonWriter jsonWriter,
            JsonSerializerOptions options)
        {
            _jsonWriter = jsonWriter;
            _options = options;
        }

        public void Write(
            Event @event)
        {
            switch (@event)
            {
                case UrlVerificationEvent urlVerificationEvent:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        urlVerificationEvent,
                        _options);
                    return;
                case EventWrapper eventWrapper:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        eventWrapper,
                        _options);
                    return;
                case AppRateLimitedEvent appRateLimitedEvent:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        appRateLimitedEvent,
                        _options);
                    return;
            }

            WriteDefault(@event);
        }

        private void WriteDefault(
            Event value)
        {
            _jsonWriter.WriteStartObject();
            _jsonWriter.WriteString(
                Event.EventTypeJsonName,
                value.EventType);
            _jsonWriter.WriteEndObject();
        }
    }
}

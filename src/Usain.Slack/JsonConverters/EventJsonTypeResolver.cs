namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using Models.Events;

    internal class EventJsonTypeResolver
    {
        private readonly JsonElement _jsonElement;

        public EventJsonTypeResolver(
            JsonElement jsonElement)
            => _jsonElement = jsonElement;

        public Type ResolveType()
        {
            if (!_jsonElement.TryGetProperty(
                Event.EventTypeJsonName,
                out var property)) { throw new JsonException(); }

            var typeValue = property.GetString();
            return typeValue switch
            {
                UrlVerificationEvent.EventTypeValue =>
                    typeof(UrlVerificationEvent),
                EventWrapper.EventTypeValue => typeof(EventWrapper),
                AppRateLimitedEvent.EventTypeValue =>
                    typeof(AppRateLimitedEvent),
                // We should throw an exception here.
                // We don't do it until the complete Slack Event API surface is covered,
                // otherwise we wouldn't be able to support unknown (not yet implemented) events.
                // This will certainly change in a future version.
                _ => typeof(Event),
            };
        }
    }
}

namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using Models;
    using Models.Events.CallbackEvents;

    internal class CallbackEventJsonTypeResolver
    {
        private readonly JsonElement _jsonElement;

        public CallbackEventJsonTypeResolver(
            JsonElement jsonElement)
            => _jsonElement = jsonElement;

        public Type ResolveType()
        {
            if (!_jsonElement.TryGetProperty(
                CallbackEvent.EventTypeJsonName,
                out var property)) { throw new JsonException(); }

            var eventType = property.GetString();
            return eventType switch
            {
                AppMentionEvent.CallbackEventTypeValue => typeof(AppMentionEvent),
                MessageEvent.CallbackEventTypeValue => GetMessageType(_jsonElement),
                // We should throw an exception here.
                // We don't do it until the complete Slack Event API surface is covered,
                // otherwise we wouldn't be able to support unknown (not yet implemented ) events.
                // This will certainly change in a future version.
                _ => typeof(CallbackEvent),
            };
        }

        private static Type GetMessageType(
            JsonElement root)
        {
            // This is a particular case for message_replied event type
            // to mitigate the current bug in the Slack Event API
            // see MessageRepliedEvent type documentation for more info.
            if (IsMessageReply(root)) { return typeof(MessageRepliedEvent); }

            if (!root.TryGetProperty(
                MessageEvent.SubTypeJsonName,
                out var property)) { return typeof(MessageEvent); }

            var subType = property.GetString();
            return subType switch
            {
                MeMessageEvent.MessageSubTypeValue => typeof(MeMessageEvent),
                MessageChangedEvent.MessageSubTypeValue =>
                typeof(MessageChangedEvent),
                MessageDeletedEvent.MessageSubTypeValue =>
                typeof(MessageDeletedEvent),
                // We should throw an exception here.
                // We don't do it until the complete Slack Event API surface is covered,
                // otherwise we wouldn't be able to support unknown (not yet implemented ) events.
                // This will certainly change in a future version.
                _ => typeof(MessageEvent),
            };
        }

        private static bool IsMessageReply(
            JsonElement jsonElement)
        {
            if (!jsonElement.TryGetProperty(
                MessageEvent.MessageIdJsonName,
                out var messageIdValue)) { return false; }

            if (!Timestamp.TryParse(
                messageIdValue.GetString(),
                out var messageId)) { return false; }

            if (!jsonElement.TryGetProperty(
                MessageEvent.ParentMessageIdJsonName,
                out var parentMessageIdValue)) { return false; }

            if (!Timestamp.TryParse(
                parentMessageIdValue.GetString(),
                out var parentMessageId)) { return false; }

            return messageId != parentMessageId;
        }
    }
}

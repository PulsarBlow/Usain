namespace Usain.Slack.Tests.JsonConverters
{
    using System;
    using System.Text.Json;
    using Slack.JsonConverters;
    using Slack.Models.Events.CallbackEvents;
    using Xunit;

    public class CallbackEventJsonTypeResolverTest
    {
        [Fact]
        public void ResolveType_Throws_When_Type_Property_Does_Not_Exist()
        {
            const string json = "{\"property\": \"value\"}";
            var jsonDocument = JsonDocument.Parse(json);
            var typeProvider =
                new CallbackEventJsonTypeResolver(jsonDocument.RootElement);

            Assert.Throws<JsonException>(() => typeProvider.ResolveType());
        }

        [Theory]
        [InlineData(
            CallbackEvent.DefaultCallbackEventTypeValue,
            typeof(CallbackEvent))]
        [InlineData(
            "whatever",
            typeof(CallbackEvent))]
        [InlineData(
            AppMentionEvent.CallbackEventTypeValue,
            typeof(AppMentionEvent))]
        [InlineData(
            MessageEvent.CallbackEventTypeValue,
            typeof(MessageEvent))]
        public void ResolveType_Returns_Expected_Type(
            string callbackEventTypeValue,
            Type expectedType)
        {
            var json =
                $"{{\"{CallbackEvent.EventTypeJsonName}\":\"{callbackEventTypeValue}\"}}";
            var jsonDocument = JsonDocument.Parse(json);
            var typeResolver =
                new CallbackEventJsonTypeResolver(jsonDocument.RootElement);

            var actualType = typeResolver.ResolveType();

            Assert.Equal(
                expectedType,
                actualType);
        }

        [Theory]
        [InlineData(
            null,
            typeof(MessageEvent))]
        [InlineData(
            "",
            typeof(MessageEvent))]
        [InlineData(
            " ",
            typeof(MessageEvent))]
        [InlineData(
            MeMessageEvent.MessageSubTypeValue,
            typeof(MeMessageEvent))]
        [InlineData(
            MessageChangedEvent.MessageSubTypeValue,
            typeof(MessageChangedEvent))]
        public void ResolveType_Returns_Expected_Message_Type(
            string messageSubTypeValue,
            Type expectedType)
        {
            var json =
                $"{{\"{CallbackEvent.EventTypeJsonName}\":\"{MessageEvent.CallbackEventTypeValue}\",\"{MessageEvent.SubTypeJsonName}\":\"{messageSubTypeValue}\"}}";
            var jsonDocument = JsonDocument.Parse(json);
            var typeResolver =
                new CallbackEventJsonTypeResolver(jsonDocument.RootElement);

            var actualType = typeResolver.ResolveType();

            Assert.Equal(
                expectedType,
                actualType);
        }

        [Theory]
        [InlineData(
            "",
            "",
            false)]
        [InlineData(
            "12345.001",
            "",
            false)]
        [InlineData(
            "",
            "12345.001",
            false)]
        [InlineData(
            "12345.001",
            "12345.001",
            false)]
        [InlineData(
            "12345.002",
            "12345.001",
            true)]
        public void ResolveType_Returns_Expected_MessageRepliedEvent(
            string messageId,
            string parentMessageId,
            bool isMessageReply)
        {
            // due to a bug in Slack Event API, subtype is always null for MessageRepliedEvent
            // see resolver implementation for more details.
            var typeToken =
                $"\"{CallbackEvent.EventTypeJsonName}\":\"{MessageEvent.CallbackEventTypeValue}\"";
            var subTypeToken = $"\"{MessageEvent.SubTypeJsonName}\":null";
            var messageIdToken =
                $"\"{MessageEvent.MessageIdJsonName}\":\"{messageId}\"";
            var parentMessageIdToken =
                $"\"{MessageEvent.ParentMessageIdJsonName}\":\"{parentMessageId}\"";

            var json =
                $"{{{typeToken},{subTypeToken},{messageIdToken},{parentMessageIdToken}}}";
            var jsonDocument = JsonDocument.Parse(json);
            var typeResolver =
                new CallbackEventJsonTypeResolver(jsonDocument.RootElement);

            var actualType = typeResolver.ResolveType();

            Assert.Equal(
                isMessageReply,
                actualType == typeof(MessageRepliedEvent));
        }
    }
}

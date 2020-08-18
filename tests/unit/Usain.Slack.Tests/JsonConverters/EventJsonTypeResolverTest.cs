namespace Usain.Slack.Tests.JsonConverters
{
    using System;
    using System.Text.Json;
    using Slack.JsonConverters;
    using Slack.Models.Events;
    using Xunit;

    public class EventJsonTypeResolverTest
    {
        [Theory]
        [InlineData(
            null,
            typeof(Event))]
        [InlineData(
            Event.DefaultEventTypeValue,
            typeof(Event))]
        [InlineData(
            AppRateLimitedEvent.EventTypeValue,
            typeof(AppRateLimitedEvent))]
        [InlineData(
            EventWrapper.EventTypeValue,
            typeof(EventWrapper))]
        public void ResolveType_Returns_Expected_Type(
            string eventTypeValue,
            Type expectedType)
        {
            var json = $"{{\"type\":\"{eventTypeValue}\"}}";
            var document = JsonDocument.Parse(json);
            var typeResolver = new EventJsonTypeResolver(document.RootElement);

            var actualType = typeResolver.ResolveType();

            Assert.Equal(
                expectedType,
                actualType);
        }

        [Fact]
        public void ResolveType_Throws_When_Type_Property_Does_Not_Exist()
        {
            var json = $"{{\"property\":\"value\"}}";
            var document = JsonDocument.Parse(json);
            var typeResolver = new EventJsonTypeResolver(document.RootElement);

            Assert.Throws<JsonException>(() => typeResolver.ResolveType());
        }
    }
}

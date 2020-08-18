namespace Usain.Slack.Tests.JsonConverters
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using Snapper;
    using Usain.Slack.JsonConverters;
    using Usain.Slack.Models.Events;
    using Usain.Slack.Models.Events.CallbackEvents;
    using Xunit;

    public class EventJsonConverterTest
    {
        private static readonly JsonSerializerOptions Options =
            new JsonSerializerOptions();

        [Theory]
        [InlineData(
            typeof(NotEvent),
            false)]
        [InlineData(
            typeof(CallbackEvent),
            false)]
        [InlineData(
            typeof(UnknownEvent),
            true)]
        [InlineData(
            typeof(Event),
            true)]
        [InlineData(
            typeof(UrlVerificationEvent),
            true)]
        [InlineData(
            typeof(EventWrapper),
            true)]
        public void CanConvert_Returns_Expected_Value(
            Type typeToConvert,
            bool expected)
        {
            var converter = new EventJsonConverter();
            Assert.Equal(
                expected,
                converter.CanConvert(typeToConvert));
        }

        [Fact]
        public void Read_Throws_JsonException_When_TypeProperty_Not_Present()
        {
            const string json = "{\"property\":0}";
            Assert.Throws<JsonException>(() => ExecuteRead(json));
        }

        [Theory]
        [InlineData(
            UrlVerificationEvent.EventTypeValue,
            typeof(UrlVerificationEvent))]
        [InlineData(
            AppRateLimitedEvent.EventTypeValue,
            typeof(AppRateLimitedEvent))]
        [InlineData(
            EventWrapper.EventTypeValue,
            typeof(EventWrapper))]
        [InlineData(
            Event.DefaultEventTypeValue,
            typeof(Event))]
        public void Read_Returns_Event(
            string eventTypeValue,
            Type expectedType)
        {
            var json = $"{{\"type\":\"{eventTypeValue}\"}}";
            var @event = ExecuteRead(json);
            Assert.NotNull(@event);
            Assert.IsAssignableFrom(expectedType, @event);
            Assert.Equal(
                eventTypeValue,
                @event.EventType);
        }

        [Fact]
        public void Write_Writes_Expected_UnknownEvent()
        {
            Serialize(new UnknownEvent())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_Event()
        {
            Serialize(new Event())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_AppRateLimitedEvent()
        {
            Serialize(new AppRateLimitedEvent())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_UrlVerificationEvent()
        {
            Serialize(new UrlVerificationEvent())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_EventWrapper()
        {
            Serialize(new EventWrapper())
                .ShouldMatchSnapshot();
        }

        private static Event ExecuteRead(
            string json)
        {
            var reader =
                new Utf8JsonReader(Encoding.UTF8.GetBytes(json));

            return new EventJsonConverter().Read(
                ref reader,
                typeof(Event), // This is not used, use anything
                Options);
        }

        private static string Serialize(
            Event @event)
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);
            var converter = new EventJsonConverter();
            converter.Write(
                writer,
                @event,
                Options);
            writer.Flush();

            return Encoding.UTF8.GetString(stream.ToArray());
        }

        private class NotEvent
        {
        }

        private class UnknownEvent : Event
        {
        }
    }
}

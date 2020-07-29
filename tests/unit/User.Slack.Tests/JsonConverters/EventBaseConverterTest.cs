namespace User.Slack.Tests.JsonConverters
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using Usain.Slack.JsonConverters;
    using Usain.Slack.Models.Events;
    using Usain.Slack.Models.Events.CallbackEvents;
    using Xunit;

    public class EventBaseConverterTest
    {
        private readonly JsonSerializerOptions _options =
            new JsonSerializerOptions();

        [Theory]
        [InlineData(
            typeof(TestObject),
            false)]
        [InlineData(
            typeof(CallbackEvent),
            false)]
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
            var converter = new EventBaseConverter();
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
            "{\"type\":\"url_verification\"}",
            "url_verification")]
        [InlineData(
            "{\"type\":\"app_rate_limited\",\"team_id\":\"ABC\"}",
            "app_rate_limited")]
        public void Read_Returns_Event(
            string json,
            string type)
        {
            var @event = ExecuteRead(json);
            Assert.NotNull(@event);
            Assert.Equal(
                type,
                @event.Type);
        }

        [Fact]
        public void Write_Throws_NotImplementedException()
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("{}"));
            using var writer = new Utf8JsonWriter(stream);
            var converter = new EventBaseConverter();
            Assert.Throws<NotImplementedException>(
                () => converter.Write(
                    writer,
                    new Event(),
                    _options));
        }

        private Event ExecuteRead(
            string json)
        {
            var reader =
                new Utf8JsonReader(Encoding.UTF8.GetBytes(json));

            return new EventBaseConverter().Read(
                ref reader,
                typeof(Event), // This is not used, use anything
                _options);
        }

        private class TestObject
        {
        }
    }
}

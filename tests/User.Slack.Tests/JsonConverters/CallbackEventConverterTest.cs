namespace User.Slack.Tests.JsonConverters
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using Usain.Slack.JsonConverters;
    using Usain.Slack.Models;
    using Usain.Slack.Models.CallbackEvents;
    using Xunit;

    public class CallbackEventConverterTest
    {
        private readonly JsonSerializerOptions _options =
            new JsonSerializerOptions();

        [Theory]
        [InlineData(
            typeof(TestObject),
            false)]
        [InlineData(
            typeof(EventTimestamp),
            false)]
        [InlineData(
            typeof(CallbackEvent),
            true)]
        [InlineData(
            typeof(AppMentionEvent),
            true)]
        public void CanConvert_Returns_Expected_Value(
            Type typeToConvert,
            bool expected)
        {
            var converter = new CallbackEventConverter();
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
            "{\"type\":\"app_mention\"}",
            "app_mention")]
        [InlineData(
            "{\"type\":\"app_mention\",\"user\":\"ABC\"}",
            "app_mention")]
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
            var converter = new CallbackEventConverter();
            Assert.Throws<NotImplementedException>(
                () => converter.Write(
                    writer,
                    new CallbackEvent(),
                    _options));
        }

        private CallbackEvent ExecuteRead(
            string json)
        {
            var reader =
                new Utf8JsonReader(Encoding.UTF8.GetBytes(json));

            return new CallbackEventConverter().Read(
                ref reader,
                typeof(CallbackEvent), // This is not used, use anything
                _options);
        }


        private class TestObject
        {
        }
    }
}

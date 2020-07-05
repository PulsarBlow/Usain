namespace User.Slack.Tests.JsonConverters
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using Usain.Slack.JsonConverters;
    using Usain.Slack.Models;
    using Xunit;

    public class EventTimestampConverterTest
    {
        private const string JsonData = "{\"prop\":\"1591452219.000400\"}";

        private readonly JsonSerializerOptions _options =
            new JsonSerializerOptions();

        [Fact]
        public void Read_Returns_EventTimestamp()
        {
            var actual = ExecuteRead(JsonData);

            Assert.Equal(
                1591452219,
                actual.Timestamp);
            Assert.Equal(
                "000400",
                actual.Suffix);
        }

        [Fact]
        public void Read_Throws_JsonException_When_TokenType_Is_Not_String()
        {
            Assert.Throws<JsonException>(
                () => ExecuteRead(
                    JsonData,
                    JsonTokenType.StartObject));
        }

        [Fact]
        public void Write_Throws_NotImplementedException()
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("{}"));
            using var writer = new Utf8JsonWriter(stream);
            var converter = new EventTimestampConverter();
            Assert.Throws<NotImplementedException>(
                () => converter.Write(
                    writer,
                    new EventTimestamp(),
                    _options));
        }

        private EventTimestamp ExecuteRead(
            string json,
            JsonTokenType tokenType = JsonTokenType.String)
        {
            var reader =
                new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
            while (reader.TokenType != tokenType) { reader.Read(); }

            return new EventTimestampConverter().Read(
                ref reader,
                typeof(EventTimestamp), // This is not used, use anything
                _options);
        }
    }
}

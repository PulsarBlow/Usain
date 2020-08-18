namespace Usain.Slack.Tests.JsonConverters
{
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using Usain.Slack.JsonConverters;
    using Usain.Slack.Models;
    using Xunit;

    public class TimestampConverterTest
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
                actual.Seconds);
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

        [Theory]
        [InlineData(
            0,
            "",
            "\"0\"")]
        [InlineData(
            0,
            "0",
            "\"0.0\"")]
        [InlineData(
            123456,
            "06",
            "\"123456.06\"")]
        public void Write_Returns_Expected_Value(
            long timestamp,
            string suffix,
            string expected)
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);
            var converter = new TimestampConverter();
            converter.Write(
                writer,
                new Timestamp() { Suffix = suffix, Seconds = timestamp },
                _options);
            writer.Flush();

            var actual = Encoding.UTF8.GetString(stream.ToArray());

            Assert.Equal(
                expected,
                actual);
        }

        private Timestamp ExecuteRead(
            string json,
            JsonTokenType tokenType = JsonTokenType.String)
        {
            var reader =
                new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
            while (reader.TokenType != tokenType) { reader.Read(); }

            return new TimestampConverter().Read(
                ref reader,
                typeof(Timestamp), // This is not used, use anything
                _options);
        }
    }
}

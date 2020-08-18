namespace Usain.Slack.Tests.JsonConverters
{
    using System.Text.Json;
    using Slack.Models.Blocks.Composition;
    using Xunit;

    public class ConversationFilterOptionJsonConverterTest
    {
        private static readonly JsonSerializerOptions Options =
            new JsonSerializerOptions
            {
                // makes our intent clear here
                IgnoreNullValues = false,
            };

        [Theory]
        [InlineData(
            @"""im""",
            ConversationFilterOption.DirectMessage)]
        [InlineData(
            @"""IM""",
            ConversationFilterOption.DirectMessage)]
        [InlineData(
            @"""mpim""",
            ConversationFilterOption.MultiPartyDirectMessage)]
        [InlineData(
            @"""MPIM""",
            ConversationFilterOption.MultiPartyDirectMessage)]
        [InlineData(
            @"""private""",
            ConversationFilterOption.Private)]
        [InlineData(
            @"""PRIVATE""",
            ConversationFilterOption.Private)]
        [InlineData(
            @"""public""",
            ConversationFilterOption.Public)]
        [InlineData(
            @"""PUBLIC""",
            ConversationFilterOption.Public)]
        public void Deserialization_Returns_Expected(
            string value,
            ConversationFilterOption expected)
        {
            var style =
                JsonSerializer.Deserialize<ConversationFilterOption>(value);
            Assert.Equal(
                expected,
                style);
        }

        [Theory]
        [InlineData(@"""""")]
        [InlineData(@"""unknown""")]
        public void Deserialization_Throws_When_Value_Is_Not_Supported(string value)
        {
            Assert.Throws<JsonException>(
                () => JsonSerializer.Deserialize<ConversationFilterOption>(value));
        }

        [Theory]
        [InlineData(
            ConversationFilterOption.DirectMessage,
            @"""im""")]
        [InlineData(
            ConversationFilterOption.MultiPartyDirectMessage,
            @"""mpim""")]
        [InlineData(
            ConversationFilterOption.Public,
            @"""public""")]
        [InlineData(
            ConversationFilterOption.Private,
            @"""private""")]
        public void Serialization_Returns_Expected(
            ConversationFilterOption value,
            string expected)
        {
            var json = JsonSerializer.Serialize(value);
            Assert.Equal(
                expected,
                json);
        }
    }
}

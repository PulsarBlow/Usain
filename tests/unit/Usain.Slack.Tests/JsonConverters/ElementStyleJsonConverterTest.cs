namespace Usain.Slack.Tests.JsonConverters
{
    using System.Text.Json;
    using Usain.Slack.Models.Blocks.Elements;
    using Xunit;

    public class ElementStyleJsonConverterTest
    {
        private static readonly JsonSerializerOptions Options =
            new JsonSerializerOptions
            {
                // makes our intent clear here
                // we want to snapshot every single properties
                IgnoreNullValues = false,
            };

        [Theory]
        [InlineData(
            @"""primary""",
            ElementStyle.Primary)]
        [InlineData(
            @"""PRIMARY""",
            ElementStyle.Primary)]
        [InlineData(
            @"""danger""",
            ElementStyle.Danger)]
        [InlineData(
            @"""DANGER""",
            ElementStyle.Danger)]
        [InlineData(
            @"""""",
            ElementStyle.None)]
        public void Deserialization_Returns_Expected(
            string value,
            ElementStyle expected)
        {
            var style =
                JsonSerializer.Deserialize<ElementStyle>(value);
            Assert.Equal(
                expected,
                style);
        }

        [Theory]
        [InlineData(
            ElementStyle.Primary,
            @"""primary""")]
        [InlineData(
            ElementStyle.Danger,
            @"""danger""")]
        [InlineData(
            ElementStyle.None,
            @"null")]
        public void Serialization_Returns_Expected(
            ElementStyle value,
            string expected)
        {
            var json = JsonSerializer.Serialize(value);
            Assert.Equal(expected, json);
        }
    }
}

namespace Usain.Slack.Tests.JsonConverters
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using Snapper;
    using Usain.Slack.JsonConverters;
    using Usain.Slack.Models.Blocks.Elements;
    using Usain.Slack.Models.Blocks.Elements.Menus;
    using Xunit;

    public class ElementJsonConverterTest
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
            typeof(NotAnElement),
            false)]
        [InlineData(
            typeof(UnknownElement),
            true)]
        [InlineData(
            typeof(Element),
            true)]
        [InlineData(
            typeof(Button),
            true)]
        [InlineData(
            typeof(CheckboxGroup),
            true)]
        [InlineData(
            typeof(DatePicker),
            true)]
        [InlineData(
            typeof(Image),
            true)]
        [InlineData(
            typeof(MarkdownText),
            true)]
        [InlineData(
            typeof(PlainText),
            true)]
        [InlineData(
            typeof(PlainTextInput),
            true)]
        [InlineData(
            typeof(RadioButtonGroup),
            true)]
        [InlineData(
            typeof(ChannelMultiSelectMenu),
            true)]
        [InlineData(
            typeof(ConversationMultiSelectMenu),
            true)]
        [InlineData(
            typeof(ConversationSelectMenu),
            true)]
        [InlineData(
            typeof(ExternalMultiSelectMenu),
            true)]
        [InlineData(
            typeof(ExternalSelectMenu),
            true)]
        [InlineData(
            typeof(OverflowMenu),
            true)]
        [InlineData(
            typeof(StaticMultiSelectMenu),
            true)]
        [InlineData(
            typeof(StaticSelectMenu),
            true)]
        [InlineData(
            typeof(UserMultiSelectMenu),
            true)]
        [InlineData(
            typeof(UserSelectMenu),
            true)]
        public void CanConvert_Returns_Expected_Value(
            Type typeToConvert,
            bool expected)
        {
            var converter = new ElementJsonConverter();
            Assert.Equal(
                expected,
                converter.CanConvert(typeToConvert));
        }

        [Theory]
        [InlineData("{\"type\":null}")]
        [InlineData("{\"type\":\"\"}")]
        [InlineData("{\"property\":\"value\"}")]
        public void Read_Throws_JsonException_When_TypeProperty_Is_Not_Valid(string json)
        {
            Assert.Throws<JsonException>(() => ExecuteRead(json));
        }

        [Theory]
        [InlineData(
            PlainText.ElementTypeValue,
            typeof(PlainText))]
        [InlineData(
            MarkdownText.ElementTypeValue,
            typeof(MarkdownText))]
        public void Read_Returns_Text(
            string expectedElementTypeValue,
            Type expectedType)
        {
            var json = $"{{\"type\":\"{expectedElementTypeValue}\"}}";
            var element = ExecuteRead(json);
            Assert.NotNull(element);
            Assert.IsAssignableFrom(
                expectedType,
                element);
            Assert.Equal(
                expectedElementTypeValue,
                element.ElementType);
        }

        [Fact]
        public void Write_Writes_Expected_PlainText()
        {
            Serialize(
                    new PlainText
                    {
                        Text = "text",
                        Emoji = false,
                    })
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_MarkdownText()
        {
            Serialize(
                    new MarkdownText
                    {
                        Text = "**text**",
                        Verbatim = true,
                    })
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_UnknownText()
        {
            Serialize(
                    new UnknownElement
                    {
                        Property = "something",
                    })
                .ShouldMatchSnapshot();
        }

        private static Element ExecuteRead(
            string json)
        {
            var reader =
                new Utf8JsonReader(Encoding.UTF8.GetBytes(json));

            return new ElementJsonConverter().Read(
                ref reader,
                typeof(Element), // This is not used, use anything
                Options);
        }

        private static string Serialize<T>(
            T text)
            where T : Element
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);
            var converter = new ElementJsonConverter();
            converter.Write(
                writer,
                text,
                Options);
            writer.Flush();

            return Encoding.UTF8.GetString(stream.ToArray());
        }

        private class NotAnElement
        {
        }

        private class UnknownElement : Element
        {
            public string Property { get; set; }

            public UnknownElement()
                => ElementType = "unknown";
        }
    }
}

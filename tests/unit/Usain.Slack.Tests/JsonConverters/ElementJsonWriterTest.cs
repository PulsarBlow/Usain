namespace Usain.Slack.Tests.JsonConverters
{
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using Snapper;
    using Usain.Slack.JsonConverters;
    using Usain.Slack.Models.Blocks.Elements;
    using Xunit;

    public class ElementJsonWriterTest
    {
        private static readonly JsonSerializerOptions Options =
            new JsonSerializerOptions
            {
                IgnoreNullValues = false,
            };

        [Fact]
        public void Write_Writes_Expected_Button()
        {
            SerializeWithJsonWriter(TestModelFactory.Button())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_CheckboxGroup()
        {
            SerializeWithJsonWriter(TestModelFactory.CheckboxGroup())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_DatePicker()
        {
            SerializeWithJsonWriter(TestModelFactory.DatePicker())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_Image()
        {
            SerializeWithJsonWriter(TestModelFactory.Image())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_MarkdownText()
        {
            SerializeWithJsonWriter(TestModelFactory.MarkdownText())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_PlainText()
        {
            SerializeWithJsonWriter(TestModelFactory.PlainText())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_PlainTextInput()
        {
            SerializeWithJsonWriter(TestModelFactory.PlainTextInput())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_RadioButtonGroup()
        {
            SerializeWithJsonWriter(TestModelFactory.RadioButtonGroup())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_ChannelMultiSelectMenu()
        {
            SerializeWithJsonWriter(TestModelFactory.ChannelMultiSelectMenu())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_ChannelSelectMenu()
        {
            SerializeWithJsonWriter(TestModelFactory.ChannelSelectMenu())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_ConversationMultiSelectMenu()
        {
            SerializeWithJsonWriter(TestModelFactory.ConversationMultiSelectMenu())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_ConversationSelectMenu()
        {
            SerializeWithJsonWriter(TestModelFactory.ConversationSelectMenu())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_ExternalMultiSelectMenu()
        {
            SerializeWithJsonWriter(TestModelFactory.ExternalMultiSelectMenu())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_ExternalSelectMenu()
        {
            SerializeWithJsonWriter(TestModelFactory.ExternalSelectMenu())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_StaticMultiSelectMenu()
        {
            SerializeWithJsonWriter(TestModelFactory.StaticMultiSelectMenu())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_StaticSelectMenu()
        {
            SerializeWithJsonWriter(TestModelFactory.StaticSelectMenu())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_UserMultiSelectMenu()
        {
            SerializeWithJsonWriter(TestModelFactory.UserMultiSelectMenu())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_UserSelectMenu()
        {
            SerializeWithJsonWriter(TestModelFactory.UserSelectMenu())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_OverflowMenu()
        {
            SerializeWithJsonWriter(TestModelFactory.OverflowMenu())
                .ShouldMatchSnapshot();
        }

        private static string SerializeWithJsonWriter(
            Element element)
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);
            var converter = new ElementJsonConverter();
            converter.Write(
                writer,
                element,
                Options);
            writer.Flush();

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}

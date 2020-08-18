namespace Usain.Slack.Tests.JsonConverters
{
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using Snapper;
    using Usain.Slack.JsonConverters;
    using Usain.Slack.Models.Blocks;
    using Xunit;

    public class BlockJsonWriterTest
    {
        private static readonly JsonSerializerOptions Options =
            new JsonSerializerOptions
            {
                // even if default, makes our intent clear
                IgnoreNullValues = false,
            };

        [Fact]
        public void Write_Writes_Expected_ActionsBlock()
        {
            SerializeWithJsonWriter(TestModelFactory.ActionsBlock())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_ContextBlock()
        {
            SerializeWithJsonWriter(TestModelFactory.ContextBlock())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_DividerBlock()
        {
            SerializeWithJsonWriter(TestModelFactory.DividerBlock())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_FileBlock()
        {
            SerializeWithJsonWriter(TestModelFactory.FileBlock())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_HeaderBlock()
        {
            SerializeWithJsonWriter(TestModelFactory.HeaderBlock())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_ImageBlock()
        {
            SerializeWithJsonWriter(TestModelFactory.ImageBlock())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_InputBlock()
        {
            SerializeWithJsonWriter(TestModelFactory.InputBlock())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_SectionBlock()
        {
            SerializeWithJsonWriter(TestModelFactory.SectionBlock())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Default_When_Unknown()
        {
            SerializeWithJsonWriter(new UnknownBlock())
                .ShouldMatchSnapshot();
        }

        private static string SerializeWithJsonWriter(
            Block block)
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);
            var converter = new BlockJsonConverter();
            converter.Write(
                writer,
                block,
                Options);
            writer.Flush();

            return Encoding.UTF8.GetString(stream.ToArray());
        }

        private class UnknownBlock : Block
        {
            public UnknownBlock()
                => BlockType = "unknown";
        }
    }
}

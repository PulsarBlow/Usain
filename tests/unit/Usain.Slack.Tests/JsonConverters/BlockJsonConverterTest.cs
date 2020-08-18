namespace Usain.Slack.Tests.JsonConverters
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using Slack.JsonConverters;
    using Slack.Models.Blocks;
    using Snapper;
    using Xunit;

    public class BlockJsonConverterTest
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
            typeof(NotBlock),
            false)]
        [InlineData(
            typeof(UnknownBlock),
            true)]
        [InlineData(
            typeof(ActionsBlock),
            true)]
        public void CanConvert_Returns_Expected_Value(
            Type typeToConvert,
            bool expected)
        {
            var converter = new BlockJsonConverter();
            Assert.Equal(
                expected,
                converter.CanConvert(typeToConvert));
        }

        [Theory]
        [InlineData(
            "unknown",
            typeof(Block))]
        [InlineData(
            ActionsBlock.BlockTypeValue,
            typeof(ActionsBlock))]
        [InlineData(
            ContextBlock.BlockTypeValue,
            typeof(ContextBlock))]
        [InlineData(
            DividerBlock.BlockTypeValue,
            typeof(DividerBlock))]
        [InlineData(
            FileBlock.BlockTypeValue,
            typeof(FileBlock))]
        [InlineData(
            HeaderBlock.BlockTypeValue,
            typeof(HeaderBlock))]
        [InlineData(
            ImageBlock.BlockTypeValue,
            typeof(ImageBlock))]
        [InlineData(
            InputBlock.BlockTypeValue,
            typeof(InputBlock))]
        [InlineData(
            SectionBlock.BlockTypeValue,
            typeof(SectionBlock))]
        public void Read_Returns_Event(
            string blockTypeValue,
            Type expectedType)
        {
            var json = $"{{\"type\":\"{blockTypeValue}\"}}";
            var block = ExecuteRead(json);
            Assert.NotNull(block);
            Assert.IsAssignableFrom(
                expectedType,
                block);
            Assert.Equal(
                blockTypeValue,
                block.BlockType);
        }

        [Fact]
        public void Write_Writes_Expected_UnknownBlock()
        {
            Serialize(new UnknownBlock())
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Writes_Expected_ActionsBlock()
        {
            Serialize(new ActionsBlock())
                .ShouldMatchSnapshot();
        }

        private static Block ExecuteRead(
            string json)
        {
            var reader =
                new Utf8JsonReader(Encoding.UTF8.GetBytes(json));

            return new BlockJsonConverter().Read(
                ref reader,
                typeof(Block), // This is not used, use anything
                Options);
        }

        private static string Serialize(
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

        private class NotBlock
        {
        }

        private class UnknownBlock : Block
        {
        }
    }
}

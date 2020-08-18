namespace Usain.Slack.Tests.JsonConverters
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using Slack.JsonConverters;
    using Usain.Slack.Models.Blocks;
    using Xunit;

    public class BlockTypeResolverTest
    {
        [Theory]
        [InlineData("{\"property\": \"value\"}")]
        public void ResolveType_Throws_When_BlockType_Property_DoesNot_Exist(
            string json)
        {
            var jsonDocument = JsonDocument.Parse(json);
            var typeProvider = new BlockJsonTypeResolver(jsonDocument.RootElement);

            Assert.Throws<JsonException>(() => typeProvider.ResolveType());
        }

        [Theory]
        [MemberData(nameof(ResolveType_Test_Data))]
        public void ResolveType_Returns_Expected_Type(
            string elementType,
            Type expectedType)
        {
            var jsonDocument = JsonDocument.Parse(
                $"{{\"{Block.BlockTypeJsonName}\":\"{elementType}\"}}");
            var typeProvider = new BlockJsonTypeResolver(jsonDocument.RootElement);

            var actualType = typeProvider.ResolveType();

            Assert.Equal(
                expectedType,
                actualType);
        }

        public static IEnumerable<object[]> ResolveType_Test_Data()
        {
            yield return new object[]
            {
                ActionsBlock.BlockTypeValue,
                typeof(ActionsBlock),
            };

            yield return new object[]
            {
                ContextBlock.BlockTypeValue,
                typeof(ContextBlock),
            };

            yield return new object[]
            {
                DividerBlock.BlockTypeValue,
                typeof(DividerBlock),
            };

            yield return new object[]
            {
                FileBlock.BlockTypeValue,
                typeof(FileBlock),
            };

            yield return new object[]
            {
                HeaderBlock.BlockTypeValue,
                typeof(HeaderBlock),
            };

            yield return new object[]
            {
                ImageBlock.BlockTypeValue,
                typeof(ImageBlock),
            };

            yield return new object[]
            {
                InputBlock.BlockTypeValue,
                typeof(InputBlock),
            };

            yield return new object[]
            {
                SectionBlock.BlockTypeValue,
                typeof(SectionBlock),
            };

            yield return new object[]
            {
                Block.DefaultBlockTypeValue,
                typeof(Block),
            };

            yield return new object[]
            {
                "whatever",
                typeof(Block),
            };
        }
    }
}

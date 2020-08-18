namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using Models.Blocks;

    internal class BlockJsonTypeResolver
    {
        private readonly JsonElement _jsonElement;

        public BlockJsonTypeResolver(
            JsonElement jsonElement)
            => _jsonElement = jsonElement;

        public Type ResolveType()
        {
            if (!_jsonElement.TryGetProperty(
                Block.BlockTypeJsonName,
                out var property)) { throw new JsonException(); }

            var blockTypeValue = property.GetString();
            return blockTypeValue switch
            {
                ActionsBlock.BlockTypeValue => typeof(ActionsBlock),
                ContextBlock.BlockTypeValue => typeof(ContextBlock),
                DividerBlock.BlockTypeValue => typeof(DividerBlock),
                FileBlock.BlockTypeValue => typeof(FileBlock),
                HeaderBlock.BlockTypeValue => typeof(HeaderBlock),
                ImageBlock.BlockTypeValue => typeof(ImageBlock),
                InputBlock.BlockTypeValue => typeof(InputBlock),
                SectionBlock.BlockTypeValue => typeof(SectionBlock),
                // We should throw an exception here.
                // We don't do it until the complete Slack Event API surface is covered,
                // in particular the RichTextBlock.
                // Otherwise we wouldn't be able to support unknown (not yet implemented ) blocks.
                // This will certainly change in a future version.
                _ => typeof(Block),
            };
        }
    }
}

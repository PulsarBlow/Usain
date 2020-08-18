namespace Usain.Slack.JsonConverters
{
    using System.Text.Json;
    using Models.Blocks;

    internal class BlockJsonWriter
    {
        private readonly Utf8JsonWriter _jsonWriter;
        private readonly JsonSerializerOptions _options;

        public BlockJsonWriter(
            Utf8JsonWriter jsonWriter,
            JsonSerializerOptions options)
        {
            _jsonWriter = jsonWriter;
            _options = options;
        }

        public void Write(
            Block value)
        {
            switch (value)
            {
                case ActionsBlock actionsBlock:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        actionsBlock,
                        _options);
                    return;
                case ContextBlock contextBlock:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        contextBlock,
                        _options);
                    return;
                case DividerBlock dividerBlock:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        dividerBlock,
                        _options);
                    return;
                case FileBlock fileBlock:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        fileBlock,
                        _options);
                    return;
                case HeaderBlock headerBlock:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        headerBlock,
                        _options);
                    return;
                case ImageBlock imageBlock:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        imageBlock,
                        _options);
                    return;
                case InputBlock inputBlock:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        inputBlock,
                        _options);
                    return;
                case SectionBlock sectionBlock:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        sectionBlock,
                        _options);
                    return;
            }

            WriteDefault(value);
        }

        private void WriteDefault(
            Block value)
        {
            _jsonWriter.WriteStartObject();
            _jsonWriter.WriteString(Block.BlockTypeJsonName, value.BlockType);
            _jsonWriter.WriteString(Block.BlockIdJsonName, value.BlockId);
            _jsonWriter.WriteEndObject();
        }
    }
}

namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Models.Blocks;

    public class BlockJsonConverter : JsonConverter<Block>
    {
        public override bool CanConvert(
            Type typeToConvert)
            => typeof(Block).IsAssignableFrom(typeToConvert);

        public override Block Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            var root = document.RootElement;
            var typeProvider = new BlockJsonTypeResolver(root);
            Type type = typeProvider.ResolveType();

            // Avoid infinite recursive behavior of the JsonSerializer
            // when returning Block type (default case of the type resolver).
            if (type == typeof(Block)) { return new Block(); }

            return (Block) JsonSerializer.Deserialize(
                root.GetRawText(),
                type,
                options);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Block value,
            JsonSerializerOptions options)
        {
            var jsonWriter = new BlockJsonWriter(
                writer,
                options);
            jsonWriter.Write(value);
        }
    }
}

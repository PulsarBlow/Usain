namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Models.Blocks.Elements;

    public class ElementJsonConverter : JsonConverter<Element>
    {
        public override bool CanConvert(
            Type typeToConvert)
            => typeof(Element).IsAssignableFrom(typeToConvert);

        public override Element Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            var root = document.RootElement;

            var typeProvider = new ElementJsonTypeResolver(root);
            Type type = typeProvider.ResolveType();

            return (Element) JsonSerializer.Deserialize(
                root.GetRawText(),
                type,
                options);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Element value,
            JsonSerializerOptions options)
        {
            var jsonWriter = new ElementJsonWriter(
                writer,
                options);
            jsonWriter.Write(value);
        }
    }
}

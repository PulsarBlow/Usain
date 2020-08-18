namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Models.Blocks.Elements;

    public class ElementStyleJsonConverter
        : JsonConverter<ElementStyle>
    {
        public override ElementStyle Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            string enumValue = reader.GetString();
            return Enum.TryParse<ElementStyle>(
                enumValue,
                true,
                out var style)
                ? style
                : ElementStyle.None;
        }

        public override void Write(
            Utf8JsonWriter writer,
            ElementStyle value,
            JsonSerializerOptions options)
        {
            switch (value)
            {
                case ElementStyle.Primary:
                    writer.WriteStringValue(
                        ElementStyle.Primary.ToString()
                            .ToLowerInvariant());
                    return;
                case ElementStyle.Danger:
                    writer.WriteStringValue(
                        ElementStyle.Danger.ToString()
                            .ToLowerInvariant());
                    return;
                default:
                    writer.WriteNullValue();
                    return;
            }
        }
    }
}

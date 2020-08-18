namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using Models.Blocks.Elements;
    using Models.Blocks.Elements.Menus;

    internal class ElementJsonTypeResolver
    {
        private readonly JsonElement _jsonElement;

        public ElementJsonTypeResolver(
            JsonElement jsonElement)
            => _jsonElement = jsonElement;

        public Type ResolveType()
        {
            if (!_jsonElement.TryGetProperty(
                Element.ElementTypeJsonName,
                out var property)) { throw new JsonException(); }

            var elementTypeValue = property.GetString();
            return elementTypeValue switch
            {
                Button.ElementTypeValue => typeof(Button),
                CheckboxGroup.ElementTypeValue => typeof(CheckboxGroup),
                DatePicker.ElementTypeValue => typeof(DatePicker),
                Image.ElementTypeValue => typeof(Image),
                MarkdownText.ElementTypeValue => typeof(MarkdownText),
                PlainText.ElementTypeValue => typeof(PlainText),
                PlainTextInput.ElementTypeValue => typeof(PlainTextInput),
                RadioButtonGroup.ElementTypeValue => typeof(RadioButtonGroup),
                ChannelMultiSelectMenu.ElementTypeValue =>
                    typeof(ChannelMultiSelectMenu),
                ChannelSelectMenu.ElementTypeValue => typeof(ChannelSelectMenu),
                ConversationMultiSelectMenu.ElementTypeValue =>
                    typeof(ConversationMultiSelectMenu),
                ConversationSelectMenu.ElementTypeValue =>
                    typeof(ConversationSelectMenu),
                ExternalMultiSelectMenu.ElementTypeValue =>
                    typeof(ExternalMultiSelectMenu),
                ExternalSelectMenu.ElementTypeValue =>
                    typeof(ExternalSelectMenu),
                StaticMultiSelectMenu.ElementTypeValue =>
                    typeof(StaticMultiSelectMenu),
                StaticSelectMenu.ElementTypeValue => typeof(StaticSelectMenu),
                UserMultiSelectMenu.ElementTypeValue =>
                    typeof(UserMultiSelectMenu),
                UserSelectMenu.ElementTypeValue => typeof(UserSelectMenu),
                OverflowMenu.ElementTypeValue => typeof(OverflowMenu),
                _ => throw new JsonException(
                    $"Invalid element type property value `{elementTypeValue}`"),
            };
        }
    }
}

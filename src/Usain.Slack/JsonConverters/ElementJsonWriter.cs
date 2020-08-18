namespace Usain.Slack.JsonConverters
{
    using System.Text.Json;
    using Models.Blocks.Elements;
    using Models.Blocks.Elements.Menus;

    internal class ElementJsonWriter
    {
        private readonly Utf8JsonWriter _jsonWriter;
        private readonly JsonSerializerOptions _options;

        public ElementJsonWriter(
            Utf8JsonWriter jsonWriter,
            JsonSerializerOptions options)
        {
            _jsonWriter = jsonWriter;
            _options = options;
        }

        public void Write(
            Element value)
        {
            switch (value)
            {
                case Button button:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        button,
                        _options);
                    return;
                case CheckboxGroup checkboxGroup:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        checkboxGroup,
                        _options);
                    return;
                case DatePicker datePicker:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        datePicker,
                        _options);
                    return;
                case Image image:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        image,
                        _options);
                    return;
                case MarkdownText markdownText:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        markdownText,
                        _options);
                    return;
                case PlainText plainText:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        plainText,
                        _options);
                    return;
                case PlainTextInput plainTextInput:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        plainTextInput,
                        _options);
                    return;
                case RadioButtonGroup radioButtonGroup:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        radioButtonGroup,
                        _options);
                    return;
                case ChannelMultiSelectMenu channelMultiSelectMenu:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        channelMultiSelectMenu,
                        _options);
                    return;
                case ChannelSelectMenu channelSelectMenu:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        channelSelectMenu,
                        _options);
                    return;
                case ConversationMultiSelectMenu conversationMultiSelectMenu:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        conversationMultiSelectMenu,
                        _options);
                    return;
                case ConversationSelectMenu conversationsSelect:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        conversationsSelect,
                        _options);
                    return;
                case ExternalMultiSelectMenu externalMultiSelectMenu:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        externalMultiSelectMenu,
                        _options);
                    return;
                case ExternalSelectMenu externalSelectMenu:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        externalSelectMenu,
                        _options);
                    return;
                case OverflowMenu overflowMenu:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        overflowMenu,
                        _options);
                    return;
                case StaticMultiSelectMenu staticMultiSelectMenu:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        staticMultiSelectMenu,
                        _options);
                    return;
                case StaticSelectMenu staticSelectMenu:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        staticSelectMenu,
                        _options);
                    return;
                case UserMultiSelectMenu usersMultiSelectMenu:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        usersMultiSelectMenu,
                        _options);
                    return;
                case UserSelectMenu usersSelectMenu:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        usersSelectMenu,
                        _options);
                    return;
            }

            WriteDefault(value);
        }

        private void WriteDefault(
            Element value)
        {
            _jsonWriter.WriteStartObject();
            _jsonWriter.WriteString(
                Element.ElementTypeJsonName,
                value.ElementType);
            _jsonWriter.WriteEndObject();
        }
    }
}

namespace Usain.Slack.Tests
{
    using Slack.Models.Blocks.Composition;
    using Usain.Slack.Models.Blocks;
    using Usain.Slack.Models.Blocks.Elements;
    using Usain.Slack.Models.Blocks.Elements.Menus;

    internal class TestModelFactory
    {
        public static Option Option()
            => new Option
            {
                Text = PlainText(),
                Description = PlainText(),
                Url = "http://localhost/",
                Value = "value",
            };

        public static OptionGroup OptionGroup()
            => new OptionGroup
            {
                Label = PlainText(),
                Options = new[] { Option() },
            };

        public static Button Button()
            => new Button
            {
                ActionId = "action_id",
                ConfirmDialog = ConfirmDialog(),
                Text = PlainText(),
                Style = ElementStyle.Danger,
                Url = "http://localhost",
                Value = "value",
            };

        public static CheckboxGroup CheckboxGroup()
            => new CheckboxGroup
            {
                ActionId = "action_id",
                ConfirmDialog = ConfirmDialog(),
                Options = new[] { Option() },
                SelectedOptions = new[] { Option() },
            };

        public static ConfirmDialog ConfirmDialog()
            => new ConfirmDialog
            {
                Title = PlainText(),
                Text = MarkdownText(),
                Confirm = PlainText(),
                Cancel = PlainText(),
                Style = ElementStyle.Danger,
            };

        public static DatePicker DatePicker()
            => new DatePicker
            {
                ActionId = "action_id",
                ConfirmDialog = ConfirmDialog(),
                Placeholder = PlainText(),
                SelectedDate = "2020-08-15",
            };

        public static Image Image()
            => new Image
            {
                AltText = "alt_text",
                ImageUrl = "http:/localhost/",
            };

        public static MarkdownText MarkdownText()
            => new MarkdownText { Text = "**text**" };

        public static PlainText PlainText()
            => new PlainText { Text = "text" };

        public static PlainTextInput PlainTextInput()
            => new PlainTextInput
            {
                ActionId = "action_id",
                Multiline = true,
                Placeholder = PlainText(),
                InitialValue = "value",
                MinLength = 1,
                MaxLength = 2000,
            };

        public static RadioButtonGroup RadioButtonGroup()
            => new RadioButtonGroup
            {
                ActionId = "action_id",
                ConfirmDialog = ConfirmDialog(),
                Options = new[] { Option() },
                SelectedOption = Option(),
            };

        public static ChannelMultiSelectMenu ChannelMultiSelectMenu()
            => new ChannelMultiSelectMenu
            {
                ActionId = "action_id",
                ConfirmDialog = ConfirmDialog(),
                MaxSelectedItems = 1,
                Placeholder = PlainText(),
                SelectedChannels = new[] { "channel_id" },
            };

        public static ChannelSelectMenu ChannelSelectMenu()
            => new ChannelSelectMenu
            {
                ActionId = "action_id",
                ConfirmDialog = ConfirmDialog(),
                Placeholder = PlainText(),
                ResponseUrlEnabled = true,
                SelectedChannel = "channel_id",
            };

        public static ConversationMultiSelectMenu
            ConversationMultiSelectMenu()
            => new ConversationMultiSelectMenu
            {
                ActionId = "action_id",
                ConfirmDialog = ConfirmDialog(),
                ConversationFilter = ConversationFilter(),
                Placeholder = PlainText(),
                DefaultToCurrentConversation = true,
                MaxSelectedItems = 1,
                SelectedConversations = new[] { "conversation_id" },
            };

        public static ConversationSelectMenu ConversationSelectMenu()
            => new ConversationSelectMenu
            {
                ActionId = "action_id",
                Placeholder = PlainText(),
                ConfirmDialog = ConfirmDialog(),
                ConversationFilter = ConversationFilter(),
                SelectedConversation = "conversation_id",
            };

        public static ConversationFilter ConversationFilter()
            => new ConversationFilter
            {
                Include = new[]
                {
                    ConversationFilterOption.DirectMessage,
                    ConversationFilterOption.Private,
                },
                ExcludeBotUsers = true,
                ExcludeExternalSharedChannels = true,
            };

        public static ExternalMultiSelectMenu ExternalMultiSelectMenu()
            => new ExternalMultiSelectMenu
            {
                ActionId = "action_id",
                Placeholder = PlainText(),
                ConfirmDialog = ConfirmDialog(),
                MaxSelectedItems = 1,
                MinQueryLength = 3,
                SelectedOptions = new[] { Option() },
            };

        public static ExternalSelectMenu ExternalSelectMenu()
            => new ExternalSelectMenu
            {
                ActionId = "action_id",
                ConfirmDialog = ConfirmDialog(),
                MinQueryLength = 1,
                Placeholder = PlainText(),
                SelectedOption = Option(),
            };

        public static StaticMultiSelectMenu StaticMultiSelectMenu()
            => new StaticMultiSelectMenu
            {
                ActionId = "action_id",
                ConfirmDialog = ConfirmDialog(),
                MaxSelectedItems = 1,
                OptionGroups = new[] { OptionGroup() },
                Placeholder = PlainText(),
                SelectedOptions = new[] { Option() },
            };

        public static StaticSelectMenu StaticSelectMenu()
            => new StaticSelectMenu
            {
                ActionId = "action_id",
                ConfirmDialog = ConfirmDialog(),
                OptionGroups = new[] { OptionGroup() },
                Placeholder = PlainText(),
                SelectedOption = Option(),
            };

        public static UserMultiSelectMenu UserMultiSelectMenu()
            => new UserMultiSelectMenu
            {
                Placeholder = PlainText(),
                ActionId = "action_id",
                ConfirmDialog = ConfirmDialog(),
                MaxSelectedItems = 1,
                SelectedUsers = new[] { "user_id" },
            };

        public static UserSelectMenu UserSelectMenu()
            => new UserSelectMenu
            {
                Placeholder = PlainText(),
                ActionId = "action_id",
                ConfirmDialog = ConfirmDialog(),
                SelectedUser = "user_id",
            };

        public static OverflowMenu OverflowMenu()
            => new OverflowMenu
            {
                Options = new[] { Option() },
                ActionId = "action_id",
                ConfirmDialog = ConfirmDialog(),
            };

        public static ActionsBlock ActionsBlock()
            => new ActionsBlock
            {
                BlockId = "block_id",
                ActionElements = new ActionElement[]
                {
                    OverflowMenu(),
                    Button(),
                },
            };

        public static ContextBlock ContextBlock()
            => new ContextBlock
            {
                Elements = new Element[]
                {
                    Image(),
                    PlainText(),
                    PlainText(),
                },
            };

        public static DividerBlock DividerBlock()
            => new DividerBlock();

        public static FileBlock FileBlock()
            => new FileBlock
            {
                Source = "http://localhost/",
                ExternalId = "external_id",
            };

        public static HeaderBlock HeaderBlock()
        => new HeaderBlock();

        public static ImageBlock ImageBlock()
            => new ImageBlock
            {
                Title = PlainText(),
                AltText = "alt_text",
                ImageUrl = "http://localhost/",
            };

        public static InputBlock InputBlock()
            => new InputBlock
            {
                Hint = PlainText(),
                Label = PlainText(),
                Optional = true,
            };

        public static SectionBlock SectionBlock()
            => new SectionBlock
            {
                Accessory = Button(),
                Fields = new TextElement[]
                {
                    PlainText(),
                    MarkdownText(),
                },
            };
    }
}

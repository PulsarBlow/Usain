namespace Usain.Slack.Tests.JsonConverters
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using Slack.Models.Blocks;
    using Snapper;
    using Usain.Slack.JsonConverters;
    using Usain.Slack.Models;
    using Usain.Slack.Models.Events.CallbackEvents;
    using Usain.Slack.Models.Messages;
    using Xunit;

    public class CallbackEventJsonConverterTest
    {
        private static readonly JsonSerializerOptions Options =
            new JsonSerializerOptions
            {
                IgnoreNullValues = true,
            };

        [Theory]
        [InlineData(
            typeof(NotCallbackEvent),
            false)]
        [InlineData(
            typeof(CallbackEvent),
            true)]
        [InlineData(
            typeof(UnknownCallbackEvent),
            true)]
        [InlineData(
            typeof(AppMentionEvent),
            true)]
        public void CanConvert_Returns_Expected_Value(
            Type typeToConvert,
            bool expected)
        {
            var converter = new CallbackEventJsonConverter();
            Assert.Equal(
                expected,
                converter.CanConvert(typeToConvert));
        }

        [Fact]
        public void Read_Throws_JsonException_When_TypeProperty_Not_Present()
        {
            const string json = "{\"property\":\"value\"}";
            Assert.Throws<JsonException>(() => ExecuteRead(json));
        }

        [Theory]
        [InlineData(
            CallbackEvent.DefaultCallbackEventTypeValue,
            typeof(CallbackEvent))]
        [InlineData(
            AppMentionEvent.CallbackEventTypeValue,
            typeof(AppMentionEvent))]
        public void Read_Returns_Event(
            string callbackEventType,
            Type expectedType)
        {
            var json =
                $"{{\"{CallbackEvent.EventTypeJsonName}\":\"{callbackEventType}\"}}";
            var @event = ExecuteRead(json);
            Assert.IsAssignableFrom(
                expectedType,
                @event);
            Assert.Equal(
                callbackEventType,
                @event.CallbackEventType);
        }

        [Fact]
        public void Write_Returns_Expected_CallbackEvent()
        {
            var @event = new CallbackEvent
            {
                CallbackEventType = "callback_event",
                EventTimestamp = new Timestamp
                    { Suffix = "006", Seconds = 123456 },
            };

            Serialize(@event)
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Returns_Expected_AppMentionEvent()
        {
            var @event = new AppMentionEvent
            {
                ChannelId = "channel",
                Text = "text",
                CallbackEventType = AppMentionEvent.CallbackEventTypeValue,
                UserId = "user",
                MessageId = new Timestamp
                    { Suffix = "006", Seconds = 123456, },
                EventTimestamp = new Timestamp
                    { Suffix = "006", Seconds = 123456, },
            };

            Serialize(@event)
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Returns_Expected_MessageEvent()
        {
            Serialize(
                    new MessageEvent
                    {
                        CallbackEventType = MessageEvent.CallbackEventTypeValue,
                        ChannelId = "channelId",
                        Edited = new Edited
                        {
                            Timestamp = new Timestamp
                                { Seconds = 123456, Suffix = "001" },
                            UserId = "user",
                        },
                        ChannelType = "channelType",
                        EventTimestamp = new Timestamp
                            { Seconds = 123456, Suffix = "001" },
                        IsHidden = false,
                        IsStarred = true,
                        MessageId = new Timestamp
                            { Seconds = 123456, Suffix = "001" },
                        ParentMessageId = new Timestamp
                            { Seconds = 123456, Suffix = "000" },
                        Text = "text",
                        UserId = "userId",
                        ParentUserId = "parentUserId",
                        PinnedTo = new[] { "something" },
                        Reactions = new[]
                        {
                            new Reaction
                            {
                                Name = "astonished",
                                Users = new[] { "user_id" },
                                Count = 1,
                            },
                        },
                        Blocks = new[] { new Block(), },
                    })
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Returns_Expected_MeMessageEvent()
        {
            Serialize(
                    new MeMessageEvent
                    {
                        CallbackEventType = MessageEvent.CallbackEventTypeValue,
                        MessageSubType = MeMessageEvent.MessageSubTypeValue,
                    })
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Returns_Expected_MessageChangedEvent()
        {
            Serialize(
                    new MessageChangedEvent
                    {
                        CallbackEventType = MessageEvent.CallbackEventTypeValue,
                        MessageSubType =
                            MessageChangedEvent.MessageSubTypeValue,
                        PreviousMessage = new MessageEvent
                        {
                            CallbackEventType =
                                MessageEvent.CallbackEventTypeValue,
                            MessageSubType = MeMessageEvent.MessageSubTypeValue,
                            Text = "previousText",
                        },
                        Text = "newText",
                    })
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Returns_Expected_MessageDeletedEvent()
        {
            Serialize(
                    new MessageDeletedEvent
                    {
                        DeletedMessageId = new Timestamp
                        {
                            Seconds = 123456,
                            Suffix = "006",
                        },
                    })
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Returns_Expected_MessageRepliedEvent()
        {
            Serialize(
                    new MessageRepliedEvent
                    {
                        ParentMessage = new MessageEvent
                        {
                            ReplyCount = 1,
                            Replies = new[]
                            {
                                new MessageEvent
                                {
                                    MessageId = new Timestamp
                                    {
                                        Seconds = 123456798,
                                        Suffix = "0002",
                                    },
                                },
                            },
                        },
                    })
                .ShouldMatchSnapshot();
        }

        [Fact]
        public void Write_Returns_Expected_UnknownEvent()
        {
            var @event = new UnknownCallbackEvent
            {
                CallbackEventType = "unknown_event",
                NewValue = "new_value",
                SomeFlag = true,
            };

            Serialize(@event)
                .ShouldMatchSnapshot();
        }

        private static string Serialize<T>(
            T callbackEvent)
            where T : CallbackEvent
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);
            var converter = new CallbackEventJsonConverter();
            converter.Write(
                writer,
                callbackEvent,
                Options);
            writer.Flush();

            return Encoding.UTF8.GetString(stream.ToArray());
        }

        private static CallbackEvent ExecuteRead(
            string json)
        {
            var reader =
                new Utf8JsonReader(Encoding.UTF8.GetBytes(json));

            return new CallbackEventJsonConverter().Read(
                ref reader,
                typeof(CallbackEvent), // This is not used, use anything
                Options);
        }

        private class NotCallbackEvent
        {
        }

        private class UnknownCallbackEvent : CallbackEvent
        {
            public string NewValue { get; set; }
            public bool SomeFlag { get; set; }
        }
    }
}

namespace User.Slack.Tests.JsonConverters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using Usain.Slack.JsonConverters;
    using Usain.Slack.Models;
    using Usain.Slack.Models.CallbackEvents;
    using Xunit;

    public class CallbackEventConverterTest
    {
        private readonly JsonSerializerOptions _options =
            new JsonSerializerOptions();

        [Theory]
        [InlineData(
            typeof(EmptyTestObject),
            false)]
        [InlineData(
            typeof(EventTimestamp),
            false)]
        [InlineData(
            typeof(CallbackEvent),
            true)]
        [InlineData(
            typeof(AppMentionEvent),
            true)]
        public void CanConvert_Returns_Expected_Value(
            Type typeToConvert,
            bool expected)
        {
            var converter = new CallbackEventConverter();
            Assert.Equal(
                expected,
                converter.CanConvert(typeToConvert));
        }

        [Fact]
        public void Read_Throws_JsonException_When_TypeProperty_Not_Present()
        {
            const string json = "{\"property\":0}";
            Assert.Throws<JsonException>(() => ExecuteRead(json));
        }

        [Theory]
        [InlineData(
            "{\"type\":\"app_mention\"}",
            "app_mention")]
        [InlineData(
            "{\"type\":\"app_mention\",\"user\":\"ABC\"}",
            "app_mention")]
        public void Read_Returns_Event(
            string json,
            string type)
        {
            var @event = ExecuteRead(json);
            Assert.NotNull(@event);
            Assert.Equal(
                type,
                @event.Type);
        }

        [Theory]
        [MemberData(nameof(Write_Returns_Expected_Value_Data))]
        public void Write_Returns_Expected_Value(
            CallbackEvent callbackEvent,
            string expected)
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);
            var converter = new CallbackEventConverter();
            converter.Write(
                writer,
                callbackEvent,
                _options);
            writer.Flush();

            var actual = Encoding.UTF8.GetString(stream.ToArray());

            Assert.Equal(
                expected,
                actual);
        }

        public static IEnumerable<object[]> Write_Returns_Expected_Value_Data()
        {
            yield return new object[]
            {
                new AppMentionEvent
                {
                    Channel = "channel",
                    Text = "text",
                    Type = AppMentionEvent.EventType,
                    User = "user",
                    Timestamp = new EventTimestamp
                        { Suffix = "006", Timestamp = 123456, },
                    EventTimestamp = new EventTimestamp
                        { Suffix = "006", Timestamp = 123456, },
                },
                "{\"user\":\"user\",\"text\":\"text\",\"ts\":\"123456.006\",\"channel\":\"channel\",\"type\":\"app_mention\",\"event_ts\":\"123456.006\"}",
            };

            yield return new object[]
            {
                new AppMentionEvent
                {
                    Channel = "channel",
                    Text = "text",
                    Type = AppMentionEvent.EventType,
                    User = "user",
                    Timestamp = new EventTimestamp
                        { Suffix = "006", Timestamp = 123456, },
                    EventTimestamp = new EventTimestamp
                        { Suffix = "006", Timestamp = 123456, },
                },
                "{\"user\":\"user\",\"text\":\"text\",\"ts\":\"123456.006\",\"channel\":\"channel\",\"type\":\"app_mention\",\"event_ts\":\"123456.006\"}",
            };

            yield return new object[]
            {
                new CallbackEvent
                {
                    Type = "callback_event",
                    EventTimestamp = new EventTimestamp
                        { Suffix = "006", Timestamp = 123456 },
                },
                "{\"type\":\"callback_event\",\"event_ts\":\"123456.006\"}",
            };

            yield return new object[]
            {
                new NewEvent
                {
                    Type = "new_event",
                },
                "{\"type\":\"new_event\"}",
            };
        }

        private CallbackEvent ExecuteRead(
            string json)
        {
            var reader =
                new Utf8JsonReader(Encoding.UTF8.GetBytes(json));

            return new CallbackEventConverter().Read(
                ref reader,
                typeof(CallbackEvent), // This is not used, use anything
                _options);
        }

        private class EmptyTestObject
        {
        }

        private class NewEvent : CallbackEvent
        {
            public string NewValue { get; set; }
            public bool SomeFlag { get; set; }
        }
    }
}

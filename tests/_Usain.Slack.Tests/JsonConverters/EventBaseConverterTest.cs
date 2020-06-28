namespace User.Slack.Tests.JsonConverters
{
    using System.Text;
    using System.Text.Json;
    using Usain.Slack.JsonConverters;
    using Usain.Slack.Models;
    using Xunit;

    public class EventBaseConverterTest
    {
        public const string UrlVerificationEventPayload =
            "{\"token\":\"token\",\"challenge\":\"challenge\",\"type\":\"url_verification\"}";

        [Fact]
        public void Read_With_EventType_Returns_Event()
        {
            var @event = new UrlVerificationEvent();
            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(UrlVerificationEventPayload));
            var converter = new EventBaseConverter();

            var actual = converter.Read(
                ref reader,
                typeof(UrlVerificationEvent),
                new JsonSerializerOptions());

            Assert.Equal("url_verification", actual.Type);
        }
    }
}

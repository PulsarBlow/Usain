namespace Usain.Slack.Tests.Models
{
    using Usain.Slack.Models;
    using Xunit;

    public class TimestampTest
    {
        public const long Timestamp = 1591452219;
        public const string Suffix = "0001";

        [Theory]
        [InlineData(
            0,
            "0001",
            false)]
        [InlineData(
            1,
            "",
            false)]
        [InlineData(
            0,
            "",
            true)]
        public void IsEmpty_Returns_Expected_Value(
            long timestamp,
            string suffix,
            bool expected)
        {
            var eventTimestamp = new Timestamp
            {
                Seconds = timestamp,
                Suffix = suffix,
            };
            Assert.Equal(
                expected,
                eventTimestamp.IsEmpty);
        }

        [Fact]
        public void Empty_Returns_Empty_Timestamp()
        {
            var actual = Usain.Slack.Models.Timestamp.Empty;
            Assert.Equal(
                0,
                actual.Seconds);
            Assert.Empty(actual.Suffix);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void TryParse_Returns_False_When_Value_Is_NullEmptyOrWhitespace(
            string value)
        {
            Assert.False(
                Usain.Slack.Models.Timestamp.TryParse(
                    value,
                    out _));
        }

        [Theory]
        [InlineData(
            "0",
            "0")]
        [InlineData(
            "1",
            "1")]
        [InlineData(
            "123456.06",
            "123456.06")]
        [InlineData(
            "A",
            "0")]
        [InlineData(
            "1.",
            "1")]
        [InlineData(
            ".ABC",
            "0")]
        public void ToString_Returns_Expected_Value(
            string value,
            string expected)
        {
            Usain.Slack.Models.Timestamp.TryParse(
                value,
                out var eventTimeStamp);

            Assert.Equal(
                expected,
                eventTimeStamp.ToString());
        }

        [Theory]
        [InlineData(
            "12345.001",
            "12345.002",
            false)]
        [InlineData(
            "12345.001",
            "123456.001",
            false)]
        [InlineData(
            "12345.001",
            "12345.001",
            true)]
        public void EqualityOperators_Returns_Expected_Value(
            string timestamp1,
            string timestamp2,
            bool areEqual)
        {
            Usain.Slack.Models.Timestamp.TryParse(
                timestamp1,
                out var ts1);
            Usain.Slack.Models.Timestamp.TryParse(
                timestamp2,
                out var ts2);

            Assert.Equal(
                areEqual,
                ts1 == ts2);
            Assert.Equal(
                !areEqual,
                ts1 != ts2);
        }
    }
}

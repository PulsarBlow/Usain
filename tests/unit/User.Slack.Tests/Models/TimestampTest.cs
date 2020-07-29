namespace User.Slack.Tests.Models
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
        [InlineData(
            1,
            Suffix,
            false)]
        [InlineData(
            Timestamp,
            "",
            false)]
        [InlineData(
            Timestamp,
            Suffix,
            true)]
        public void CompareTo_Returns_Expected_Value(
            long timestamp,
            string suffix,
            bool areEqual)
        {
            var expectedTs = new Timestamp
            {
                Seconds = Timestamp,
                Suffix = Suffix,
            };

            Assert.Equal(
                areEqual,
                0
                == expectedTs.CompareTo(
                    new Timestamp
                        { Seconds = timestamp, Suffix = suffix }));
        }

        [Fact]
        public void CompareTo_Equal_Self()
        {
            var timestamp = new Timestamp();
            Assert.Equal(
                0,
                timestamp.CompareTo(timestamp));
        }

        [Fact]
        public void CompareTo_Not_Equal_When_Null()
        {
            Assert.NotEqual(
                0,
                new Timestamp().CompareTo(null));
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
            "0", "0")]
        [InlineData(
            "1", "1")]
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
    }
}

namespace User.Slack.Tests.Security
{
    using System;
    using Usain.Slack.Security;
    using Xunit;

    public class SignatureVerifierTest
    {
        private const string Secret = "secret";
        private const int TimeDeltaToleranceSeconds = 60;

        [Fact]
        public void Verify_Returns_False_When_Timestamp_Has_Expired()
        {
            var expiredTimestamp =
                DateTimeOffset.UtcNow.AddSeconds(-TimeDeltaToleranceSeconds + 1)
                    .ToUnixTimeSeconds();
            var verifier = new SignatureVerifier(
                Secret,
                TimeSpan.FromSeconds(TimeDeltaToleranceSeconds));

            var actual = verifier.Verify(
                "signature",
                expiredTimestamp,
                "message");

            Assert.False(actual);
        }

        [Fact]
        public void Verify_Returns_False_When_Signature_Is_Not_Valid()
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var verifier = new SignatureVerifier(
                Secret,
                TimeSpan.FromSeconds(TimeDeltaToleranceSeconds));

            var actual = verifier.Verify(
                "signature",
                timestamp,
                "message");

            Assert.False(actual);
        }

        [Fact]
        public void Verify_Returns_True_When_Timestamp_And_Signature_Are_Valid()
        {
            var timestamp = DateTimeOffset.UnixEpoch.ToUnixTimeSeconds();
            var verifier = new SignatureVerifier(
                Secret,
                TimeSpan.FromSeconds(int.MaxValue));

            var actual = verifier.Verify(
                "v0=213cb52f067f4cd9faa388704ba5eac347bc79df40fc4491590cd4d8571b8af1",
                timestamp,
                "message");

            Assert.True(actual);
        }
    }
}

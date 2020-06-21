namespace Usain.Slack.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class SignatureVerifier
    {
        private readonly string _signingSecret;
        private readonly TimeSpan _timeTolerance;

        public SignatureVerifier(
            string signingSecret,
            TimeSpan timeTolerance)
        {
            _signingSecret = signingSecret;
            _timeTolerance = timeTolerance;
        }

        public bool Verify(
            string signature,
            long timestamp,
            string message)
        {
            if (!IsTimestampValid(timestamp)) return false;

            var generated = GenerateSignature(
                timestamp,
                message);
            return signature.Equals(
                generated,
                StringComparison.OrdinalIgnoreCase);
        }

        private bool IsTimestampValid(
            long timestamp)
        {
            var currentPosixTime = DateTimeOffset.UtcNow;
            var messagePosixTime =
                DateTimeOffset.FromUnixTimeSeconds(timestamp);
            return currentPosixTime - messagePosixTime <= _timeTolerance;
        }

        private string GenerateSignature(
            long timestamp,
            string message)
        {
            var signature = $"v0:{timestamp}:{message}";
            using var hasher =
                new HMACSHA256(Encoding.UTF8.GetBytes(_signingSecret));
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(signature));
            return $"v0={ByteArrayToHex(hash)}";
        }

        private static string ByteArrayToHex(
            byte[] value)
        {
            return BitConverter.ToString(value)
                .Replace(
                    "-",
                    "");
        }
    }
}

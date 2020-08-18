namespace Usain.Slack.Models
{
    using System;
    using System.Text.Json.Serialization;
    using JsonConverters;

    [JsonConverter(typeof(TimestampConverter))]
    public class Timestamp : IEquatable<Timestamp>
    {
        public long Seconds { get; set; }
        public string Suffix { get; set; } = string.Empty;

        public bool IsEmpty
            => Seconds == 0 && Suffix == string.Empty;

        public static Timestamp Empty
            => new Timestamp();

        public static bool TryParse(
            string value,
            out Timestamp timestamp)
        {
            timestamp = Empty;
            if (string.IsNullOrWhiteSpace(value)) { return false; }

            if (value.IndexOf('.') == -1)
            {
                return TryParsePartial(
                    value,
                    out timestamp);
            }

            return TryParseComplete(
                value,
                out timestamp);
        }

        private static bool TryParsePartial(
            string value,
            out Timestamp timestamp)
        {
            timestamp = Empty;
            if (!long.TryParse(
                value,
                out var seconds)) { return false; }

            timestamp.Seconds = seconds;
            return true;
        }

        private static bool TryParseComplete(
            string value,
            out Timestamp timestamp)
        {
            timestamp = Empty;
            var parts = value.Split(
                '.',
                StringSplitOptions.RemoveEmptyEntries);
            if (!long.TryParse(
                parts[0],
                out var seconds)) { return false; }

            timestamp.Seconds = seconds;
            if (parts.Length == 2) { timestamp.Suffix = parts[1]; }

            return true;
        }

        public bool Equals(
            Timestamp? other)
        {
            if (ReferenceEquals(
                null,
                other))
            {
                return false;
            }

            if (ReferenceEquals(
                this,
                other))
            {
                return true;
            }

            return Seconds == other.Seconds
                && Suffix == other.Suffix;
        }

        public override bool Equals(
            object? obj)
        {
            if (ReferenceEquals(
                null,
                obj))
            {
                return false;
            }

            if (ReferenceEquals(
                this,
                obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((Timestamp) obj);
        }

        public override int GetHashCode()
            => HashCode.Combine(
                Seconds,
                Suffix);

        public static bool operator ==(
            Timestamp? left,
            Timestamp? right)
            => Equals(
                left,
                right);

        public static bool operator !=(
            Timestamp? left,
            Timestamp? right)
            => !Equals(
                left,
                right);

        public override string ToString()
            => $"{Seconds}{(string.IsNullOrEmpty(Suffix) ? "" : ".")}{Suffix}";
    }
}

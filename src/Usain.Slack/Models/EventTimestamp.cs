namespace Usain.Slack.Models
{
    using System;
    using System.Text.Json.Serialization;
    using JsonConverters;

    [JsonConverter(typeof(EventTimestampConverter))]
    public class EventTimestamp : IComparable<EventTimestamp>
    {
        public long Timestamp { get; set; }
        public string Suffix { get; set; } = string.Empty;

        public bool IsEmpty
            => Timestamp == 0 && Suffix == string.Empty;

        public static EventTimestamp Empty
            => new EventTimestamp();

        public static bool TryParse(
            string value,
            out EventTimestamp eventTimestamp)
        {
            eventTimestamp = Empty;
            if (string.IsNullOrWhiteSpace(value)) { return false; }

            if (value.IndexOf('.') == -1)
            {
                return TryParsePartial(
                    value,
                    out eventTimestamp);
            }

            return TryParseComplete(
                value,
                out eventTimestamp);
        }

        private static bool TryParsePartial(
            string value,
            out EventTimestamp eventTimestamp)
        {
            eventTimestamp = Empty;
            if (!long.TryParse(
                value,
                out var timestamp)) { return false; }

            eventTimestamp.Timestamp = timestamp;
            return true;
        }

        private static bool TryParseComplete(
            string value,
            out EventTimestamp eventTimestamp)
        {
            eventTimestamp = Empty;
            var parts = value.Split(
                '.',
                StringSplitOptions.RemoveEmptyEntries);
            if (!long.TryParse(
                parts[0],
                out var timestamp)) { return false; }

            eventTimestamp.Timestamp = timestamp;
            if (parts.Length == 2) { eventTimestamp.Suffix = parts[1]; }

            return true;
        }

        public int CompareTo(
            EventTimestamp? other)
        {
            if (ReferenceEquals(
                this,
                other)) return 0;
            if (ReferenceEquals(
                null,
                other)) return 1;

            var timestampComparison = Timestamp.CompareTo(other.Timestamp);
            if (timestampComparison != 0) return timestampComparison;

            return string.Compare(
                Suffix,
                other.Suffix,
                StringComparison.Ordinal);
        }

        public override string ToString()
            => $"{Timestamp}{(string.IsNullOrEmpty(Suffix) ? "" : ".")}{Suffix}";
    }
}

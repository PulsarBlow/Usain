namespace Usain.Slack.Models
{
    using System;
    using System.Text.Json.Serialization;
    using JsonConverters;

    [JsonConverter(typeof(EventTimestampConverter))]
    public class EventTimestamp
    {
        public long Timestamp { get; set; }
        public string Suffix { get; set; } = string.Empty;

        public bool IsEmpty()
        {
            return Timestamp == 0 && Suffix == string.Empty;
        }

        public static EventTimestamp Empty
            => new EventTimestamp();

        public static bool TryParse(
            string value,
            out EventTimestamp eventTimestamp)
        {
            eventTimestamp = Empty;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var parts = value.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (parts?.Length != 2) return false;

            long.TryParse(parts[0], out var timestamp);
            eventTimestamp.Timestamp = timestamp;
            eventTimestamp.Suffix = parts[1];
            return true;
        }
    }
}

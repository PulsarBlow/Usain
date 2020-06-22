namespace Usain.Core.Serialization
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;

    public static class ObjectSerializer
    {
        private static readonly JsonSerializerOptions Options =
            new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

        public static string ToString(
            object o)
        {
            return JsonSerializer.Serialize(
                o,
                Options);
        }

        [return: MaybeNull]
        public static T FromString<T>(
            string? value)
        {
            if (string.IsNullOrEmpty(value)) { return default; }

            return JsonSerializer.Deserialize<T>(
                value,
                Options);
        }
    }
}

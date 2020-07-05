namespace Usain.EventListener.Extensions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Serialization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Net.Http.Headers;

    internal static class HttpResponseExtensions
    {
        public const string CacheControlValue = "no-store, no-cache, max-age=0";
        public const string PragmaValue = "no-cache";

        public static void SetNoCache(
            this HttpResponse httpResponse)
        {
            httpResponse.Headers[HeaderNames.CacheControl] = CacheControlValue;
            httpResponse.Headers[HeaderNames.Pragma] = PragmaValue;
        }

        public static async Task WriteJsonAsync(
            this HttpResponse response,
            object value,
            string? contentType = null,
            CancellationToken cancellationToken = default)
        {
            var json = ObjectSerializer.ToString(value);
            await response.WriteJsonAsync(
                json,
                contentType,
                cancellationToken);
        }

        public static async Task WriteJsonAsync(
            this HttpResponse response,
            string json,
            string? contentType = null,
            CancellationToken cancellationToken = default)
        {
            response.ContentType =
                contentType ?? "application/json; charset=UTF-8";
            await response.WriteAsync(
                json,
                cancellationToken);
            await response.Body.FlushAsync(cancellationToken);
        }
    }
}

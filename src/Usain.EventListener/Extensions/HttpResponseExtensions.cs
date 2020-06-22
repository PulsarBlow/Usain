namespace Usain.EventListener.Extensions
{
    using System.Threading.Tasks;
    using Core.Serialization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Net.Http.Headers;

    public static class HttpResponseExtensions
    {
        public static void SetNoCache(this HttpResponse httpResponse)
        {
            if (!httpResponse.Headers.ContainsKey(HeaderNames.CacheControl))
            {
                httpResponse.Headers.Add(HeaderNames.CacheControl, "no-store, no-cache, max-age=0");
            }
            else
            {
                httpResponse.Headers[HeaderNames.CacheControl] = "no-store, no-cache, max-age=0";
            }

            if (!httpResponse.Headers.ContainsKey(HeaderNames.Pragma))
            {
                httpResponse.Headers.Add(HeaderNames.Pragma, "no-cache");
            }
        }

        public static async Task WriteJsonAsync(
            this HttpResponse response,
            object value,
            string? contentType = null)
        {
            var json = ObjectSerializer.ToString(value);
            await response.WriteJsonAsync(json, contentType);
            await response.Body.FlushAsync();
        }

        public static async Task WriteJsonAsync(
            this HttpResponse response,
            string json,
            string? contentType = null)
        {
            response.ContentType =
                contentType ?? "application/json; charset=UTF-8";
            await response.WriteAsync(json);
            await response.Body.FlushAsync();
        }
    }
}

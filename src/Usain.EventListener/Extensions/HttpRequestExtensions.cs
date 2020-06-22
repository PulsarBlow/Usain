namespace Usain.EventListener.Extensions
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Serialization;
    using Microsoft.AspNetCore.Http;

    public static class HttpRequestExtensions
    {
        public static async Task<string?> ReadAsync(
            this HttpRequest request,
            CancellationToken cancellationToken)
        {
            // Allows reading body stream several time in ASP.Net Core
            request.EnableBuffering();

            if (!request.Body.CanSeek) return null;

            string bodyContent;
            request.Body.Seek(
                0,
                SeekOrigin.Begin);
            using (var reader = new StreamReader(
                request.Body,
                Encoding.UTF8,
                false,
                8192,
                true)) { bodyContent = await reader.ReadToEndAsync(); }
            request.Body.Seek(
                0,
                SeekOrigin.Begin);

            return bodyContent;
        }

        [return: MaybeNull]
        public static async Task<T> ReadJsonAsync<T>(
            this HttpRequest request,
            CancellationToken cancellationToken)
        {
            var body = await ReadAsync(
                request,
                cancellationToken);
            return ObjectSerializer.FromString<T>(body)!;
        }
    }
}

namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.Results
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    internal class StatusCodeEndpointResult : IEndpointResult
    {
        public int StatusCode { get; }

        public StatusCodeEndpointResult(
            int statusCode)
        {
            StatusCode = statusCode;
        }

        public Task ExecuteAsync(
            HttpContext context,
            CancellationToken cancellationToken = default)
        {
            context.Response.StatusCode = StatusCode;
            return Task.CompletedTask;
        }
    }
}

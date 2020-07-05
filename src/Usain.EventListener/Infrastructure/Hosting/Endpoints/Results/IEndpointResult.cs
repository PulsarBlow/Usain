namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.Results
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    internal interface IEndpointResult
    {
        int StatusCode { get; }

        Task ExecuteAsync(HttpContext context, CancellationToken cancellationToken);
    }
}

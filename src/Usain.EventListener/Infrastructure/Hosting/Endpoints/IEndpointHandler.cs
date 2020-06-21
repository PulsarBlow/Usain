namespace Usain.EventListener.Infrastructure.Hosting.Endpoints
{
    using System.Threading;
    using System.Threading.Tasks;
    using Endpoints.Results;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Endpoint handler
    /// </summary>
    public interface IEndpointHandler
    {
        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="cancellationToken">Cancellation token for this async operation</param>
        /// <returns></returns>
        Task<IEndpointResult> ProcessAsync(HttpContext context, CancellationToken cancellationToken = default);
    }
}

namespace Usain.EventListener.Infrastructure.Hosting.Endpoints
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Results;

    /// <summary>
    /// Endpoint handler
    /// </summary>
    internal interface IEndpointHandler
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

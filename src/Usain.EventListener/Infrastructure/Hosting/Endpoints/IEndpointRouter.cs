namespace Usain.EventListener.Infrastructure.Hosting.Endpoints
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// The endpoint router
    /// </summary>
    internal interface IEndpointRouter
    {
        /// <summary>
        /// Finds a matching endpoint
        /// </summary>
        /// <param name="context">The HttpContext</param>
        /// <returns></returns>
        IEndpointHandler? Find(HttpContext context);
    }
}

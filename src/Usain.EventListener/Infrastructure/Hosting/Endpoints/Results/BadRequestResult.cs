namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.Results
{
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.AspNetCore.Http;

    internal class BadRequestResult : IEndpointResult
    {
        public string? Error { get; set; }
        public string? ErrorDescription { get; set; }

        public BadRequestResult(
            string? error = null,
            string? errorDescription = null)
        {
            Error = error;
            ErrorDescription = errorDescription;
        }

        public async Task ExecuteAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.SetNoCache();

            if (!string.IsNullOrEmpty(Error))
            {
                await context.Response.WriteJsonAsync(new
                    { error = Error, error_description = ErrorDescription });
            }
        }
    }
}

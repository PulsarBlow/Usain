namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.Results
{
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.AspNetCore.Http;

    public class OkResult : IEndpointResult
    {
        private readonly object? _value;

        public OkResult(
            object? value = null)
        {
            _value = value;
        }

        public async Task ExecuteAsync(
            HttpContext context)
        {
            context.Response.SetNoCache();
            context.Response.StatusCode =
                StatusCodes.Status200OK;

            if (_value != null)
            {
                await context.Response.WriteJsonAsync(_value);
            }
        }
    }
}

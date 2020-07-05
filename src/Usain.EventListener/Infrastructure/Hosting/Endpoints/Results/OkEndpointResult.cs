namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.Results
{
    using System.Threading;
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.AspNetCore.Http;

    public class OkEndpointResult<TBodyContent> : IEndpointResult
    {
        public int StatusCode { get; } = StatusCodes.Status200OK;
        public TBodyContent BodyContent { get; }

        public OkEndpointResult(
            TBodyContent bodyContent)
            => BodyContent = bodyContent;

        public async Task ExecuteAsync(
            HttpContext context,
            CancellationToken cancellationToken)
        {
            context.Response.SetNoCache();
            context.Response.StatusCode = StatusCode;

            if (BodyContent != null)
            {
                await context.Response.WriteJsonAsync(
                    BodyContent,
                    cancellationToken: cancellationToken);
            }
        }
    }

    public class OkEndpointResult : OkEndpointResult<object?>
    {
        public OkEndpointResult(
            object? bodyContent = null)
            : base(bodyContent)
        {
        }
    }
}

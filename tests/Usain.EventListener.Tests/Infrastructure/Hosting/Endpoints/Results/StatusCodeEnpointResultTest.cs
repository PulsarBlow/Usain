namespace Usain.EventListener.Tests.Infrastructure.Hosting.Endpoints.Results
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventListener.Infrastructure.Hosting.Endpoints.Results;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    public class StatusCodeEnpointResultTest
    {
        [Theory]
        [InlineData(StatusCodes.Status100Continue)]
        [InlineData(StatusCodes.Status400BadRequest)]
        [InlineData(StatusCodes.Status422UnprocessableEntity)]
        public async Task ExecuteAsync_Set_Response_StatusCode(int statusCode)
        {
            var httpContext =
                Mock.Of<HttpContext>(x => x.Response.StatusCode == statusCode);
            var result = new StatusCodeEndpointResult(statusCode);
            await result.ExecuteAsync(
                httpContext,
                CancellationToken.None);
        }
    }
}

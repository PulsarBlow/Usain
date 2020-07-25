namespace Usain.EventListener.Tests.Infrastructure.Hosting.Endpoints.Results
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventListener.Extensions;
    using EventListener.Infrastructure.Hosting.Endpoints.Results;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    public class OkEndpointResultTest
    {
        private readonly Mock<HttpContext> _httpContextMock =
            new Mock<HttpContext>();
        private readonly HeaderDictionary _headers = new HeaderDictionary();

        public OkEndpointResultTest()
        {
            _httpContextMock
                .Setup(x => x.Response)
                .Returns(Mock.Of<HttpResponse>());
            _httpContextMock
                .SetupGet(x => x.Response.Headers)
                .Returns(_headers);
        }

        [Fact]
        public async Task
            ExecuteAsync_Sets_Expected_CachePolicy_And_StatusCode()
        {
            var result = new OkEndpointResult();
            await result.ExecuteAsync(
                _httpContextMock.Object,
                CancellationToken.None);

            Assert.Collection(
                _headers,
                kvp => Assert.Equal(
                    HttpResponseExtensions.CacheControlValue,
                    kvp.Value),
                kvp
                    => Assert.Equal(
                        HttpResponseExtensions.PragmaValue,
                        kvp.Value));
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
    }
}

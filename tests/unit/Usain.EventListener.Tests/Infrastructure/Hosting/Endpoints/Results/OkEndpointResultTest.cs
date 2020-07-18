namespace Usain.EventListener.Tests.Infrastructure.Hosting.Endpoints.Results
{
    using System;
    using System.IO;
    using System.IO.Pipelines;
    using System.Threading;
    using System.Threading.Tasks;
    using EventListener.Extensions;
    using EventListener.Infrastructure.Hosting.Endpoints.Results;
    using EventListener.Infrastructure.Hosting.Endpoints.Results.Responses;
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
        }

        [Fact]
        public async Task ExecuteAsync__Write_BodyContent_To_Response()
        {
            var eventStoreId = Guid.NewGuid();
            await using var stream = new MemoryStream();
            var mockPipeWriter = PipeWriter.Create(
                stream,
                new StreamPipeWriterOptions());

            _httpContextMock
                .SetupGet(x => x.Response.Body)
                .Returns(stream);
            _httpContextMock
                .SetupGet(x => x.Response.BodyWriter)
                .Returns(mockPipeWriter);

            var result = new OkEndpointResult<CallbackEventResponse>(new CallbackEventResponse(eventStoreId));
            await result.ExecuteAsync(
                _httpContextMock.Object,
                CancellationToken.None);

            _httpContextMock.VerifyAll();
        }
    }
}

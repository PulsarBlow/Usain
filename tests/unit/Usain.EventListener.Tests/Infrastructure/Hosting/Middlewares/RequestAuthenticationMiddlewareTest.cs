namespace Usain.EventListener.Tests.Infrastructure.Hosting.Middlewares
{
    using System;
    using System.Threading.Tasks;
    using EventListener.Infrastructure.Hosting.Middlewares;
    using EventListener.Infrastructure.Security;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class RequestAuthenticationMiddlewareTest
    {
        private readonly Mock<ILogger<RequestAuthenticationMiddleware>>
            _loggerMock
                = new Mock<ILogger<RequestAuthenticationMiddleware>>();
        private readonly Mock<IRequestAuthenticator> _requestAuthenticatorMock =
            new Mock<IRequestAuthenticator>();
        private readonly Mock<HttpContext> _httpContextMock =
            new Mock<HttpContext>();
        private readonly Mock<HttpResponse> _httpResponseMock =
            new Mock<HttpResponse>();
        private readonly HeaderDictionary _headers = new HeaderDictionary();
        private int _countNextCalls;
        private readonly RequestDelegate _next;

        public RequestAuthenticationMiddlewareTest()
        {
            _requestAuthenticatorMock
                .Setup(x => x.IsAuthenticAsync(It.IsAny<HttpRequest>()))
                .Returns(Task.FromResult(true));
            _httpResponseMock
                .SetupGet(x => x.Headers)
                .Returns(_headers);
            _httpContextMock
                .SetupGet(x => x.Response)
                .Returns(_httpResponseMock.Object);
            _next = x =>
            {
                _countNextCalls++;
                return Task.CompletedTask;
            };
        }

        [Fact]
        public async Task
            InvokeAsync_Returns_Unauthorized_When_Authentication_Fails()
        {
            _requestAuthenticatorMock
                .Setup(x => x.IsAuthenticAsync(It.IsAny<HttpRequest>()))
                .Returns(Task.FromResult(false));
            _httpResponseMock
                .SetupSet(
                    x => x.StatusCode = StatusCodes.Status401Unauthorized);

            var middleware = CreateMiddleware();

            await middleware.InvokeAsync(
                _httpContextMock.Object,
                _next);

            // next has not been called
            Assert.Equal(
                0,
                _countNextCalls);
            Mock.VerifyAll();
        }

        [Fact]
        public async Task InvokeAsync_Calls_Next_When_Authentication_Succeeds()
        {
            var middleware = CreateMiddleware();

            await middleware.InvokeAsync(
                _httpContextMock.Object,
                _next);

            // next has been called
            Assert.Equal(
                1,
                _countNextCalls);
            Mock.VerifyAll();
        }

        [Fact]
        public async Task InvokeAsync_Throws_When_Exception_Occurs()
        {
            _requestAuthenticatorMock
                .Setup(x => x.IsAuthenticAsync(It.IsAny<HttpRequest>()))
                .Throws<Exception>();

            var middleware = CreateMiddleware();

            await Assert.ThrowsAsync<Exception>(
                () => middleware.InvokeAsync(
                    _httpContextMock.Object,
                    _next));

            // next has not been called
            Assert.Equal(
                0,
                _countNextCalls);
            Mock.VerifyAll();
        }

        private RequestAuthenticationMiddleware CreateMiddleware()
            => new RequestAuthenticationMiddleware(
                _loggerMock.Object,
                _requestAuthenticatorMock.Object);
    }
}

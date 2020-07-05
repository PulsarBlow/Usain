namespace Usain.EventListener.Tests.Infrastructure.Hosting.Middlewares
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventListener.Infrastructure.Hosting.Endpoints;
    using EventListener.Infrastructure.Hosting.Endpoints.Results;
    using EventListener.Infrastructure.Hosting.Middlewares;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class EventListenerMiddlewareTest
    {
        private readonly Mock<ILogger<EventListenerMiddleware>>
            _loggerMock
                = new Mock<ILogger<EventListenerMiddleware>>();
        private readonly Mock<IEndpointRouter> _endpointRouterMock =
            new Mock<IEndpointRouter>();
        private readonly Mock<IEndpointHandler> _endpointHandlerMock =
            new Mock<IEndpointHandler>();
        private readonly Mock<IEndpointResult> _endpointResultMock =
            new Mock<IEndpointResult>();
        private readonly Mock<HttpContext> _httpContextMock =
            new Mock<HttpContext>();
        private readonly Mock<HttpResponse> _httpResponseMock =
            new Mock<HttpResponse>();
        private readonly HeaderDictionary _headers = new HeaderDictionary();
        private int _countNextCalls = 0;
        private readonly RequestDelegate _next;

        public EventListenerMiddlewareTest()
        {
            _httpResponseMock
                .SetupGet(x => x.Headers)
                .Returns(_headers);
            _httpContextMock
                .SetupGet(x => x.Response)
                .Returns(_httpResponseMock.Object);
            _httpContextMock
                .SetupGet(x => x.Request.Path)
                .Returns("/path");
            _next = x =>
            {
                _countNextCalls++;
                return Task.CompletedTask;
            };

            _endpointHandlerMock
                .Setup(
                    x => x.ProcessAsync(
                        It.IsAny<HttpContext>(),
                        It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(_endpointResultMock.Object));
            _endpointRouterMock
                .Setup(x => x.Find(It.IsAny<HttpContext>()))
                .Returns(_endpointHandlerMock.Object);
        }

        [Fact]
        public async Task InvokeAsync_Calls_Next_When_No_Endpoint_Matches()
        {
            _endpointRouterMock
                .Setup(x => x.Find(It.IsAny<HttpContext>()))
                .Returns<IEndpointHandler>(null);

            var middleware = CreateMiddleware();
            await middleware.InvokeAsync(
                _httpContextMock.Object,
                _next);

            // next has been called one time
            Assert.Equal(1, _countNextCalls);
        }

        [Fact]
        public async Task InvokeAsync_Throws_When_Exception_Occurs()
        {
            _endpointHandlerMock
                .Setup(
                    x => x.ProcessAsync(
                        It.IsAny<HttpContext>(),
                        It.IsAny<CancellationToken>()))
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

        [Fact]
        public async Task InvokeAsync_Executes_EndpointResult()
        {
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

        private EventListenerMiddleware CreateMiddleware()
        {
            return new EventListenerMiddleware(
                _loggerMock.Object,
                _endpointRouterMock.Object);
        }
    }
}

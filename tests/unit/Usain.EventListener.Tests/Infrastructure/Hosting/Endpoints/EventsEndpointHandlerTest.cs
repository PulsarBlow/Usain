namespace Usain.EventListener.Tests.Infrastructure.Hosting.Endpoints
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using EventListener.Infrastructure.Hosting.Endpoints;
    using EventListener.Infrastructure.Hosting.Endpoints.ResultGenerators;
    using EventListener.Infrastructure.Hosting.Endpoints.Results;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Slack.Models.Events;
    using Xunit;

    public class EventsEndpointHandlerTest
    {
        private const string EventCallbackBodyContent =
            "{\"type\":\"event_callback\"}";
        private const string UrlVerificationBodyContent =
            "{\"type\":\"url_verification\"}";
        private const string AppRateLimitedBodyContent =
            "{\"type\":\"app_rate_limited\"}";
        private const string UnsupportedBodyContent =
            "{\"type\":\"some_event\"}";
        private readonly Mock<ILogger<EventsEndpointHandler>> _loggerMock =
            new Mock<ILogger<EventsEndpointHandler>>();
        private readonly
            Mock<IEventsEndpointResultGenerator<UrlVerificationEvent>>
            _urlVerificationResultGeneratorMock =
                new Mock<IEventsEndpointResultGenerator<UrlVerificationEvent>
                >();
        private readonly
            Mock<IEventsEndpointResultGenerator<AppRateLimitedEvent>>
            _appRateLimitedResultGeneratorMock =
                new Mock<IEventsEndpointResultGenerator<AppRateLimitedEvent>>();
        private readonly Mock<IEventsEndpointResultGenerator<EventWrapper>>
            _callbackEventResultGeneratorMock =
                new Mock<IEventsEndpointResultGenerator<EventWrapper>>();
        private readonly Mock<HttpContext> _httpContextMock =
            new Mock<HttpContext>();

        public EventsEndpointHandlerTest()
        {
            _httpContextMock
                .Setup(x => x.Request.Body)
                .Returns(
                    new MemoryStream(
                        Encoding.UTF8.GetBytes(EventCallbackBodyContent)));
            _httpContextMock
                .Setup(x => x.Request.Method)
                .Returns(HttpMethods.Post);
            _urlVerificationResultGeneratorMock
                .Setup(
                    x => x.GenerateResult(
                        It.IsNotNull<UrlVerificationEvent>(),
                        It.IsAny<CancellationToken>()))
                .Returns(
                    Task.FromResult<IEndpointResult>(new OkEndpointResult()));
            _appRateLimitedResultGeneratorMock
                .Setup(
                    x => x.GenerateResult(
                        It.IsNotNull<AppRateLimitedEvent>(),
                        It.IsAny<CancellationToken>()))
                .Returns(
                    Task.FromResult<IEndpointResult>(new OkEndpointResult()));
            _callbackEventResultGeneratorMock
                .Setup(
                    x => x.GenerateResult(
                        It.IsNotNull<EventWrapper>(),
                        It.IsAny<CancellationToken>()))
                .Returns(
                    Task.FromResult<IEndpointResult>(new OkEndpointResult()));
        }

        [Fact]
        public async Task
            ProcessAsync_Returns_MethodNotAllowed_When_HttpMethod_Is_Not_Post()
        {
            _httpContextMock
                .Setup(x => x.Request.Method)
                .Returns(HttpMethods.Get);

            var handler = CreateHandler();

            var actual = await handler.ProcessAsync(
                _httpContextMock.Object,
                CancellationToken.None);

            var result = Assert.IsType<StatusCodeEndpointResult>(actual);
            Assert.Equal(
                StatusCodes.Status405MethodNotAllowed,
                result.StatusCode);
        }

        [Fact]
        public async Task
            ProcessAsync_Returns_UnprocessableEntity_When_Event_Deserialization_Fails()
        {
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(""));
            _httpContextMock
                .Setup(x => x.Request.Body)
                .Returns(memoryStream);
            var handler = CreateHandler();

            var actual = await handler.ProcessAsync(
                _httpContextMock.Object,
                CancellationToken.None);

            var result = Assert.IsType<StatusCodeEndpointResult>(actual);
            Assert.Equal(
                StatusCodes.Status422UnprocessableEntity,
                result.StatusCode);
        }

        [Theory]
        [InlineData(
            UrlVerificationBodyContent,
            typeof(OkEndpointResult))]
        [InlineData(
            AppRateLimitedBodyContent,
            typeof(OkEndpointResult))]
        [InlineData(
            EventCallbackBodyContent,
            typeof(OkEndpointResult))]
        [InlineData(
            UnsupportedBodyContent,
            typeof(StatusCodeEndpointResult))]
        public async Task ProcessAsync_Returns_Expected_EndpointResult(
            string bodyContent,
            Type expected)
        {
            var memoryStream =
                new MemoryStream(Encoding.UTF8.GetBytes(bodyContent));
            _httpContextMock
                .Setup(x => x.Request.Body)
                .Returns(memoryStream);
            var handler = CreateHandler();

            var actual = await handler.ProcessAsync(
                _httpContextMock.Object,
                CancellationToken.None);

            Assert.IsType(
                expected,
                actual);
        }

        private EventsEndpointHandler CreateHandler()
            => new EventsEndpointHandler(
                _loggerMock.Object,
                _urlVerificationResultGeneratorMock.Object,
                _appRateLimitedResultGeneratorMock.Object,
                _callbackEventResultGeneratorMock.Object);
    }
}

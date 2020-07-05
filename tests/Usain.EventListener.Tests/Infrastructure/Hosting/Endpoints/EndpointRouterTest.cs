namespace Usain.EventListener.Tests.Infrastructure.Hosting.Endpoints
{
    using System;
    using System.Collections.Generic;
    using EventListener.Infrastructure.Hosting.Endpoints;
    using EventListener.Infrastructure.Hosting.Endpoints.ResultGenerators;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Slack.Models;
    using Xunit;
    using Endpoint = EventListener.Infrastructure.Hosting.Endpoints.Endpoint;

    public class EndpointRouterTest
    {
        private readonly Mock<ILogger<EndpointRouter>> _loggerMock =
            new Mock<ILogger<EndpointRouter>>();
        private readonly List<Endpoint> _endpoints = new List<Endpoint>();
        private readonly Mock<IServiceProvider> _serviceProviderMock =
            new Mock<IServiceProvider>();
        private readonly EventsEndpointHandler _handler =
            new EventsEndpointHandler(
                Mock.Of<ILogger<EventsEndpointHandler>>(),
                Mock.Of<IEventsEndpointResultGenerator<UrlVerificationEvent>>(),
                Mock.Of<IEventsEndpointResultGenerator<AppRateLimitedEvent>>(),
                Mock.Of<IEventsEndpointResultGenerator<EventWrapper>>());

        public EndpointRouterTest()
        {
            _endpoints.Add(
                new Endpoint(
                    "events",
                    "/events",
                    typeof(EventsEndpointHandler)));

            _endpoints.Add(
                new Endpoint(
                    "events",
                    "/events_bad",
                    typeof(BadHandler)));

            _serviceProviderMock
                .Setup(x => x.GetService(typeof(EventsEndpointHandler)))
                .Returns(_handler);

            _serviceProviderMock
                .Setup(x => x.GetService(typeof(BadHandler)))
                .Returns(new BadHandler());
        }

        [Fact]
        public void Find_Returns_Null_When_Handler_Not_Found()
        {
            var router = CreateRouter();

            Assert.Null(router.Find(new DefaultHttpContext()));
        }

        [Fact]
        public void Find_Returns_Handler_When_Endpoint_Matches()
        {
            var router = CreateRouter();

            var actual = router.Find(
                Mock.Of<HttpContext>(
                    x => x.Request.Path == "/events"
                        && x.RequestServices == _serviceProviderMock.Object));
            Assert.Equal(
                _handler,
                actual);
        }

        [Fact]
        public void
            Find_Returns_Null_When_Registered_Handler_DoesNot_Implement_IEndpointHandler()
        {
            _serviceProviderMock
                .Setup(x => x.GetService(typeof(BadHandler)))
                .Returns(new BadHandler());

            var router = CreateRouter();
            var actual = router.Find(
                Mock.Of<HttpContext>(
                    x => x.Request.Path == "/events_bad"
                        && x.RequestServices == _serviceProviderMock.Object));

            Assert.Null(actual);
        }

        private EndpointRouter CreateRouter()
        {
            return new EndpointRouter(
                _loggerMock.Object,
                _endpoints);
        }

        private class BadHandler
        {
        }
    }
}

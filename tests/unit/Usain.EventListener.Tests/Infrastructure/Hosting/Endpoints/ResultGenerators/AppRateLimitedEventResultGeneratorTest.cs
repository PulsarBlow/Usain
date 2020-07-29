namespace Usain.EventListener.Tests.Infrastructure.Hosting.Endpoints.
    ResultGenerators
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventListener.Commands;
    using EventListener.Commands.AcknowledgeAppRateLimit;
    using EventListener.Infrastructure.Hosting.Endpoints.ResultGenerators;
    using EventListener.Infrastructure.Hosting.Endpoints.Results;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Slack.Models.Events;
    using Xunit;

    public class AppRateLimitedEventResultGeneratorTest
    {
        private readonly Mock<ILogger<AppRateLimitedEventResultGenerator>>
            _loggerMock =
                new Mock<ILogger<AppRateLimitedEventResultGenerator>>();
        private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        [Theory]
        [InlineData(
            CommandResultType.Aborted,
            typeof(StatusCodeEndpointResult),
            StatusCodes.Status422UnprocessableEntity)]
        [InlineData(
            CommandResultType.Failure,
            typeof(StatusCodeEndpointResult),
            StatusCodes.Status422UnprocessableEntity)]
        [InlineData(
            CommandResultType.Success,
            typeof(OkEndpointResult),
            StatusCodes.Status200OK)]
        public async Task
            GenerateResult_Returns_Expected_EndpointResult(
                CommandResultType commandResultType,
                Type expectedResultType,
                int expectedStatusCode)
        {
            _mediatorMock
                .Setup(
                    x => x.Send(
                        It.IsAny<AcknowledgeAppRateLimitCommand>(),
                        It.IsAny<CancellationToken>()))
                .Returns(
                    Task.FromResult(
                        new CommandResult(
                            Guid.NewGuid(),
                            commandResultType)));
            var generator = CreateGenerator();

            var actual = await generator.GenerateResult(
                new AppRateLimitedEvent(),
                CancellationToken.None);
            Assert.IsType(
                expectedResultType,
                actual);
            Assert.Equal(
                expectedStatusCode,
                actual.StatusCode);
        }

        private AppRateLimitedEventResultGenerator CreateGenerator()
            => new AppRateLimitedEventResultGenerator(
                _loggerMock.Object,
                _mediatorMock.Object);
    }
}

namespace Usain.EventListener.Tests.Infrastructure.Hosting.Endpoints.ResultGenerators
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventListener.Commands;
    using EventListener.Commands.IngestEvent;
    using EventListener.Infrastructure.Hosting.Endpoints.ResultGenerators;
    using EventListener.Infrastructure.Hosting.Endpoints.Results;
    using EventListener.Infrastructure.Hosting.Endpoints.Results.Responses;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Slack.Models;
    using Xunit;

    public class CallbackEventResultGeneratorTest
    {
                private readonly Mock<ILogger<CallbackEventResultGenerator>>
            _loggerMock =
                new Mock<ILogger<CallbackEventResultGenerator>>();
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
            typeof(OkEndpointResult<CallbackEventResponse>),
            StatusCodes.Status200OK)]
        public async Task
            GenerateResult_Returns_Expected_EndpointResult(
                CommandResultType commandResultType,
                Type expectedResultType,
                int expectedStatusCode)
        {
            var eventStoreId = Guid.NewGuid();
            _mediatorMock
                .Setup(
                    x => x.Send(
                        It.IsAny<IngestEventCommand>(),
                        It.IsAny<CancellationToken>()))
                .Returns(
                    Task.FromResult(
                        new IngestEventCommandResult(eventStoreId, commandResultType)));
            var generator = CreateGenerator();

            var actual = await generator.GenerateResult(
                new EventWrapper(),
                CancellationToken.None);

            Assert.IsType(
                expectedResultType,
                actual);
            Assert.Equal(
                expectedStatusCode,
                actual.StatusCode);
            if (actual is OkEndpointResult<CallbackEventResponse> okEndpointResult)
            {
                Assert.Equal(
                    eventStoreId,
                    okEndpointResult.BodyContent.EventStoreId);
            }
        }

        private CallbackEventResultGenerator CreateGenerator()
            => new CallbackEventResultGenerator(
                _loggerMock.Object,
                _mediatorMock.Object);
    }
}

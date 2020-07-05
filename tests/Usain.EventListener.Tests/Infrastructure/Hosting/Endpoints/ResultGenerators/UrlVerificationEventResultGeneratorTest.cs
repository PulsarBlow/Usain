namespace Usain.EventListener.Tests.Infrastructure.Hosting.Endpoints.
    ResultGenerators
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventListener.Commands;
    using EventListener.Commands.VerifyUrl;
    using EventListener.Infrastructure.Hosting.Endpoints.ResultGenerators;
    using EventListener.Infrastructure.Hosting.Endpoints.Results;
    using EventListener.Infrastructure.Hosting.Endpoints.Results.Responses;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Slack.Models;
    using Xunit;

    public class UrlVerificationEventResultGeneratorTest
    {
        private readonly Mock<ILogger<UrlVerificationEventResultGenerator>>
            _loggerMock =
                new Mock<ILogger<UrlVerificationEventResultGenerator>>();
        private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task
            GenerateResult_Returns_BadRequest_When_Event_Has_No_Challenge()
        {
            var generator = CreateGenerator();
            var actual = await generator.GenerateResult(
                new UrlVerificationEvent(),
                CancellationToken.None);
            Assert.IsType<StatusCodeEndpointResult>(actual);
            Assert.Equal(StatusCodes.Status400BadRequest, actual.StatusCode);
        }

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
            typeof(OkEndpointResult<UrlVerificationResponse>),
            StatusCodes.Status200OK)]
        public async Task
            GenerateResult_Returns_Expected_EndpointResult(
                CommandResultType commandResultType,
                Type expectedResultType,
                int expectedStatusCode)
        {
            const string challenge = "challenge";
            _mediatorMock
                .Setup(
                    x => x.Send(
                        It.IsAny<VerifyUrlCommand>(),
                        It.IsAny<CancellationToken>()))
                .Returns(
                    Task.FromResult(
                        new VerifyUrlCommandResult(
                            challenge,
                            commandResultType)));
            var generator = CreateGenerator();

            var actual = await generator.GenerateResult(
                new UrlVerificationEvent { Challenge = challenge },
                CancellationToken.None);

            Assert.IsType(
                expectedResultType,
                actual);
            Assert.Equal(
                expectedStatusCode,
                actual.StatusCode);
            if (actual is OkEndpointResult<UrlVerificationResponse> okEndpointResult)
            {
                Assert.Equal(
                    challenge,
                    okEndpointResult.BodyContent.Challenge);
            }
        }

        private UrlVerificationEventResultGenerator CreateGenerator()
        {
            return new UrlVerificationEventResultGenerator(
                _loggerMock.Object,
                _mediatorMock.Object);
        }
    }
}

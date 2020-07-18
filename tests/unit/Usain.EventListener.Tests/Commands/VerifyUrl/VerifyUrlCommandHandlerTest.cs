namespace Usain.EventListener.Tests.Commands.VerifyUrl
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventListener.Commands;
    using EventListener.Commands.VerifyUrl;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class VerifyUrlCommandHandlerTest
    {
        private const string Challenge = "challenge";
        private readonly Mock<ILogger<VerifyUrlCommandHandler>> _loggerMock =
            new Mock<ILogger<VerifyUrlCommandHandler>>();

        [Fact]
        public async Task
            Handle_Returns_Aborted_CommandResult_When_Cancellation_Is_Requested()
        {
            var cancellationToken = new CancellationToken(true);
            var handler = CreateHandler();

            var actual = await handler.Handle(
                new VerifyUrlCommand(Challenge),
                cancellationToken);

            Assert.Equal(
                CommandResultType.Aborted,
                actual.ResultType);
            Assert.Equal(
                Challenge,
                actual.Challenge);
        }

        [Theory]
        [InlineData(
            null,
            "",
            CommandResultType.Failure)]
        [InlineData(
            "",
            "",
            CommandResultType.Failure)]
        [InlineData(
            " ",
            " ",
            CommandResultType.Success)]
        [InlineData(
            Challenge,
            Challenge,
            CommandResultType.Success)]
        public async Task
            Handle_Returns_Expected_CommandResult(
                string challenge,
                string expectedChallenge,
                CommandResultType expectedResultType)
        {
            var handler = CreateHandler();

            var actual = await handler.Handle(
                new VerifyUrlCommand(challenge),
                CancellationToken.None);

            Assert.Equal(
                expectedResultType,
                actual.ResultType);
            Assert.Equal(
                expectedChallenge,
                actual.Challenge);
        }

        private VerifyUrlCommandHandler CreateHandler()
            => new VerifyUrlCommandHandler(_loggerMock.Object);
    }
}

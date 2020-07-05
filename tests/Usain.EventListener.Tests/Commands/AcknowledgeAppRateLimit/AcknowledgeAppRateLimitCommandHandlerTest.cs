namespace Usain.EventListener.Tests.Commands.AcknowledgeAppRateLimit
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventListener.Commands;
    using EventListener.Commands.AcknowledgeAppRateLimit;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class AcknowledgeAppRateLimitCommandHandlerTest
    {
        private readonly Mock<ILogger<AcknowledgeAppRateLimitCommandHandler>>
            _loggerMock =
                new Mock<ILogger<AcknowledgeAppRateLimitCommandHandler>>();

        [Fact]
        public async Task Handle_Abort_Command_When_Cancellation_Requested()
        {
            var cancellationToken = new CancellationToken(true);
            var handler = CreateHandler();
            var actual = await handler.Handle(
                new AcknowledgeAppRateLimitCommand(),
                cancellationToken);

            Assert.Equal(CommandResultType.Aborted, actual.ResultType);
        }

        [Fact]
        public async Task Handle_Returns_Success()
        {
            var cancellationToken = new CancellationToken();
            var handler = CreateHandler();

            var actual = await handler.Handle(
                new AcknowledgeAppRateLimitCommand(),
                cancellationToken);

            Assert.Equal(
                CommandResultType.Success,
                actual.ResultType);
        }

        private AcknowledgeAppRateLimitCommandHandler CreateHandler()
            => new AcknowledgeAppRateLimitCommandHandler(_loggerMock.Object);
    }
}

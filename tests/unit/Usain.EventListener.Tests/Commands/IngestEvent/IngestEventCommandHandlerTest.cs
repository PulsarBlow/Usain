namespace Usain.EventListener.Tests.Commands.IngestEvent
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using EventListener.Commands;
    using EventListener.Commands.IngestEvent;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Slack.Models;
    using Xunit;

    public class IngestEventCommandHandlerTest
    {
        private readonly Mock<ILogger<IngestEventCommandHandler>> _loggerMock =
            new Mock<ILogger<IngestEventCommandHandler>>();
        private readonly Mock<IEventQueue<EventWrapper>> _eventQueueMock =
            new Mock<IEventQueue<EventWrapper>>();

        [Fact]
        public async Task
            Handle_Returns_Aborted_CommandResult_When_Cancellation_Is_Requested()
        {
            var cancellationToken = new CancellationToken(true);
            var handler = CreateHandler();

            var actual = await handler.Handle(
                new IngestEventCommand(new EventWrapper()),
                cancellationToken);

            Assert.Equal(CommandResultType.Aborted, actual.ResultType);
        }

        [Fact]
        public async Task
            Handle_Returns_Successful_Command_When_Event_Is_Enqueued()
        {
            _eventQueueMock.Setup(
                    x => x.EnqueueAsync(
                        It.IsAny<EventWrapper>(),
                        CancellationToken.None))
                .Returns(Task.CompletedTask);

            var handler = CreateHandler();

            var actual = await handler.Handle(
                new IngestEventCommand(new EventWrapper()),
                CancellationToken.None);

            Assert.Equal(
                CommandResultType.Success,
                actual.ResultType);
        }

        private IngestEventCommandHandler CreateHandler()
            => new IngestEventCommandHandler(
                _loggerMock.Object,
                _eventQueueMock.Object);
    }
}

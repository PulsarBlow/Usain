namespace Usain.EventProcessor.Tests.HostedServices
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using EventProcessor.EventReactions;
    using EventProcessor.HostedServices;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Slack.Models.Events;
    using Slack.Models.Events.CallbackEvents;
    using Xunit;

    public class EventQueueProcessorTest
    {
        private readonly Mock<ILogger<EventQueueProcessor>> _loggerMock =
            new Mock<ILogger<EventQueueProcessor>>();
        private readonly Mock<IEventQueue<EventWrapper>> _eventQueueMock =
            new Mock<IEventQueue<EventWrapper>>();
        private readonly Mock<IEventReactionGenerator> _reactionGeneratorMock =
            new Mock<IEventReactionGenerator>();
        private readonly Mock<IEventReaction> _reactionMock =
            new Mock<IEventReaction>();
        private EventWrapper _eventWrapper = new EventWrapper
        {
            Event = new AppMentionEvent(),
        };

        public EventQueueProcessorTest()
        {
            _reactionGeneratorMock
                .Setup(x => x.Generate(_eventWrapper))
                .Returns(_reactionMock.Object);
            _eventQueueMock.Setup(
                    x => x.DequeueAsync(
                        It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(_eventWrapper));
        }

        [Fact]
        public async Task ProcessQueueAsync_Dequeue_Event_And_React()
        {
            var queueProcessor = CreateEventQueueProcessor();
            await queueProcessor.ProcessQueueAsync(CancellationToken.None);

            _reactionMock.Verify(
                x => x.ReactAsync(),
                Times.Once);
        }

        [Fact]
        public async Task ProcessQueueAsync_Dequeue_Event_And_DoesNot_React_When_CallbackEvent_Is_Null()
        {
            _eventWrapper = new EventWrapper(); // EventWrapper's Event property is not set
            _eventQueueMock.Setup(
                    x => x.DequeueAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(_eventWrapper));

            var queueProcessor = CreateEventQueueProcessor();
            await queueProcessor.ProcessQueueAsync(CancellationToken.None);

            _reactionMock.Verify(
                x => x.ReactAsync(),
                Times.Never);
        }

        private EventQueueProcessor CreateEventQueueProcessor()
            => new EventQueueProcessor(
                _loggerMock.Object,
                _eventQueueMock.Object,
                _reactionGeneratorMock.Object);
    }
}

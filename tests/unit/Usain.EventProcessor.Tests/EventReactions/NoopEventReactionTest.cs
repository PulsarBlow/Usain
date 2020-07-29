namespace Usain.EventProcessor.Tests.EventReactions
{
    using System.Threading.Tasks;
    using EventProcessor.EventReactions;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Slack.Models.Events;
    using Slack.Models.Events.CallbackEvents;
    using Xunit;

    public class NoopEventReactionTest
    {
        [Fact]
        public async Task ReactAsync_Complete_Successfully()
        {
            var loggerMock =
                new Mock<ILogger<NoopEventReaction<CallbackEvent>>>();
            loggerMock
                .Setup((x => x.IsEnabled(It.IsAny<LogLevel>())))
                .Returns(true);
            var eventWrapper = new EventWrapper
            {
                Event = new CallbackEvent(),
            };
            var reaction = new NoopEventReaction<CallbackEvent>(
                loggerMock.Object,
                eventWrapper);

            await reaction.ReactAsync();

            loggerMock.VerifyAll();
        }
    }
}

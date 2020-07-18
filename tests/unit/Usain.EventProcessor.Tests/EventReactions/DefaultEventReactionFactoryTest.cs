namespace Usain.EventProcessor.Tests.EventReactions
{
    using EventProcessor.EventReactions;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Slack.Models;
    using Slack.Models.CallbackEvents;
    using Xunit;

    public class DefaultEventReactionFactoryTest
    {
        [Fact]
        public void Create_Creates_A_NoopEventReaction()
        {
            var factory = new DefaultEventReactionFactory<CallbackEvent>(Mock.Of<ILoggerFactory>());
            var eventWrapper = new EventWrapper
            {
                Event = new CallbackEvent(),
            };

            var actual = factory.Create(eventWrapper);

            Assert.Equal(eventWrapper.Event, actual.Event);
        }
    }
}

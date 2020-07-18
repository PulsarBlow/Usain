namespace Usain.EventProcessor.Tests.EventReactions
{
    using System;
    using EventProcessor.EventReactions;
    using Moq;
    using Slack.Models;
    using Slack.Models.CallbackEvents;
    using Xunit;

    public class EventReactionGeneratorTest
    {
        private readonly Mock<IEventReactionFactory<CallbackEvent>>
            _noopEventReactionFactoryMock =
                new Mock<IEventReactionFactory<CallbackEvent>>();
        private readonly Mock<IEventReaction<CallbackEvent>>
            _noopEventReactionMock = new Mock<IEventReaction<CallbackEvent>>();
        private readonly Mock<IEventReactionFactory<AppMentionEvent>>
            _appMentionEventReactionFactoryMock =
                new Mock<IEventReactionFactory<AppMentionEvent>>();
        private readonly Mock<IEventReaction<AppMentionEvent>>
            _appMentionEventReactionMock =
                new Mock<IEventReaction<AppMentionEvent>>();

        public EventReactionGeneratorTest()
        {
            _noopEventReactionFactoryMock
                .Setup(x => x.Create(It.IsAny<EventWrapper>()))
                .Returns(_noopEventReactionMock.Object);
            _appMentionEventReactionFactoryMock
                .Setup(x => x.Create(It.IsAny<EventWrapper>()))
                .Returns(_appMentionEventReactionMock.Object);
        }

        [Fact]
        public void
            Generate_Throws_InvalidOperationException_When_EventWrapper_Is_Null()
        {
            var generator = CreateGenerator();

            Assert.Throws<InvalidOperationException>(
                () => generator.Generate(new EventWrapper()));
        }

        [Fact]
        public void Generate_Returns_EventReaction_For_AppMentionEvent()
        {
            var eventWrapper = new EventWrapper
                { Event = new AppMentionEvent() };
            var generator = CreateGenerator();

            var actual = generator.Generate(eventWrapper);

            Assert.Equal(
                _appMentionEventReactionMock.Object,
                actual);
            _appMentionEventReactionFactoryMock.Verify(
                x => x.Create(eventWrapper),
                Times.Once);
        }

        [Fact]
        public void Generate_Returns_NoopEventReaction_For_Unsupported_Events()
        {
            var eventWrapper = new EventWrapper
                { Event = new CallbackEvent() };
            var generator = CreateGenerator();

            var actual = generator.Generate(eventWrapper);

            Assert.IsAssignableFrom<IEventReaction>(actual);
            Assert.IsAssignableFrom<IEventReaction<CallbackEvent>>(actual);
        }

        private EventReactionGenerator CreateGenerator()
            => new EventReactionGenerator(
                _noopEventReactionFactoryMock.Object,
                _appMentionEventReactionFactoryMock.Object);
    }
}

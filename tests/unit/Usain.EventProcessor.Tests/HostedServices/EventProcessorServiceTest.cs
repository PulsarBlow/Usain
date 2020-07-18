namespace Usain.EventProcessor.Tests.HostedServices
{
    using System.Threading;
    using System.Threading.Tasks;
    using Configuration;
    using EventProcessor.HostedServices;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;

    public class EventProcessorServiceTest
    {
        private readonly Mock<ILogger<EventProcessorService>> _loggerMock =
            new Mock<ILogger<EventProcessorService>>();
        private readonly Mock<IEventQueueProcessor> _eventQueueProcessorMock =
            new Mock<IEventQueueProcessor>();
        private readonly Mock<IOptions<EventProcessorOptions>> _optionsMock =
            new Mock<IOptions<EventProcessorOptions>>();

        public EventProcessorServiceTest()
        {
            _optionsMock.SetupGet(x => x.Value)
                .Returns(
                    new EventProcessorOptions
                    {
                        CheckUpdateTimeMs = 10,
                    });
        }

        [Fact]
        public async Task ExecuteAsync_Calls_EventQueueProcessor()
        {
            var service = new EventProcessorService(
                _loggerMock.Object,
                _eventQueueProcessorMock.Object,
                _optionsMock.Object);

            await service.StartAsync(CancellationToken.None);
            // A little delay to ensure we don't request cancellation
            // before first loop has run once (test stability).
            await Task.Delay(
                250,
                CancellationToken.None);
            await service.StopAsync(CancellationToken.None);

            _eventQueueProcessorMock.Verify(
                x => x.ProcessQueueAsync(It.IsAny<CancellationToken>()),
                Times.AtLeastOnce);
        }
    }
}

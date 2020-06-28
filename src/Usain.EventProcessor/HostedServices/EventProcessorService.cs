namespace Usain.EventProcessor.HostedServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    internal class EventProcessorService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IEventQueueProcessor _queueProcessor;
        private readonly EventProcessorOptions _options;

        public EventProcessorService(
            ILogger<EventProcessorService> logger,
            IEventQueueProcessor queueProcessor,
            IOptions<EventProcessorOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queueProcessor = queueProcessor
                ?? throw new ArgumentNullException(nameof(queueProcessor));
            _options = options?.Value
                ?? throw new ArgumentNullException(nameof(options));
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _logger.LogServiceIsStarting();

            stoppingToken.Register(
                () => _logger.LogServiceIsStopping());

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogServiceIsDoingBackgroundWork();
                    await _queueProcessor.ProcessQueueAsync(stoppingToken);
                    await Task.Delay(
                        _options.CheckUpdateTimeMs,
                        stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogBackgroundWorkHasFailed(ex);
                }
            }
        }
    }
}

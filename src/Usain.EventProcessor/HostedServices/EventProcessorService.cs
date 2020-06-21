namespace Usain.EventProcessor.HostedServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Configuration;
    using Core.Infrastructure;
    using EventReactions;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Slack.Models;

    internal class EventProcessorService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IEventQueue<EventWrapper> _eventQueue;
        private readonly EventProcessorOptions _options;
        private readonly IEventReactionGenerator _eventReactionGenerator;

        public EventProcessorService(
            ILogger<EventProcessorService> logger,
            IEventQueue<EventWrapper> eventQueue,
            IOptions<EventProcessorOptions> options,
            IEventReactionGenerator eventReactionGenerator)
        {
            _logger = logger;
            _eventQueue = eventQueue;
            _eventReactionGenerator = eventReactionGenerator;
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
                    if (await _eventQueue.TryDequeueAsync(out var @event))
                    {
                        _logger.LogServiceHasDequeuedAnEvent(
                            @event?.Type ?? string.Empty);
                        await DoReactAsync(@event);
                    }

                    await Task.Delay(
                        _options.CheckUpdateTimeMs,
                        stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex,"Background work failed");
                }
            }
        }

        private async Task DoReactAsync(
            EventWrapper? eventWrapper)
        {
            if (eventWrapper?.Event == null)
            {
                return;
            }

            var reaction = _eventReactionGenerator.Generate(eventWrapper);
            await reaction.ReactAsync();
        }
    }
}

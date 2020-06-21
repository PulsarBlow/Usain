namespace Usain.EventProcessor.EventReactions
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Slack.Models;

    public class NoopEventReaction : IEventReaction
    {
        private readonly ILogger _logger;
        private readonly EventWrapper _eventWrapper;

        public NoopEventReaction(
            ILogger<NoopEventReaction> logger,
            EventWrapper eventWrapper)
        {
            _logger = logger;
            _eventWrapper = eventWrapper;
        }

        public Task ReactAsync()
        {
            string eventType = _eventWrapper.Type ?? "unknown_type";
            _logger.LogNoReaction(eventType);
            return Task.CompletedTask;
        }
    }
}

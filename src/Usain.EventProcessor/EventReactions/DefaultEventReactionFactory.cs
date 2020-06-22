namespace Usain.EventProcessor.EventReactions
{
    using Microsoft.Extensions.Logging;
    using Slack.Models;

    public class DefaultEventReactionFactory<TCallbackEvent> : IEventReactionFactory<TCallbackEvent>
    {
        private readonly ILogger<NoopEventReaction> _logger;

        public DefaultEventReactionFactory(
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<NoopEventReaction>();
        }

        public IEventReaction<TCallbackEvent> Create(
            EventWrapper eventWrapper)
        {
            throw new System.NotImplementedException();
        }
    }
}

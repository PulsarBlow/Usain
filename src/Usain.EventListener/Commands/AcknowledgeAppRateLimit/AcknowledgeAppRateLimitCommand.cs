namespace Usain.EventListener.Commands.AcknowledgeAppRateLimit
{
    using Slack.Models;

    public class AcknowledgeAppRateLimitCommand
        : Command<CommandResult>
    {
        public AppRateLimitedEvent Event { get; }

        public AcknowledgeAppRateLimitCommand(
            AppRateLimitedEvent @event)
        {
            Event = @event;
        }
    }
}

namespace Usain.EventListener.Commands.IngestEvent
{
    using Slack.Models;

    internal class IngestEventCommand
        : Command<CommandResult>
    {
        public EventWrapper Event { get; }

        public IngestEventCommand(
            EventWrapper @event)
            => Event = @event;
    }
}

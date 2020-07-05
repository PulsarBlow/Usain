namespace Usain.EventListener.Commands.IngestEvent
{
    using Slack.Models;

    internal class IngestEventCommand
        : Command<IngestEventCommandResult>
    {
        public EventWrapper Event { get; }

        public IngestEventCommand(
            EventWrapper eventToStore)
            => Event = eventToStore;
    }
}

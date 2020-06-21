namespace Usain.EventListener.Commands.IngestEvent
{
    using Slack.Models;

    public class IngestEventCommand
        : Command<IngestEventCommandResult>
    {
        public EventWrapper Event { get; }

        public IngestEventCommand(
            EventWrapper eventToStore)
        {
            Event = eventToStore;
        }
    }
}

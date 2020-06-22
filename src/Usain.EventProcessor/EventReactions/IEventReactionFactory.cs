namespace Usain.EventProcessor.EventReactions
{
    using Slack.Models;

    public interface IEventReactionFactory<out TEvent>
    {
        IEventReaction<TEvent> Create(
            EventWrapper eventWrapper);
    }
}

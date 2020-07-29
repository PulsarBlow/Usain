namespace Usain.EventProcessor.EventReactions
{
    using Slack.Models.Events;

    public interface IEventReactionFactory<out TEvent>
        where TEvent : class, new()
    {
        IEventReaction<TEvent> Create(
            EventWrapper eventWrapper);
    }
}

namespace Usain.EventProcessor.EventReactions
{
    using Slack.Models.Events;

    internal interface IEventReactionGenerator
    {
        IEventReaction Generate(
            EventWrapper eventWrapper);
    }
}

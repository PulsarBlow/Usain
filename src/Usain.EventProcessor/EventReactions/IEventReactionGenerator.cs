namespace Usain.EventProcessor.EventReactions
{
    using Slack.Models;

    internal interface IEventReactionGenerator
    {
        IEventReaction Generate(
            EventWrapper eventWrapper);
    }
}

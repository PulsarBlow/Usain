namespace Usain.EventProcessor.EventReactions
{
    using System.Threading.Tasks;

    public interface IEventReaction
    {
        Task ReactAsync();
    }

    public interface IEventReaction<out TEvent> : IEventReaction
    {
        public TEvent Event { get; }
    }
}

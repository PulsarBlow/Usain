namespace Usain.Samples.Simple
{
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Slack.Models.Events;

    public class InMemoryEventQueue : IEventQueue<EventWrapper>
    {
        private readonly ConcurrentQueue<EventWrapper> _queue =
            new ConcurrentQueue<EventWrapper>();

        public Task EnqueueAsync(
            EventWrapper item,
            CancellationToken cancellationToken = default)
        {
            _queue.Enqueue(item);
            return Task.CompletedTask;
        }

        public Task<EventWrapper> DequeueAsync(
            CancellationToken cancellationToken = default)
        {
            _queue.TryDequeue(out var item);
            return Task.FromResult(item);
        }
    }
}

namespace Usain.Samples.Simple
{
    using System.Collections.Concurrent;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Slack.Models;

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

        public Task<bool> TryDequeueAsync(
            [NotNullWhen(true)] out EventWrapper item,
            CancellationToken cancellationToken = default)
            => Task.FromResult(_queue.TryDequeue(out item));
    }
}

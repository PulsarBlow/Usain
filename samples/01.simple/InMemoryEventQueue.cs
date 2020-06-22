namespace Usain.Samples.Simple
{
    using System.Collections.Concurrent;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Usain.Core.Infrastructure;
    using Usain.Slack.Models;

    public class InMemoryEventQueue : IEventQueue<EventWrapper>
    {
        private readonly ConcurrentQueue<EventWrapper> _queue =
            new ConcurrentQueue<EventWrapper>();

        public Task EnqueueAsync(
            EventWrapper item)
        {
            _queue.Enqueue(item);
            return Task.CompletedTask;
        }

        public Task<bool> TryDequeueAsync(
            [NotNullWhen(true)] out EventWrapper item)
        {
            return Task.FromResult(_queue.TryDequeue(out item));
        }
    }
}

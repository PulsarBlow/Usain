namespace Usain.Core.Infrastructure
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a queue abstraction that is used to decouple event ingesting and processing.
    /// </summary>
    /// <typeparam name="TItem">The type of the event</typeparam>
    public interface IEventQueue<TItem>
    {
        /// <summary>
        /// Enqueue an item
        /// </summary>
        /// <param name="item">The item to enqueue</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        Task EnqueueAsync(
            [NotNull] TItem item, CancellationToken cancellationToken);

        /// <summary>
        /// Dequeue an item and delete it from the queue
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The dequeued item</returns>
        Task<TItem> DequeueAsync(CancellationToken cancellationToken);
    }
}

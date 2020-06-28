namespace Usain.Core.Infrastructure
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IEventQueue<TItem>
    {
        Task EnqueueAsync(
            [NotNull] TItem item, CancellationToken cancellationToken);

        Task<bool> TryDequeueAsync(
            [NotNullWhen(true)] out TItem item,
            CancellationToken cancellationToken);
    }
}

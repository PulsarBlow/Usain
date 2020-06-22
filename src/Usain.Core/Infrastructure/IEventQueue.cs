namespace Usain.Core.Infrastructure
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    public interface IEventQueue<TItem>
    {
        Task EnqueueAsync(
            [NotNull] TItem item);

        Task<bool> TryDequeueAsync(
            [NotNullWhen(true)] out TItem item);
    }
}

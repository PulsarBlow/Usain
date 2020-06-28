namespace Usain.EventProcessor.HostedServices
{
    using System.Threading;
    using System.Threading.Tasks;

    internal interface IEventQueueProcessor
    {
        Task ProcessQueueAsync(CancellationToken cancellationToken);
    }
}

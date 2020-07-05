namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.ResultGenerators
{
    using System.Threading;
    using System.Threading.Tasks;
    using Results;
    using Slack.Models;

    internal interface IEventsEndpointResultGenerator<in TEvent>
        where TEvent : Event
    {
        Task<IEndpointResult> GenerateResult(
            TEvent @event,
            CancellationToken cancellationToken);
    }
}

namespace Usain.EventListener.Infrastructure.Hosting.Endpoints
{
    using Results;

    public interface IEventsEndpointResultGenerator
    {
        IEndpointResult GenerateResult();
    }
}

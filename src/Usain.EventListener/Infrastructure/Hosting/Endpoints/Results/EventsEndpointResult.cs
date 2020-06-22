namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.Results
{
    using System.Threading.Tasks;
    using Commands.IngestEvent;
    using Extensions;
    using Microsoft.AspNetCore.Http;

    internal class EventsEndpointResult : IEndpointResult
    {
        private readonly IngestEventCommandResult _result;

        public EventsEndpointResult(IngestEventCommandResult result)
        {
            _result = result;
        }

        public async Task ExecuteAsync(HttpContext context)
        {
            context.Response.SetNoCache();

            if (!_result.IsSuccess)
            {
                context.Response.StatusCode =
                    StatusCodes.Status422UnprocessableEntity;
                return;
            }

            await context.Response.WriteJsonAsync(new
            {
                _result.EventStoreId
            });
        }
    }
}

namespace Usain.EventListener.Infrastructure.Hosting.Middlewares
{
    using System;
    using System.Threading.Tasks;
    using Endpoints;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    internal class EventListenerMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        private readonly IEndpointRouter _endpointRouter;

        public EventListenerMiddleware(
            ILogger<EventListenerMiddleware> logger,
            IEndpointRouter endpointRouter)
        {
            _logger = logger;
            _endpointRouter = endpointRouter;
        }

        public async Task InvokeAsync(
            HttpContext context,
            RequestDelegate next)
        {
            try
            {
                var endpointHandler = _endpointRouter.Find(context);
                if (endpointHandler != null)
                {
                    _logger.LogUsainServerMiddlewareInvokingEndpointHandler(
                        endpointHandler
                            .GetType()
                            .FullName
                        ?? string.Empty,
                        context.Request.Path.ToString());
                    var result = await endpointHandler.ProcessAsync(context, context.RequestAborted);

                    _logger.LogUsainServerMiddlewareInvokingEndpointResult(
                        result.GetType()
                            .FullName
                        ?? string.Empty);
                    await result.ExecuteAsync(context, context.RequestAborted);

                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogUsainServerMiddlewareUnhandledException(ex);
                throw;
            }

            await next(context);
        }
    }
}

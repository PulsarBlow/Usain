namespace Usain.EventListener.Infrastructure.Hosting.Middlewares
{
    using System;
    using System.Threading.Tasks;
    using Extensions;
    using Logging;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Security;

    public class RequestAuthenticationMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        private readonly IRequestAuthenticator _requestAuthenticator;

        public RequestAuthenticationMiddleware(
            ILogger<RequestAuthenticationMiddleware> logger,
            IRequestAuthenticator requestAuthenticator)
        {
            _logger = logger;
            _requestAuthenticator = requestAuthenticator;
        }

        public async Task InvokeAsync(
            HttpContext context,
            RequestDelegate next)
        {
            try
            {
                if (!await _requestAuthenticator.IsAuthenticAsync(
                    context.Request,
                    context.RequestAborted))
                {
                    _logger.LogRequestAuthenticationMiddlewareAuthenticationFailed();
                    context.Response.SetNoCache();
                    context.Response.StatusCode =
                        StatusCodes.Status401Unauthorized;
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogRequestAuthenticationMiddlewareUnhandledException(
                    ex);
                throw;
            }

            await next(context);
        }
    }
}

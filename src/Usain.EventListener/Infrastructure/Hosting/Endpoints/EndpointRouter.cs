namespace Usain.EventListener.Infrastructure.Hosting.Endpoints
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    internal class EndpointRouter : IEndpointRouter
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<Endpoint> _endpoints;

        public EndpointRouter(
            ILogger<EndpointRouter> logger,
            IEnumerable<Endpoint> endpoints)
        {
            _logger = logger;
            _endpoints = endpoints;
        }

        public IEndpointHandler? Find(
            HttpContext context)
        {
            foreach (var endpoint in _endpoints)
            {
                var path = endpoint.Path;
                if (context.Request.Path.Equals(
                    path,
                    StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogEndpointFound(
                        context.Request.Path,
                        endpoint.Name);

                    return GetEndpointHandler(
                        endpoint,
                        context);
                }
            }

            _logger.LogEndpointNotFound(context.Request.Path);

            return null;
        }

        private IEndpointHandler? GetEndpointHandler(
            Endpoint endpoint,
            HttpContext context)
        {
            if (context.RequestServices.GetService(endpoint.Handler) is
                IEndpointHandler handler)
            {
                _logger.LogEndpointHandlerFound(
                    endpoint.Name,
                    endpoint.Handler.FullName ?? string.Empty);
                return handler;
            }

            _logger.LogEndpointHandlerNotFound(
                endpoint.Name,
                endpoint.Handler.FullName ?? string.Empty);

            return null;
        }
    }
}

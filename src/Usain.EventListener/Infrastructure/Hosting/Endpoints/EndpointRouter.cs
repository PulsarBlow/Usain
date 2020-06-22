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

        public IEndpointHandler? Find(HttpContext context)
        {
            foreach (var endpoint in _endpoints)
            {
                var path = endpoint.Path;
                if (context.Request.Path.Equals(path,
                    StringComparison.OrdinalIgnoreCase))
                {
                    var endpointName = endpoint.Name;
                    _logger.LogDebug(
                        "Request path {path} matched to endpoint type {endpoint}",
                        context.Request.Path, endpointName);

                    return GetEndpointHandler(endpoint, context);
                }
            }

            _logger.LogTrace("No endpoint entry found for request path: {path}",
                context.Request.Path);

            return null;
        }

        private IEndpointHandler? GetEndpointHandler(
            Endpoint endpoint,
            HttpContext context)
        {
            if (context.RequestServices.GetService(endpoint.Handler) is
                IEndpointHandler handler)
            {
                _logger.LogDebug(
                    "Endpoint enabled: {endpoint}, successfully created handler: {endpointHandler}",
                    endpoint.Name, endpoint.Handler.FullName);
                return handler;
            }

            _logger.LogDebug(
                "Endpoint enabled: {endpoint}, failed to create handler: {endpointHandler}",
                endpoint.Name, endpoint.Handler.FullName);
            return null;
        }
    }
}

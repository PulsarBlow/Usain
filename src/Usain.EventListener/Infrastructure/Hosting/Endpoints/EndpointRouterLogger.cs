namespace Usain.EventListener.Infrastructure.Hosting.Endpoints
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class EndpointRouterLogger
    {
        private static readonly Action<ILogger, string, string, Exception?> EndpointFound
            = LoggerMessage.Define<string, string>(
                LogLevel.Debug,
                new EventId(
                    0,
                    nameof(EndpointFound)),
                "Request path {RequestPath} matched to endpoint type {EndpointName}");

        private static readonly Action<ILogger, string, Exception?> EndpointNotFound
            = LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(EndpointNotFound)),
                "No endpoint entry found for request path: {RequestPath}");

        private static readonly Action<ILogger, string, string, Exception?>
            EndpointHandlerFound = LoggerMessage.Define<string, string>(
                LogLevel.Debug,
                new EventId(
                    0,
                    nameof(EndpointHandlerFound)),
                "Endpoint enabled: {EndpointName} => successfully created handler: {EndpointHandlerName}");

        private static readonly Action<ILogger, string, string, Exception?>
            EndpointHandlerNotFound = LoggerMessage.Define<string, string>(
                LogLevel.Warning,
                new EventId(
                    0,
                    nameof(EndpointHandlerNotFound)),
                "Endpoint enabled: {EndpointName} => failed to create handler: {EndpointHandlerName}.\nIs it registered and implement IEndpointHandler ?");

        public static void LogEndpointFound(
            this ILogger logger,
            string requestPath,
            string endpointName)
        {
            EndpointFound(
                logger,
                requestPath,
                endpointName,
                null);
        }

        public static void LogEndpointNotFound(
            this ILogger logger,
            string requestPath)
        {
            EndpointNotFound(
                logger,
                requestPath,
                null);
        }

        public static void LogEndpointHandlerFound(
            this ILogger logger,
            string endpointName,
            string endpointHandlerName)
        {
            EndpointHandlerFound(
                logger,
                endpointName,
                endpointHandlerName,
                null);
        }

        public static void LogEndpointHandlerNotFound(
            this ILogger logger,
            string endpointName,
            string endpointHandlerName)
        {
            EndpointHandlerNotFound(
                logger,
                endpointName,
                endpointHandlerName,
                null);
        }
    }
}

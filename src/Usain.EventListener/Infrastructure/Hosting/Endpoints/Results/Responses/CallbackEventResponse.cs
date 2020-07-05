namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.Results.Responses
{
    using System;

    public class CallbackEventResponse
    {
        public Guid EventStoreId { get; }

        public CallbackEventResponse(Guid eventStoreId)
            => EventStoreId = eventStoreId;
    }
}

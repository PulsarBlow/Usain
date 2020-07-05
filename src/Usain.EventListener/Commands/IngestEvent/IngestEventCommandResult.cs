namespace Usain.EventListener.Commands.IngestEvent
{
    using System;

    public class IngestEventCommandResult : CommandResult
    {
        public Guid EventStoreId { get; }

        public IngestEventCommandResult(
            Guid eventStoreId,
            CommandResultType resultType = CommandResultType.Success)
            : base(resultType)
            => EventStoreId = eventStoreId;
    }
}

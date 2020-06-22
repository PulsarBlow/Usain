namespace Usain.EventListener.Commands
{
    using System;

    public interface ICommandResult
    {
        Guid CommandId { get; }
        CommandResultType ResultType { get; }
    }
}

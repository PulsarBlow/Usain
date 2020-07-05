namespace Usain.EventListener.Commands
{
    using System;

    internal interface ICommandResult
    {
        Guid CommandId { get; }
    }
}

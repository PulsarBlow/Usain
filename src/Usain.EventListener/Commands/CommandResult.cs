namespace Usain.EventListener.Commands
{
    using System;

    public class CommandResult : ICommandResult
    {
        public Guid CommandId { get; }
        public CommandResultType ResultType { get; }

        public bool IsSuccess
            => ResultType == CommandResultType.Success;

        public CommandResult(
            Guid commandId,
            CommandResultType resultType = CommandResultType.Success)
        {
            CommandId = commandId;
            ResultType = resultType;
        }

        public override string ToString()
            => $"{GetType().Name}:{CommandId}:{ResultType}";
    }
}

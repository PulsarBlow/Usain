namespace Usain.EventListener.Commands
{
    using System;

    public class CommandResult : ICommandResult
    {
        public Guid CommandId { get; }
        public CommandResultType ResultType { get; }

        public bool IsSuccess
            => ResultType == CommandResultType.Success;

        public CommandResult(CommandResultType resultType = CommandResultType.Success)
        {
            CommandId = Guid.NewGuid();
            ResultType = resultType;
        }
    }
}

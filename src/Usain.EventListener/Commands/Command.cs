namespace Usain.EventListener.Commands
{
    using System;

    public class Command<TCommandResult> : ICommand<TCommandResult>
        where TCommandResult: ICommandResult
    {
        public Guid Id { get; } = Guid.NewGuid();

        public override string ToString()
        {
            return $"{GetType().Name}:{Id}";
        }
    }
}

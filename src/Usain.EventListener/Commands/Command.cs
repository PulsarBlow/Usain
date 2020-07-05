namespace Usain.EventListener.Commands
{
    using System;

    public class Command<TCommandResult> : ICommand<TCommandResult>
        where TCommandResult : ICommandResult
    {
        public Guid Id { get; }

        protected Command()
            : this(Guid.NewGuid())
        {
        }

        protected Command(
            Guid commandId)
        {
            Id = commandId;
        }

        public override string ToString() { return $"{GetType().Name}:{Id}"; }
    }
}

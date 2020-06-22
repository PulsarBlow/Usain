namespace Usain.EventListener.Commands
{
    using MediatR;

    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }

    public interface ICommandHandler<in TCommand, TResult>
        : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
        where TResult: ICommandResult
    {
    }
}

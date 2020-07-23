namespace Usain.EventListener.Commands.VerifyUrl
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    internal class VerifyUrlCommandHandler
        : ICommandHandler<VerifyUrlCommand, VerifyUrlCommandResult>
    {
        private readonly ILogger<VerifyUrlCommandHandler> _logger;

        public VerifyUrlCommandHandler(
            ILogger<VerifyUrlCommandHandler> logger)
            => _logger = logger;

        public Task<VerifyUrlCommandResult> Handle(
            VerifyUrlCommand command,
            CancellationToken cancellationToken)
        {
            _logger.LogCommandHandling(command.ToString());

            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogCommandCancelling(command.ToString());
                return Task.FromResult(
                    new VerifyUrlCommandResult(
                        command.Challenge,
                        command.Id,
                        CommandResultType.Aborted));
            }

            var commandResult = string.IsNullOrEmpty(command.Challenge)
                ? new VerifyUrlCommandResult(
                    string.Empty,
                    command.Id,
                    CommandResultType.Failure)
                : new VerifyUrlCommandResult(command.Challenge, command.Id);

            _logger.LogCommandHandled(command.ToString());

            return Task.FromResult(commandResult);
        }
    }
}

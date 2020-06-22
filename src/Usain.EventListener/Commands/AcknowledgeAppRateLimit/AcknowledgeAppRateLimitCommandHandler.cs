namespace Usain.EventListener.Commands.AcknowledgeAppRateLimit
{
    using System.Threading;
    using System.Threading.Tasks;
    using Infrastructure.Logging;
    using Microsoft.Extensions.Logging;

    public class AcknowledgeAppRateLimitCommandHandler
        : ICommandHandler<AcknowledgeAppRateLimitCommand,
            CommandResult>
    {
        private readonly ILogger _logger;

        public AcknowledgeAppRateLimitCommandHandler(
            ILogger<AcknowledgeAppRateLimitCommandHandler> logger)
        {
            _logger = logger;
        }

        public Task<CommandResult> Handle(
            AcknowledgeAppRateLimitCommand command,
            CancellationToken cancellationToken)
        {
            _logger.LogCommandHandling(command.ToString());
            if (cancellationToken
                .IsCancellationRequested)
            {
                _logger.LogCommandCancelling(command.ToString());
                return
                    Task.FromResult(
                        new CommandResult(CommandResultType.Aborted));
            }

            _logger.LogCommandHandled(command.ToString());

            return Task.FromResult(new CommandResult());
        }
    }
}

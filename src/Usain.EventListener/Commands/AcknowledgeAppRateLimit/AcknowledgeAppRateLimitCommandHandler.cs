namespace Usain.EventListener.Commands.AcknowledgeAppRateLimit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    internal class AcknowledgeAppRateLimitCommandHandler
        : ICommandHandler<AcknowledgeAppRateLimitCommand,
            CommandResult>
    {
        private readonly ILogger _logger;

        public AcknowledgeAppRateLimitCommandHandler(
            ILogger<AcknowledgeAppRateLimitCommandHandler> logger)
            => _logger =
                logger ?? throw new ArgumentNullException(nameof(logger));

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
                        new CommandResult(
                            command.Id,
                            CommandResultType.Aborted));
            }

            _logger.LogCommandHandled(command.ToString());

            return Task.FromResult(new CommandResult(command.Id));
        }
    }
}

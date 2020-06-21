namespace Usain.EventListener.Commands.VerifyUrl
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class VerifyUrlCommandHandler
        : ICommandHandler<VerifyUrlCommand, VerifyUrlCommandResult>
    {
        private readonly ILogger<VerifyUrlCommandHandler> _logger;

        public VerifyUrlCommandHandler(
            ILogger<VerifyUrlCommandHandler> logger)
        {
            _logger = logger;
        }

        public Task<VerifyUrlCommandResult> Handle(
            VerifyUrlCommand command,
            CancellationToken cancellationToken)
        {
            _logger.LogDebug(
                "Handling Slack UrlVerification command:\nCommand={0}",
                command);
            return Task.FromResult(string.IsNullOrEmpty(command.Challenge)
                ? new VerifyUrlCommandResult(
                    string.Empty,
                    CommandResultType.Failure)
                : new VerifyUrlCommandResult(command.Challenge));
        }
    }
}

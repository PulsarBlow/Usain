namespace Usain.EventListener.Commands.VerifyUrl
{
    public class VerifyUrlCommandResult
        : CommandResult
    {
        public string Challenge { get; }

        public VerifyUrlCommandResult(
            string challenge,
            CommandResultType commandResultType = CommandResultType.Success)
            : base(commandResultType)
            => Challenge = challenge;
    }
}

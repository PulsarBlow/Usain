namespace Usain.EventListener.Commands.VerifyUrl
{
    internal class VerifyUrlCommand
        : Command<VerifyUrlCommandResult>
    {
        public string Challenge { get; }

        public VerifyUrlCommand(
            string challenge)
            => Challenge = challenge;
    }
}

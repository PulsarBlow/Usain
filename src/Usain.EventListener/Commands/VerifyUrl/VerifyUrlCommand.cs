namespace Usain.EventListener.Commands.VerifyUrl
{
    public class VerifyUrlCommand
        : Command<VerifyUrlCommandResult>
    {
        public string Challenge { get; }

        public VerifyUrlCommand(
            string challenge)
        {
            Challenge = challenge;
        }
    }
}

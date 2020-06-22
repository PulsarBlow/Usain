namespace Usain.EventListener.Commands.VerifyUrl
{
    public class VerifyUrlCommand
        : Command<VerifyUrlCommandResult>
    {
        public string? Challenge { get; set; }
    }
}

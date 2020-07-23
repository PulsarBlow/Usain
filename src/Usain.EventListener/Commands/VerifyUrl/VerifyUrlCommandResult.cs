namespace Usain.EventListener.Commands.VerifyUrl
{
    using System;

    public class VerifyUrlCommandResult
        : CommandResult
    {
        public string Challenge { get; }

        public VerifyUrlCommandResult(
            string challenge,
            Guid commandId,
            CommandResultType commandResultType = CommandResultType.Success)
            : base(
                commandId,
                commandResultType)
            => Challenge = challenge;
    }
}

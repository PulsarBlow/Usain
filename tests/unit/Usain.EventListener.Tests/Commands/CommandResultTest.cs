namespace Usain.EventListener.Tests.Commands
{
    using System;
    using EventListener.Commands;
    using Xunit;

    public class CommandResultTest
    {
        [Fact]
        public void Constructor_Sets_CommandId_And_ResultType()
        {
            var commandId = Guid.NewGuid();
            var commandResultType = CommandResultType.Failure;

            var result = new CommandResult(
                commandId,
                commandResultType);

            Assert.Equal(
                commandId,
                result.CommandId);
            Assert.Equal(
                commandResultType,
                result.ResultType);
        }

        [Fact]
        public void ToString_Returns_Expected_Value()
        {
            var commandId = Guid.NewGuid();
            var commandResultType = CommandResultType.Aborted;

            var commandResult = new CommandResult(
                commandId,
                commandResultType);

            Assert.Equal(
                $"{nameof(CommandResult)}:{commandId}:{commandResultType}",
                commandResult.ToString());
        }
    }
}

namespace Usain.EventListener.Tests.Commands
{
    using System;
    using System.ComponentModel.Design;
    using EventListener.Commands;
    using Xunit;

    public class CommandTest
    {
        [Fact]
        public void ToString_Returns_Expected_Value()
        {
            var commandId = Guid.NewGuid();
            var command = new CustomCommandTest(commandId);

            Assert.Equal(
                $"{nameof(CustomCommandTest)}:{commandId}",
                command.ToString());
        }

        private class CustomCommandTest : Command<CommandResultTest>
        {
            public CustomCommandTest(
                Guid commandId)
                : base(commandId)
            {
            }
        }

        private class CommandResultTest : ICommandResult
        {
            public Guid CommandId { get; }

            public CommandResultTest(
                Guid commandId)
                => CommandId = commandId;
        }
    }
}

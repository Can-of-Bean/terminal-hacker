using System;

namespace Commands
{
    public class ListCommand : CommandBase
    {
        public override void Execute()
        {
            throw new NotImplementedException(); // This is waiting for the implementation of the terminal text input
        }

        public override string Name { get; }
        public override string Description { get; }

        public ListCommand()
        {
            Name = "ls";
            Description = "List all items in the current directory.";
        }
    }
}
using System;

namespace Commands
{
    public class ListCommand : ICommand
    {
        public void Execute()
        {
            throw new NotImplementedException(); // This is waiting for the implementation of the terminal text input
        }

        public string Name { get; } = "ls";
        public string Description { get; } = "List all items in the current directory.";
    }
}
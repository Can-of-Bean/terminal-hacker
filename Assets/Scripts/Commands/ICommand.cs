using System.Collections.Generic;

namespace Commands
{
    public interface ICommand
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        void Execute(string[] args, Dictionary<string, string> flags);

        /// <summary>
        /// The Name property represents the name of the command.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The Description property provides a description of the command.
        /// </summary>
        string Description { get; }
    }
}
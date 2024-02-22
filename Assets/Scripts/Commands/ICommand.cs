namespace Commands
{
    public interface ICommand
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        void Execute();

        /// <summary>
        /// Execute the command with arguments
        /// </summary>
        void Execute(string[] args);

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
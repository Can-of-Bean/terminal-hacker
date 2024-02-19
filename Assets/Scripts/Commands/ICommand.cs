namespace Commands
{
    public interface ICommand
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        void Execute();

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
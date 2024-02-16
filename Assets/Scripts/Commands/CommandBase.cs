using UnityEngine;

namespace Commands
{
    /// <summary>
    /// The CommandBase abstract class serves as the base for all command classes in the game.
    /// It implements the ICommand interface and extends the MonoBehaviour class.
    /// </summary>
    public abstract class CommandBase : MonoBehaviour, ICommand
    {
        /// <summary>
        /// The Execute method is responsible for executing the command's action.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// The Name property represents the name of the command.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The Description property provides a description of the command.
        /// </summary>
        public abstract string Description { get; }
    }
}
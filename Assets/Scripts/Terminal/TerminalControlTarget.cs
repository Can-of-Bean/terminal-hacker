namespace Terminal
{
    /// <summary>
    /// A base class implemented by all normal receivers of terminal input.
    /// </summary>
    public abstract class TerminalControlTarget
    {
        /// <summary>
        /// Handles text coming in from the user.
        /// </summary>
        /// <param name="message"></param>
        public abstract void HandleUserInput(string message);
    }
}
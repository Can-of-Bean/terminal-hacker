namespace Terminal
{
    /// <summary>
    /// A base class implemented by all normal receivers of terminal input.
    /// </summary>
    public interface ITerminalControlTarget
    {
        /// <summary>
        /// Handles text coming in from the user.
        /// </summary>
        /// <param name="message"></param>
        public void HandleUserInput(string message);
    }
}
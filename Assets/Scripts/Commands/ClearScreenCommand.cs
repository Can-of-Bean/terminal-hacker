using Terminal;

namespace Commands
{
    public class ClearScreenCommand : ICommand
    {
        public void Execute(string[] args)
        {
            TerminalControl.Instance.ClearConsoleText();
        }

        public string Name { get; } = "cls";
        public string Description { get; } = "Clears the screen of text";
    }
}
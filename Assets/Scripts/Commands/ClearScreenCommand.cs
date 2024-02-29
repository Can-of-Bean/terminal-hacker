using System.Collections.Generic;
using Terminal;

namespace Commands
{
    public class ClearScreenCommand : ICommand
    {

        public void Execute(string[] args, Dictionary<string, string> flags)
        {
            TerminalControl.Instance.ClearConsoleText();
        }

        public string Name { get; } = "cls";
        public string Description { get; } = "Clears the screen of text";
    }
}
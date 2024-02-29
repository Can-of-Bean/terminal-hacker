using Files;
using Terminal;

namespace Commands
{
    public class ExitSshCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (TerminalControl.Instance.CurrentFileSystem is RemoteFileSystem)
            {
                TerminalControl.Instance.CurrentFileSystem = LocalFileSystem.Instance;
                TerminalControl.Instance.InputHeader = "user@home";
                TerminalControl.Instance.WriteLineToConsole("exited");
            }
            else
            {
                TerminalControl.Instance.WriteLineToConsole("cannot exit session: no ssh session is currently active.");
            }
        }

        public string Name { get; } = "exit";
        public string Description { get; } = "exit out of an ssh session";
    }
}
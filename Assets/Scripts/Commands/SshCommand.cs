using Files;
using Terminal;

namespace Commands
{
    public class SshCommand : ICommand
    {
        private readonly string m_host = "219.22.98.32";
        private readonly string m_username = "root";

        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                TerminalControl.Instance.WriteLineToConsole("Usage: ssh <username>@<host>");
                return;
            }

            if (args[0] == $"{m_username}@{m_host}")
            {
                TerminalControl.Instance.WriteLineToConsole("Connected to remote server");
                TerminalControl.Instance.CurrentFileSystem = RemoteFileSystem.Instance;
                TerminalControl.Instance.InputHeader = $"{m_username}@{m_host}";
            }
            else
            {
                TerminalControl.Instance.WriteLineToConsole("Unable to connect to remote server");
            }
        }


        public string Name { get; } = "ssh";
        public string Description { get; } = "Connect to a remote server";
    }
}
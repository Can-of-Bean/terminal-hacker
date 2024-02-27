using System;
using System.Collections.Generic;
using Exceptions;
using Terminal;

namespace Commands
{
    public class CommandControl : SingletonBehaviour<CommandControl>
    {
        private Dictionary<string, ICommand> m_commands = null!;

        public void Start()
        {
            TerminalControl.Instance.RawInputSubmitted += OnRawInputSubmitted;

            // Initialize the commands dictionary
            m_commands = new Dictionary<string, ICommand>
            {
                { "ls", new ListCommand() },
                { "cat", new CatCommand() },
                { "ssh", new SshCommand()},
                { "cd", new ChangeDirectoryCommand() },
                { "mkdir", new MakeDirectoryCommand() },
                { "exit", new ExitSshCommand() },
                // { "touch", new TouchCommand() },
                // { "rm", new RemoveCommand() },
                // { "clear", new ClearCommand() },
                // { "pwd", new PrintWorkingDirectoryCommand() },
                // { "echo", new EchoCommand() },
                // { "help", new HelpCommand() }
            };
        }

        private void OnRawInputSubmitted(object sender, TerminalInputEventArgs e)
        {
            string cmd = e.Input.Split(' ')[0];
            string[] args = e.Input.Split(' ')[1..] ?? Array.Empty<string>();

            if (m_commands.TryGetValue(cmd, out ICommand command))
            {
                try
                {
                    command.Execute(args);
                }
                catch (CommandException ex)
                {
                    TerminalControl.Instance.WriteLineToConsole($"Error: {ex.Message}");
                }
                catch (FileSystemException ex)
                {
                    TerminalControl.Instance.WriteLineToConsole($"Error: {ex.Message}");
                }
            }
            else
            {
                TerminalControl.Instance.WriteLineToConsole($"Unknown command: {cmd}");
            }
        }
    }
}
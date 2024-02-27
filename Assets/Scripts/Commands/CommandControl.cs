using System;
using System.Collections.Generic;
using Commands.Decryption;
using Exceptions;
using Terminal;

namespace Commands
{
    public class CommandControl : Singleton<CommandControl>, ITerminalControlTarget
    {
        private readonly Dictionary<string, ICommand> m_commands;

        public CommandControl()
        {
            // Initialize the commands dictionary
            m_commands = new Dictionary<string, ICommand>
            {
                { "ls", new ListCommand() },
                { "cat", new CatCommand() },
                { "ssh", new SshCommand()},
                { "cd", new ChangeDirectoryCommand() },
                { "mkdir", new MakeDirectoryCommand() },
                { "exit", new ExitSshCommand() },
                { "decrypt", new DecryptionCommand() },
                // { "touch", new TouchCommand() },
                // { "rm", new RemoveCommand() },
                // { "clear", new ClearCommand() },
                // { "pwd", new PrintWorkingDirectoryCommand() },
                // { "echo", new EchoCommand() },
                // { "help", new HelpCommand() }
            };
        }

        public void HandleUserInput(string message)
        {
            string cmd = message.Split(' ')[0];
            string[] args = message.Split(' ')[1..] ?? Array.Empty<string>();

            // notify the user where the command came from
            TerminalControl.Instance.WriteLineToConsole(TerminalControl.Instance.InputHeader);

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
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
                { "cls", new ClearScreenCommand() },
                { "rm", new RemoveCommand() },
            };
        }

        private void GetFlags(string[] args, Dictionary<string, string> flags)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith('-'))
                {
                    string flag = args[i].Remove(0, 1);
                    string value = args[i + 1];
                    flags.Add(flag, value);
                    args[i] = string.Empty;
                    args[i + 1] = string.Empty;
                    i++;
                }
                if (args[i].StartsWith("--"))
                {
                    string flag = args[i].Remove(0, 2);
                    string value = args[i + 1];
                    flags.Add(flag, value);
                    args[i] = string.Empty;
                    args[i + 1] = string.Empty;
                    i++;
                }
            }
        }

        public void HandleUserInput(string message)
        {
            string cmd = message.Split(' ')[0];
            string[] args = message.Split(' ')[1..] ?? Array.Empty<string>();
            Dictionary<string, string> flags = new();

            // notify the user where the command came from and what command was used
            TerminalControl.Instance.WriteLineToConsole(TerminalControl.Instance.InputHeader + " " + message);

            if (m_commands.TryGetValue(cmd, out ICommand command))
            {
                GetFlags(args, flags);
                try
                {
                    command.Execute(args, flags);
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
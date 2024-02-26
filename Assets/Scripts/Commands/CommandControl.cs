using System;
using System.Collections.Generic;
using Exceptions;
using Terminal;

namespace Commands
{
    public class CommandControl : SingletonBehaviour<CommandControl>
    {
        private Dictionary<string, ICommand> commands;

        public void Start()
        {
            TerminalControl.Instance.RawInputSubmitted += OnRawInputSubmitted;

            // Initialize the commands dictionary
            commands = new Dictionary<string, ICommand>
            {
                { "ls", new ListCommand() },
                { "cat", new CatCommand() },
                { "ssh", new SshCommand()},
                { "cd", new ChangeDirectoryCommand() },
                { "mkdir", new MakeDirectoryCommand() }
                // { "touch", new TouchCommand() },
                // { "rm", new RemoveCommand() },
                // { "clear", new ClearCommand() },
                // { "pwd", new PrintWorkingDirectoryCommand() },
                // { "echo", new EchoCommand() },
                // { "help", new HelpCommand() }
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

        private void OnRawInputSubmitted(object sender, TerminalInputEventArgs e)
        {
            string cmd = e.Input.Split(' ')[0];
            string[] args = e.Input.Split(' ')[1..] ?? Array.Empty<string>();
            Dictionary<string, string> flags = new();

            if (commands.TryGetValue(cmd, out ICommand command))
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
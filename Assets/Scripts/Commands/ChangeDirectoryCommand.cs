using System.Collections.Generic;
using Exceptions;
using Files;
using Terminal;

namespace Commands
{
    public class ChangeDirectoryCommand : ICommand
    {
        public void Execute(string[] args, Dictionary<string, string> flags)
        {
            if (args.Length == 0)
            {
                throw new CommandException("You need to input a file path.");
            }

            IFileSystemItem fileSystemItem;
            string path = args[0];

            if (path == "/")
            {
                fileSystemItem = TerminalControl.Instance.CurrentFileSystem.Root;
            }
            else if (path.StartsWith('/'))
            {
                path = path.Remove(0, 1);
                fileSystemItem = TerminalControl.Instance.CurrentFileSystem.Root.GetItem(path);
            }
            else
            {
                fileSystemItem = TerminalControl.Instance.CurrentFileSystem.CurrentDirectory.GetItem(path);
            }

            if (fileSystemItem is Directory directory)
            {
                TerminalControl.Instance.CurrentFileSystem.ChangeDirectory(path);
            }
            else
            {
                throw new CommandException($"{args[0]} is not a directory.");
            }
        }

        public string Name { get; } = "cd";
        public string Description { get; } = "Change the current directory";
    }
}
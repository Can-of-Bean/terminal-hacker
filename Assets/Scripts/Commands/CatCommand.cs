using System.Collections.Generic;
using Exceptions;
using Files;
using Terminal;

namespace Commands
{
    public class CatCommand : ICommand
    {
        public void Execute(string[] args, Dictionary<string, string> flags)
        {
            if (args.Length == 0)
            {
                throw new CommandException("You need to input a file path.");
            }

            IFileSystemItem fileSystemItem;
            string path = args[0];
            if (path.StartsWith('/'))
            {
                path = path.Remove(0, 1);
                fileSystemItem = TerminalControl.Instance.CurrentFileSystem.Root.GetItem(path);
            }
            else
            {
                fileSystemItem = TerminalControl.Instance.CurrentFileSystem.CurrentDirectory.GetItem(path);
            }

            if (fileSystemItem is File file)
            {
                TerminalControl.Instance.WriteToConsole(file.Content);
            }
            else
            {
                throw new CommandException("Cannot read a directory.");
            }
        }

        public string Name { get; }
        public string Description { get; }
    }
}
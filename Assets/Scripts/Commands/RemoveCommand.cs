using System;
using System.Collections.Generic;
using Exceptions;
using Files;
using Terminal;

namespace Commands
{
    public class RemoveCommand : ICommand
    {
        public void Execute(string[] args, Dictionary<string, string> flags)
        {
            if (args.Length == 0)
            {
                throw new CommandException("You need to input a file path.");
            }

            IFileSystemItem fileSystemItem;
            string path = args[0];
            string fileName;
            if (path.Contains('/'))
            {
                string pathToParent = path.Substring(0, path.LastIndexOf('/'));
                fileName = path.Substring(path.LastIndexOf('/') + 1);
                if (pathToParent.StartsWith('/'))
                {
                    pathToParent = pathToParent.Remove(0, 1);
                    fileSystemItem = TerminalControl.Instance.CurrentFileSystem.Root.GetItem(pathToParent);
                }
                else
                {
                    fileSystemItem = TerminalControl.Instance.CurrentFileSystem.CurrentDirectory.GetItem(pathToParent);
                }
            }
            else
            {
                fileSystemItem = TerminalControl.Instance.CurrentFileSystem.CurrentDirectory;
                fileName = path;
            }

            if (fileSystemItem == null)
            {
                throw new CommandException("The directory does not exist.");
            }

            Directory parent = fileSystemItem as Directory ?? throw new InvalidOperationException();
            File? file = parent.FindFile(fileName);

            if (file == null)
            {
                throw new CommandException("The file does not exist.");
            }

            parent.RemoveItem(file);
        }

        public string Name { get; } = "rm";
        public string Description { get; } = "Removes a file from the file system.";
    }
}
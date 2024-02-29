using System;
using System.Collections.Generic;
using Files;
using Terminal;
using UnityEngine;

namespace Commands
{
    public class ListCommand : ICommand
    {
        public void Execute(string[] args, Dictionary<string, string> flags)
        {
            if (args.Length == 0)
            {
                OutputDirectoryContents(TerminalControl.Instance.CurrentFileSystem.CurrentDirectory);
            }
            else
            {
                string path = args[0];

                if (path.StartsWith('/'))
                {
                    path = path.Remove(0, 1);
                    IFileSystemItem item = TerminalControl.Instance.CurrentFileSystem.Root.GetItem(path);

                    if (item is Directory directory)
                    {
                        OutputDirectoryContents(directory);
                    }
                    else
                    {
                        TerminalControl.Instance.WriteLineToConsole(item.Name);
                    }
                }
                else
                {
                    IFileSystemItem item = TerminalControl.Instance.CurrentFileSystem.CurrentDirectory.GetItem(path);
                    if (item is Directory directory)
                    {
                        OutputDirectoryContents(directory);
                    }
                    else
                    {
                        TerminalControl.Instance.WriteLineToConsole(item.Name);
                    }
                }

            }

        }

        /// <summary>
        /// Outputs the contents of a directory to the terminal.
        /// </summary>
        /// <param name="directory">Directory to output contents of</param>
        private static void OutputDirectoryContents(Directory directory)
        {
            foreach (IFileSystemItem item in directory.Items)
            {
                if (item is File)
                {
                    TerminalControl.Instance.WriteToConsole(item.Name + "\t");
                }
                else if (item is Directory)
                {
                    TerminalControl.Instance.WriteToConsole(item.Name + "/\t");
                }
            }
            TerminalControl.Instance.WriteToConsole("\n");
        }

        public string Name { get; } = "ls";
        public string Description { get; } = "List all items in the current directory.";
    }
}
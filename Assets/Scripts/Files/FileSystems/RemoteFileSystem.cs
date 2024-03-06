using System;
using Exceptions;
using Terminal;

namespace Files
{
    public class RemoteFileSystem : Singleton<RemoteFileSystem>, IFileSystem
    {
        public string Name { get; }
        public Directory Root { get; }
        public Directory CurrentDirectory { get; private set; }

        public RemoteFileSystem()
        {
            Name = "Remote File System";
            Root = new Directory("Root");
            CurrentDirectory = Root;
        }

        public void ChangeDirectory(string path)
        {
            IFileSystemItem fileSystemItem;

            if (path == "/")
            {
                CurrentDirectory = Root;
                ChangeHeader();
                return;
            }
            if (path.StartsWith('/'))
            {
                path = path.Remove(0, 1);
                fileSystemItem = Root.GetItem(path);
            }
            else
            {
                fileSystemItem = CurrentDirectory.GetItem(path);
            }

            if (fileSystemItem is Directory item)
            {
                CurrentDirectory = item;
                ChangeHeader();
            }
            else
            {
                throw new FileSystemException($"{path} is not a directory.");
            }
        }

        private void ChangeHeader()
        {
            String inputHeader = TerminalControl.Instance.InputHeader;
            inputHeader = inputHeader.Split(' ')[0];
            TerminalControl.Instance.InputHeader = $"{inputHeader} {CurrentDirectory.GetPath()} >";
        }
    }
}
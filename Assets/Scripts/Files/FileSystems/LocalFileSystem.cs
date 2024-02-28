using System;
using Exceptions;
using Terminal;

namespace Files
{
    public class LocalFileSystem : Singleton<LocalFileSystem>, IFileSystem
    {
        public string Name { get; }
        public Directory Root { get; }
        public Directory CurrentDirectory { get; private set; }

        public LocalFileSystem()
        {
            Name = "Local File System";
            Root = new Directory("Root");
            SetupRoot();
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

        public void SetupRoot()
        {
           string[] directories = { "var", "lib", "usr", "bin", "etc", "home" };

            foreach (string dir in directories)
            {
                Directory directory = Root.AddItem(new Directory(dir, Root));
                switch (dir)
                {
                    case "var":
                        // SetupVar(directory);
                        break;
                    case "lib":
                        // SetupLib(directory);
                        break;
                    case "usr":
                        // SetupUsr(directory);
                        break;
                    case "bin":
                        // SetupBin(directory);
                        break;
                    case "etc":
                        // SetupEtc(directory);
                        break;
                    case "home":
                        SetupHome(directory);
                        break;
                }
            }
        }

        private void SetupVar(Directory var)
        {
            throw new System.NotImplementedException();
        }

        private void SetupLib(Directory lib)
        {
            throw new System.NotImplementedException();
        }

        private void SetupUsr(Directory usr)
        {
            throw new System.NotImplementedException();
        }

        private void SetupBin(Directory bin)
        {
            throw new System.NotImplementedException();
        }

        private void SetupEtc(Directory etc)
        {
            throw new System.NotImplementedException();
        }

        private void SetupHome(Directory home)
        {
            Directory user = home.AddItem(new Directory("user", home));
            SetupUser(user);
        }

        private void SetupUser(Directory user)
        {
            File task = user.AddItem(new File("task.txt"));
            task.Content = "Find your way into the remote system (192.168.62.2) and retrieve the password for the user.";
        }
    }
}
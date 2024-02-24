using Exceptions;

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
            SetupRoot();
            CurrentDirectory = Root;
        }

        public void ChangeDirectory(string path)
        {
            IFileSystemItem fileSystemItem = Root.GetItem(path);
            if (fileSystemItem is Directory)
            {
                CurrentDirectory = (Directory) fileSystemItem;
            }
            else
            {
                throw new FileSystemException("Not a directory.");
            }
        }

        public void SetupRoot()
        {

        }
    }
}
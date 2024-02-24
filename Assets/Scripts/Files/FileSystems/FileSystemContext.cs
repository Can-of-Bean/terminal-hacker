namespace Files
{
    public static class FileSystemContext
    {
        public static IFileSystem FileSystem { get; private set; }
        public static Directory CurrentDirectory { get; private set; }

        public static void SetFileSystem(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
            CurrentDirectory = fileSystem.Root;
        }

        public static void SetCurrentDirectory(Directory directory)
        {
            CurrentDirectory = directory;
        }

    }
}
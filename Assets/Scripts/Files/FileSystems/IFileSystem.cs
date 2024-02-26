namespace Files
{
    public interface IFileSystem
    {
        string Name { get; }
        Directory Root { get; }
        Directory CurrentDirectory { get; }

        void SetupRoot();

        void ChangeDirectory(string path);
    }
}
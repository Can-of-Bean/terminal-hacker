namespace Files
{
    public interface IFileSystem
    {
        string Name { get; }
        Directory Root { get; }

        void SetupRoot();
    }
}
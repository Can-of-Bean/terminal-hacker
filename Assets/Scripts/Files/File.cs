namespace FileStructure
{
    public class File : IFileSystemItem
    {
        public string Name { get; }

        public File(string name)
        {
            Name = name;
        }
    }
}
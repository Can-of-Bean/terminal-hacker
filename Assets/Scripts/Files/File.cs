namespace Files
{
    public class File : IFileSystemItem
    {
        public string Name { get; }
        public string Content { get; set; }

        public File(string name)
        {
            Name = name;
            Content = "";
        }

        public File(string name, string content)
        {
            Name = name;
            Content = content;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
using System.Collections.Generic;

namespace FileStructure
{
    public class Directory : IFileSystemItem
    {
        public string Name { get; }
        public List<IFileSystemItem> Items { get; }

        public Directory(string name)
        {
            Name = name;
            Items = new List<IFileSystemItem>();
        }

        public void AddItem(IFileSystemItem item)
        {
            Items.Add(item);
        }
    }
}
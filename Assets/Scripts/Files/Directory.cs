using System.Collections.Generic;

namespace Files
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

        public void RemoveItem(IFileSystemItem item)
        {
            Items.Remove(item);
        }

        public IFileSystemItem GetItem(string name)
        {
            return Items.Find(item => item.Name == name);
        }

        public bool IsDirectory(string name)
        {
            return GetItem(name) is Directory;
        }

        public bool IsFile(string name)
        {
            return GetItem(name) is File;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
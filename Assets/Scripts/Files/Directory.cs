using System.Collections.Generic;
using Exceptions;
using UnityEngine;

namespace Files
{
    public class Directory : IFileSystemItem
    {
        public string Name { get; }
        public List<IFileSystemItem> Items { get; }
        public Directory? Parent { get; set; }

        public Directory(string name)
        {
            Name = name;
            Items = new List<IFileSystemItem>();
        }

        public Directory(string name, Directory parent)
        {
            Name = name;
            Items = new List<IFileSystemItem>();
            Parent = parent;
        }

        public T AddItem<T>(T item) where T : IFileSystemItem
        {
            Items.Add(item);
            return item;
        }

        public T RemoveItem<T>(T item) where T : IFileSystemItem
        {
            Items.Remove(item);
            return item;
        }

        public IFileSystemItem GetItem(string path)
        {
            string[] pathParts = path.Split('/');
            IFileSystemItem current = this;
            for (int i = 0; i < pathParts.Length; i++)
            {
                string part = pathParts[i];
                if (part == "..")
                {
                    if (current is Directory directory)
                    {
                        current = directory.Parent ?? throw new FileSystemException("Cannot go up from root.");
                    }
                    else
                    {
                        throw new FileSystemException($"{current.Name} is not a directory.");
                    }
                }
                else if (part == ".")
                {
                    continue;
                }
                else
                {
                    if (current is Directory directory)
                    {
                        IFileSystemItem? item = directory.Items.Find(x => x.Name == part);
                        if (item == null)
                        {
                            throw new FileSystemException($"No such file or directory: {part}");
                        }

                        current = item;
                    }
                    else
                    {
                        throw new FileSystemException($"{current.Name} is not a directory.");
                    }
                }
            }

            return current;
        }

        public string GetPath()
        {
            if (Parent == null)
            {
                return "/";
            }

            string parentPath = Parent.GetPath();

            if (parentPath == "/")
            {
                return $"/{Name}";
            }

            return $"{parentPath}/{Name}";
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
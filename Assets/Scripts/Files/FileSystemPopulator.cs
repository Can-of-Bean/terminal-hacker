using System;
using UnityEngine;

namespace Files
{
    public class FileSystemPopulator : SingletonBehaviour<FileSystemPopulator>
    {
        public FileSystemCatalogue LocalFilesystemRootCatalogue = null!;
        public FileSystemCatalogue RemoteFilesystemRootCatalogue = null!;

        private void Awake()
        {
            if (LocalFilesystemRootCatalogue != null)
            {
                LocalFilesystemRootCatalogue.AddToDirectory(LocalFileSystem.Instance.Root);
            }
            
            if (RemoteFilesystemRootCatalogue != null)
            {
                RemoteFilesystemRootCatalogue.AddToDirectory(RemoteFileSystem.Instance.Root);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Files
{
    [CreateAssetMenu(menuName = "ScriptableObjects/File System Catalogue", fileName = "Folder Catalogue", order = 0)]
    public class FileSystemCatalogue : ScriptableObject
    {
        public string FolderName = String.Empty;
        
        public List<FileSystemCatalogue> Subdirectories = new List<FileSystemCatalogue>();

        public List<FileSystemCatalogueEntry> Files = new List<FileSystemCatalogueEntry>();

        public void AddToDirectory(Directory directory)
        {
            // add this directory
            directory.GetOrAddDirectory(FolderName);
            
            // depth first recursive call to add subfolders
            foreach (FileSystemCatalogue subdirectory in Subdirectories)
            {
                subdirectory.AddToDirectory(directory);
            }

            // add all child files
            foreach (FileSystemCatalogueEntry catalogueEntry in Files)
            {
                directory.AddItem(new File(catalogueEntry.Name, catalogueEntry.File.text));
            }
        }
    }
}
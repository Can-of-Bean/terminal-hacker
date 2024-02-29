using System;
using UnityEngine;

namespace Files
{
    [Serializable]
    public class FileSystemCatalogueEntry
    {
        [SerializeField]
        public TextAsset File = null!;
        
        [SerializeField]
        public string Name = String.Empty;
    }
}
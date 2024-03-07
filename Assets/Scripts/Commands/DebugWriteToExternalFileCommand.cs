using System;
using System.Collections.Generic;
using System.IO;
using Terminal;
using UnityEngine;
using File = Files.File;

namespace Commands
{
    public class DebugWriteToExternalFileCommand : ICommand
    {
        public void Execute(string[] args, Dictionary<string, string> flags)
        {
            if (args.Length != 2) return;

            File? file = TerminalControl.Instance.CurrentFileSystem.CurrentDirectory.FindFile(args[0]);

            if (file is null) return;

            string outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TerminalOutput");
            Directory.CreateDirectory(outputFolder);
            using StreamWriter sw = new StreamWriter(Path.Combine(outputFolder, args[1]));
            sw.Write(file.Content);
        }

        public string Name { get; } = "debugwrite";
        public string Description { get; } = "";
    }
}
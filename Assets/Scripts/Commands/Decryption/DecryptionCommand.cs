using System;
using System.Collections.Generic;
using Commands.Decryption.DecryptionMethods;
using Files;
using Terminal;

namespace Commands.Decryption
{
    public class DecryptionCommand : ICommand
    {
        private const string USAGE_HELP = "decrypt <source file> <destination file> <decryption type> (optional)<decryption key>";

        private static readonly Dictionary<string, IDecryptionMethod> s_decryptionMethods;

        static DecryptionCommand()
        {
            s_decryptionMethods = new Dictionary<string, IDecryptionMethod>()
            {
                { "base64", new Base64DecryptionMethod() },
            };
        }

        public void Execute(string[] args)
        {
            if (args.Length < 3 || args.Length > 4)
            {
                TerminalControl.Instance.WriteLineToConsole("Decrypt must take in between 3 and 4 arguments\nUsage: " + USAGE_HELP);
                return;
            }

            File? sourceFile = TerminalControl.Instance.CurrentFileSystem.CurrentDirectory.FindFile(args[0]);

            if (sourceFile is null)
            {
                TerminalControl.Instance.WriteLineToConsole($"Could not find source file \"{args[0]}\"");
                return;
            }

            if (String.IsNullOrEmpty(args[1]))
            {
                TerminalControl.Instance.WriteLineToConsole($"Destination must be a file.");
            }

            if (s_decryptionMethods.TryGetValue(args[2], out IDecryptionMethod method))
            {
                if (method.RequiresKey)
                {
                    if (args.Length == 3)
                    {
                        TerminalControl.Instance.WriteLineToConsole($"This decryption method requires a key but non was provided.");
                        return;
                    }
                    else
                    {
                        Decrypt(method, sourceFile, args[1], args[3]);
                        return;
                    }
                }
                else
                {
                    Decrypt(method, sourceFile, args[1], String.Empty);
                    return;
                }
            }
            else
            {
                TerminalControl.Instance.WriteLineToConsole($"Could not determine decryption method {args[2]}");
                return;
            }
        }

        private void Decrypt(IDecryptionMethod method, File source, string destination, string key)
        {
            string decryptedText = method.Decrypt(source.Content, key);
            File? destinationFile = TerminalControl.Instance.CurrentFileSystem.CurrentDirectory.FindFile(destination);

            if (destinationFile == null)
            {
                destinationFile = TerminalControl.Instance.CurrentFileSystem.CurrentDirectory.AddItem(new File(destination.TrimEnd('/').Split('/')[^1]));
            }

            destinationFile.Content = decryptedText;
        }

        public string Name { get; } = "decrypt";
        public string Description { get; } = "Decrypts a file\nUsage: " + USAGE_HELP;
    }
}
using System;
using System.Collections.Generic;
using Commands.Decryption;
using Commands.Encryption.EncryptionMethods;
using Files;
using Terminal;

namespace Commands.Encryption
{
    public class EncryptionCommand : ICommand
    {
        private const string USAGE_HELP = "encrypt <source file> <destination file> <encryption type> (optional)<encryption key>";

        private static readonly Dictionary<string, IEncryptionMethod> s_encryptionMethods;

        static EncryptionCommand()
        {
            s_encryptionMethods = new Dictionary<string, IEncryptionMethod>()
            {
                { "base64", new Base64EncryptionMethod() },
                { "aes", new AesEncryptionMethod() },
            };
        }

        public void Execute(string[] args, Dictionary<string, string> flags)
        {
            if (args.Length < 3 || args.Length > 4)
            {
                TerminalControl.Instance.WriteLineToConsole("Encrypt must take in between 3 and 4 arguments\nUsage: " + USAGE_HELP);
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

            // select the key to use
            // or notify the user and exit on an error
            string key;
            if (s_encryptionMethods.TryGetValue(args[2], out IEncryptionMethod method))
            {
                if (method.RequiresKey)
                {
                    if (args.Length == 3)
                    {
                        TerminalControl.Instance.WriteLineToConsole($"This encryption method requires a key but non was provided.");
                        return;
                    }
                    else
                    {
                        key = args[3];
                    }
                }
                else
                {
                    key = String.Empty;
                }
            }
            else
            {
                TerminalControl.Instance.WriteLineToConsole($"Could not determine encryption method {args[2]}");
                return;
            }

            try
            {
                Encrypt(method, sourceFile, args[1], key);
                TerminalControl.Instance.WriteLineToConsole($"Encrypted to {args[1]}");
            }
            catch (Exception e)
            {
                TerminalControl.Instance.WriteLineToConsole($"An error occured while encrypting:\n{e.Message}");
            }
        }

        private void Encrypt(IEncryptionMethod method, File source, string destination, string key)
        {
            string decryptedText = method.Encrypt(source.Content, key);
            File? destinationFile = TerminalControl.Instance.CurrentFileSystem.CurrentDirectory.FindFile(destination);

            if (destinationFile == null)
            {
                destinationFile = TerminalControl.Instance.CurrentFileSystem.CurrentDirectory.AddItem(new File(destination.TrimEnd('/').Split('/')[^1]));
            }

            destinationFile.Content = decryptedText;
        }

        public string Name { get; } = "encrypt";
        public string Description { get; } = "Encrypts a file\nUsage: " + USAGE_HELP;
    }
}
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Terminal;

namespace Commands.Decryption.DecryptionMethods
{
    public class AesDecryptionMethod : IDecryptionMethod
    {
        public string Decrypt(string fileContents, string key)
        {
            // create aes manager
            using Aes aes = Aes.Create();
            
            // get file contents as byte span
            ReadOnlySpan<byte> messageBytes = Convert.FromBase64String(fileContents);

            // iv is first 16 bytes of the file
            ReadOnlySpan<byte> iv = messageBytes.Slice(0, 16);
                
            // get the data part of the file
            ReadOnlySpan<byte> encryptedMessage = messageBytes.Slice(16);

            // set iv and key
            aes.IV = iv.ToArray();
            using MD5 md5 = MD5.Create();
            aes.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));

            // get the decrypt transformer and decrypt the data
            using ICryptoTransform? decryptTransformer = aes.CreateDecryptor();
            
            // read data using streams
            using MemoryStream dataStream = new MemoryStream(encryptedMessage.ToArray());
            using CryptoStream cryptoStream = new CryptoStream(dataStream, decryptTransformer, CryptoStreamMode.Read);
            using StreamReader sr = new StreamReader(cryptoStream);
            
            string result = sr.ReadToEnd();
            return result;
        }

        public bool RequiresKey { get; } = true;
    }
}
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
            byte[] encryptedBytes = Convert.FromBase64String(fileContents);

            // Create AES manager and set block size
            Aes aesDecrypt = Aes.Create();
            aesDecrypt.BlockSize = 128;

            // Set key using MD5
            using MD5 md5 = MD5.Create();
            aesDecrypt.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));

            // Set IV using first 16 bytes of the file
            byte[] iv = new byte[16];
            Array.Copy(encryptedBytes, iv, 16);
            aesDecrypt.IV = iv;

            // Get the data part of the file and decrypt it
            ICryptoTransform decryptor = aesDecrypt.CreateDecryptor();
            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 16, encryptedBytes.Length - 16);

            return  Encoding.Unicode.GetString(decryptedBytes);;
        }

        public bool RequiresKey { get; } = true;
    }
}
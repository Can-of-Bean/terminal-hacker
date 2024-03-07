using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Commands.Encryption.EncryptionMethods
{
    public class AesEncryptionMethod : IEncryptionMethod
    {
        public string Encrypt(string message, string key)
        {
            // Create AES manager and set block and key size
            using Aes aes = Aes.Create();
            aes.BlockSize = 128;
            aes.KeySize = 128;

            // Set key
            using MD5 md5 = MD5.Create();
            byte[] keyBytes = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            aes.Key = keyBytes;

            // Set IV using RandomNumberGenerator
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] iv = new byte[16];
            rng.GetBytes(iv);
            aes.IV = iv;

            // Encrypt the message
            ICryptoTransform encryptor = aes.CreateEncryptor();
            byte[] plainText = Encoding.Unicode.GetBytes(message);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(plainText, 0, plainText.Length);

            // Combine IV and encrypted message
            byte[] finalData = new byte[iv.Length + encryptedBytes.Length];
            Array.Copy(iv, finalData, 16);
            Array.Copy(encryptedBytes, 0, finalData, 16, encryptedBytes.Length);

            return Convert.ToBase64String(finalData);
        }

        public bool RequiresKey { get; } = true;
    }
}
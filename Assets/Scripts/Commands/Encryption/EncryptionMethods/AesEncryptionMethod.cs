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
            using MD5 md5 = MD5.Create();
            byte[] keyBytes = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            // exception will be caught by encryptor
            if (keyBytes.Length != 128 / 8)
                throw new Exception($"A key of length {keyBytes.Length} is not valid. Key must be {128 / 8} bytes.");

            // create aes object
            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            
            // get iv part of key
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] iv = new byte[16];
            rng.GetBytes(iv);
            aes.IV = iv;

            using ICryptoTransform encryptTransformer = aes.CreateEncryptor();

            using MemoryStream dataStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(dataStream, encryptTransformer, CryptoStreamMode.Write);
            using StreamWriter sw = new StreamWriter(cryptoStream);
            sw.Write(message);
            
            // ensure dataStream contains data
            sw.Flush();
            cryptoStream.Flush();

            byte[] dataStreamBytes = dataStream.ToArray();
            byte[] finalData = new byte[iv.Length + dataStreamBytes.Length];
            Array.Copy(iv, finalData, 16);
            Array.Copy(dataStreamBytes, 0, finalData, 16, dataStreamBytes.Length);
            
            return Convert.ToBase64String(finalData);
        }

        public bool RequiresKey { get; } = true;
    }
}
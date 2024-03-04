using System;
using System.Text;

namespace Commands.Encryption.EncryptionMethods
{
    public class Base64EncryptionMethod : IEncryptionMethod
    {
        public string Encrypt(string message, string key)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            return Convert.ToBase64String(messageBytes);
        }

        public bool RequiresKey { get; } = false;
    }
}
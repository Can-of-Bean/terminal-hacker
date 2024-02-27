using System;
using System.Text;

namespace Commands.Decryption.DecryptionMethods
{
    public class Base64DecryptionMethod : IDecryptionMethod
    {
        public string Decrypt(string fileContents, string key)
        {
            byte[] data = Convert.FromBase64String(fileContents);
            return Encoding.UTF8.GetString(data);
        }

        public bool RequiresKey => false;
    }
}
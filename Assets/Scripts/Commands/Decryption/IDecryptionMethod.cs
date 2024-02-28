namespace Commands.Decryption
{
    public interface IDecryptionMethod
    {
        public string Decrypt(string fileContents, string key);
        
        public bool RequiresKey { get; }
    }
}
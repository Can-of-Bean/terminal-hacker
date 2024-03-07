namespace Commands.Encryption
{
    public interface IEncryptionMethod
    {
        public string Encrypt(string message, string key);
        
        public bool RequiresKey { get; }
    }
}
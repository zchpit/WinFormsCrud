namespace CommonLibrary.Strategy
{
    public interface IEncryptStrategy
    {
        string Encrypt(string clearText);
        string Decrypt(string cipherText);
    }
}

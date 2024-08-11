namespace CommonLibrary.Strategy
{
    public interface ITransferStrategy
    {
        string Encrypt(string clearText);
        string Decrypt(string cipherText);
    }
}

namespace CommonLibrary.Strategy
{
    public class Base64TransferStrategy : ITransferStrategy
    {
        public string Encrypt(string clearText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(clearText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string Decrypt(string cipherText)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(cipherText);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}

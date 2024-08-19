using CommonLibrary.Strategy;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace CommonLibraryTests.StrategyTests
{
    [ExcludeFromCodeCoverage]
    public class Rfc2898EncryptStrategyTests
    {
        private Rfc2898EncryptStrategy _strategy;
        public Rfc2898EncryptStrategyTests()
        {
            _strategy = new Rfc2898EncryptStrategy();
        }

        [Theory()]
        [InlineData("test")]
        [InlineData("abracadabra")]
        [InlineData("ąężzźqlł")]
        public void Encrypt_ShouldBeRepetitive_IsRepetitive(string input)
        {
            string empty = string.Empty;

            var result1 = _strategy.Encrypt(input);
            var result2 = _strategy.Encrypt(input);

            result1.Should().Be(result2);
        }

        [Theory()]
        [InlineData("test")]
        [InlineData("abracadabra")]
        [InlineData("ąężzźqlł")]
        public void EncryptDecrypt_ShouldBeRepetitive_IsRepetitive(string input)
        {
            var result1 = _strategy.Encrypt(input);
            var result2 = _strategy.Decrypt(result1);

            result2.Should().Be(input);
        }
    }
}

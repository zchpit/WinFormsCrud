using CommonLibrary.Strategy;
using FluentAssertions;

namespace CommonLibraryTests.StrategyTests
{
    public class Base64TransferStrategyTests
    {
        private Base64TransferStrategy _strategy;
        public Base64TransferStrategyTests()
        {
            _strategy = new Base64TransferStrategy();
        }

        [Theory()]
        [InlineData("test")]
        [InlineData("abracadabra")]
        [InlineData("ąężzźqlł")]
        public void Encrypt_ShouldBeRepetitive_IsRepetitive(string input)
        {
            var result1 = _strategy.Encrypt(input);
            var result2 = _strategy.Decrypt(result1);

            result2.Should().Be(input);
        }
    }
}

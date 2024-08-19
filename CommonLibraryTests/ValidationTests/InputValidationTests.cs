using CommonLibrary.Validation;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;


namespace CommonLibraryTests.ValidationTests
{
    [ExcludeFromCodeCoverage]
    public class InputValidationTests
    {
        [Fact]
        public void IsUserValid_Empty_ReturnFalse()
        {
            string empty = string.Empty;

            var result = InputValidation.IsUserValid(empty);

            result.Should().NotBeEmpty();
            result.FirstOrDefault(a => a == CommonLibrary.Enums.UiValidationErrors.UsernameEmpty).Should().Be(CommonLibrary.Enums.UiValidationErrors.UsernameEmpty);
        }

        [Fact]
        public void IsUserValid_HaveValue_ReturnTrue()
        {
            string empty = "test";

            var result = InputValidation.IsUserValid(empty);

            result.Should().BeEmpty();
        }

        [Fact]
        public void IsPasswordValid_Empty_ReturnFalse()
        {
            string empty = string.Empty;

            var result = InputValidation.IsPasswordValid(empty);

            result.Should().NotBeEmpty();
            result.FirstOrDefault(a => a == CommonLibrary.Enums.UiValidationErrors.PasswordEmpty).Should().Be(CommonLibrary.Enums.UiValidationErrors.PasswordEmpty);
        }

        [Fact]
        public void IsPasswordValid_HaveLessThan2_ReturnFalse()
        {
            string empty = "t";

            var result = InputValidation.IsPasswordValid(empty);

            result.Should().NotBeEmpty();
            result.FirstOrDefault(a => a == CommonLibrary.Enums.UiValidationErrors.PasswordLengthToShort).Should().Be(CommonLibrary.Enums.UiValidationErrors.PasswordLengthToShort);
        }

        [Fact]
        public void IsPasswordValid_HaveMoreThan5_ReturnTrue()
        {
            string empty = "testsasasa";

            var result = InputValidation.IsPasswordValid(empty);

            result.Should().BeEmpty();
        }
    }
}

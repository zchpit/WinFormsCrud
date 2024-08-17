using CommonLibrary.Enums;

namespace CommonLibrary.Validation
{
    public static class InputValidation
    {
        public static List<UiValidationErrors> IsUserValid(string username)
        {
            List<UiValidationErrors> validationErrors = new List<UiValidationErrors>();

            if (string.IsNullOrEmpty(username))
                validationErrors.Add(UiValidationErrors.UsernameEmpty);

            return validationErrors;
        }

        public static List<UiValidationErrors> IsPasswordValid(string password)
        {
            List<UiValidationErrors> validationErrors = new List<UiValidationErrors>();

            if (string.IsNullOrEmpty(password))
                validationErrors.Add(UiValidationErrors.PasswordEmpty);

            if (password.Length < 2)
                validationErrors.Add(UiValidationErrors.PasswordLengthToShort);

            //some other validation stuff like Regexp etc.

            return validationErrors;
        }
    }
}

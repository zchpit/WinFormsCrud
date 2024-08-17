using CommonLibrary.Dto;
using CommonLibrary.Enums;
using CommonLibrary.Strategy;
using CommonLibrary.Validation;
using Flurl;
using Flurl.Http;
using WinFormsCrud.Helper;
using WinFormsCrud.Interface;

namespace WinFormsCrud.Services
{
    public class UserService : IUserService
    {
        ITransferStrategy _transferStrategy;

        public UserService(ITransferStrategy transferStrategy) 
        {
            _transferStrategy = transferStrategy;
        }

        public async ValueTask<SimpleUserDto> Login(string username, string password)
        {
            var usernameValidation = InputValidation.IsUserValid(username);
            var passwordValidation = InputValidation.IsPasswordValid(username);

            if (!usernameValidation.Any() && !passwordValidation.Any())
            {
                string encryptedUsername = _transferStrategy.Encrypt(username);
                string encryptedPassword = _transferStrategy.Encrypt(password);

                var simpleUserDto = await ApiHelper
                                    .urlBase
                                    .AppendPathSegment(ApiHelper.userControllerName)
                                    .AppendPathSegment(encryptedUsername)
                                    .AppendPathSegment(encryptedPassword)
                                    .GetAsync()
                                    .ReceiveJson<SimpleUserDto>();

                return simpleUserDto;
            }
            else
            {
                return null;
            }
        }
    }
}

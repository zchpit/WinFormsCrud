using CommonLibrary.Dto;
using CommonLibrary.Strategy;
using CommonLibrary.Validation;
using Flurl;
using Flurl.Http;
using NLog;
using WinFormsCrud.Helper;
using WinFormsCrud.Interface;

namespace WinFormsCrud.Services
{
    public class UserService : IUserService
    {
        ITransferStrategy transferStrategy;
        ILogger logger;

        public UserService(ITransferStrategy transferStrategy, ILogger logger)
        {
            this.transferStrategy = transferStrategy;
            this.logger = logger;
        }

        public async ValueTask<SimpleUserDto> Login(string username, string password)
        {
            try
            {
                var usernameValidation = InputValidation.IsUserValid(username);
                var passwordValidation = InputValidation.IsPasswordValid(username);

                if (!usernameValidation.Any() && !passwordValidation.Any())
                {
                    string encryptedUsername = transferStrategy.Encrypt(username);
                    string encryptedPassword = transferStrategy.Encrypt(password);

                    var simpleUserDto = await ApiHelper
                                        .urlBase
                                        .AppendPathSegment(ApiHelper.userControllerName)
                                        .AppendPathSegment(ApiHelper.userLoginMethodName)
                                        .AppendPathSegment(encryptedUsername)
                                        .AppendPathSegment(encryptedPassword)
                                        .GetAsync()
                                        .ReceiveJson<SimpleUserDto>();

                    return simpleUserDto;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return null;
        }
    }
}

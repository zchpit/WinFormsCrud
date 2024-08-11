using CommonLibrary.Dto;
using CommonLibrary.Strategy;
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

        public bool IsUserValid(string username)
        {
            if(string.IsNullOrEmpty(username)) 
                return false;

            return true;
        }
        public bool IsPasswordValid(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if(password.Length < 5)
                return false;

            //some other validation stuff like Regexp etc.

            return true;
        }
    }
}

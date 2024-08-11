using SimpleWebApi.Dto;
using SimpleWebApi.Interface;
using SimpleWebApi.IRepository;
using SimpleWebApi.Strategy;

namespace SimpleWebApi.Services
{
    public class UserService : IUserService
    {
        private IEncryptStrategy encryptStrategy;
        private IUserRepository userRepository;

        public UserService(IEncryptStrategy encryptStrategy, IUserRepository userRepository) 
        { 
            this.encryptStrategy = encryptStrategy;
            this.userRepository= userRepository;
        }

        public async ValueTask<SimpleUserDto> Login(string username, string password)
        {
            if (IsUserValid(username) && IsUserValid(password)) 
            {
                var encryptedPassword = encryptStrategy.Encrypt(password);
                var user = await userRepository.GetSimpleUserDto(username, encryptedPassword);
                return user;
            }

            return null; 
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

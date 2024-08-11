using CommonLibrary.Dto;
using SimpleWebApi.Interface;
using SimpleWebApi.IRepository;
using CommonLibrary.Strategy;

namespace SimpleWebApi.Services
{
    public class UserService : IUserService
    {
        private IEncryptStrategy encryptStrategy;
        private IUserRepository userRepository; 
        private ITransferStrategy transferStrategy;

        public UserService(IEncryptStrategy encryptStrategy, ITransferStrategy transferStrategy, IUserRepository userRepository) 
        { 
            this.encryptStrategy = encryptStrategy;
            this.userRepository= userRepository;
            this.transferStrategy = transferStrategy;
        }

        public async ValueTask<SimpleUserDto> Login(string encryptedUsername, string encryptedPassword)
        {
            if (IsUserValid(encryptedUsername) && IsUserValid(encryptedPassword)) 
            {
                var username = transferStrategy.Decrypt(encryptedUsername);
                var password = transferStrategy.Decrypt(encryptedPassword);

                var encryptedPasswordDb = encryptStrategy.Encrypt(password);
                var user = await userRepository.GetSimpleUserDto(username, encryptedPasswordDb);
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

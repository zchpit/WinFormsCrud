using CommonLibrary.Dto;
using CommonLibrary.Strategy;
using CommonLibrary.Validation;
using SimpleWebApi.Interface;
using SimpleWebApi.IRepository;

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
            var username = transferStrategy.Decrypt(encryptedUsername);
            var password = transferStrategy.Decrypt(encryptedPassword);

            var usernameValidation = InputValidation.IsUserValid(username);
            var passwordValidation = InputValidation.IsPasswordValid(password);
            if (!usernameValidation.Any() && !passwordValidation.Any())
            {

                var encryptedPasswordDb = encryptStrategy.Encrypt(password);
                var user = await userRepository.GetSimpleUserDto(username, encryptedPasswordDb);
                return user;
            }

            return null; 
        }
    }
}

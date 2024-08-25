using CommonLibrary.Dto;
using CommonLibrary.Strategy;
using CommonLibrary.Validation;
using SimpleWebApi.IRepository;
using SimpleWebApi.IServices;

namespace SimpleWebApi.Services
{
    public class UserService : IUserService
    {
        private IEncryptStrategy encryptStrategy;
        private IRepositoryWrapper repository;
        private ITransferStrategy transferStrategy;

        public UserService(IRepositoryWrapper repository, IEncryptStrategy encryptStrategy, ITransferStrategy transferStrategy)
        {
            this.encryptStrategy = encryptStrategy;
            this.repository = repository;
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
                var user = await repository.UserRepository.GetSimpleUserDto(username, encryptedPasswordDb);
                return user;
            }

            return null;
        }
    }
}

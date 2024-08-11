using WinFormsCrud.Dto;
using WinFormsCrud.Interface;
using WinFormsCrud.IRepository;
//using WinFormsCrud.Strategy;

namespace WinFormsCrud.Services
{
    public class UserService : IUserService
    {

        public UserService() 
        { 
            //this.encryptStrategy = encryptStrategy;
        }

        public async ValueTask<SimpleUserDto> Login(string username, string password)
        {
            /*
            if (IsUserValid(username) && IsUserValid(password)) 
            {
                var encryptedPassword = encryptStrategy.Encrypt(password);
                var user = await userRepository.GetSimpleUserDto(username, encryptedPassword);
                return user;
            }*/

            return null; 
        }

        public void Logout(int userId)
        {
            //TODO;
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

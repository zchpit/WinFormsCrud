using WinFormsCrud.Dto;
using WinFormsCrud.Interface;

namespace WinFormsCrud.Services
{
    public class LoginService : ILoginService
    {
        public UserDto Login(string username, string password)
        {
            if (IsUserValid(username) && IsUserValid(password)) 
            {
                if(username == "test" && password == "test")
                {
                    return new UserDto() { Id = 1, Name = "test" }; 
                }

                if (username == "manager" && password == "manager")
                {
                    return new UserDto() { Id = 2, Name = "manager" };
                }
            }

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

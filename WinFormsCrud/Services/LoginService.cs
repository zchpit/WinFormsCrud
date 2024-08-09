using WinFormsCrud.Dto;
using WinFormsCrud.Interface;

namespace WinFormsCrud.Services
{
    public class LoginService : ILoginService
    {
        public int Login(string username, string password)
        {
            if (IsUserValid(username) && IsUserValid(password)) 
            {
                if(username == "test" && password == "test")
                {
                    var testUser = new UserDto() { Id = 1, Name = "test" };
                    return 1;
                }

                if (username == "manager" && password == "manager")
                {
                    var manager = new UserDto() { Id = 2, Name = "manager" };

                    return 2;
                }
            }

            return 0; 
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

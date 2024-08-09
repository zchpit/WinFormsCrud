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
                    var testUser = new UserDto() { Id = 1, Name = "test" };
                    testUser.UserCases = new List<CaseDto>()
                    {
                        new CaseDto() { Id = 1, Header = "Case1", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", Priority = 1, CreateDate = new DateTime(2022,02,02) },
                        new CaseDto() { Id = 2, Header = "Case2", Description = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", Priority = 2, CreateDate = new DateTime(2023,10,02) },                        
                        new CaseDto() { Id = 3, Header = "Case3", Description = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur", Priority = 4, CreateDate = new DateTime(2024,06,02) },
                        new CaseDto() { Id = 4, Header = "Case4", Description = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\"", Priority = 5, CreateDate = new DateTime(2021,01,04) }
                    };

                    return testUser;
                }

                if (username == "manager" && password == "manager")
                {
                    var manager = new UserDto() { Id = 2, Name = "manager" };

                    return manager;
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

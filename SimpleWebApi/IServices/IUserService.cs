using SimpleWebApi.Dto;

namespace SimpleWebApi.Interface
{
    public interface IUserService
    {
        ValueTask<SimpleUserDto> Login(string username, string password);

        void Logout(int userId);

        bool IsUserValid(string username);

        bool IsPasswordValid(string password);
    }
}

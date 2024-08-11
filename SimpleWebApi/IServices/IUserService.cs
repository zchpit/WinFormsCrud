using CommonLibrary.Dto;

namespace SimpleWebApi.Interface
{
    public interface IUserService
    {
        ValueTask<SimpleUserDto> Login(string username, string password);

        bool IsUserValid(string username);

        bool IsPasswordValid(string password);
    }
}

using CommonLibrary.Dto;

namespace SimpleWebApi.Interface
{
    public interface IUserService
    {
        ValueTask<SimpleUserDto> Login(string encryptedUsername, string encryptedPassword);

        bool IsUserValid(string username);

        bool IsPasswordValid(string password);
    }
}

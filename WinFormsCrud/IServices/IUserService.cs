using CommonLibrary.Dto;

namespace WinFormsCrud.Interface
{
    public interface IUserService
    {
        ValueTask<SimpleUserDto> Login(string username, string password);

        bool IsUserValid(string username);

        bool IsPasswordValid(string password);
    }
}

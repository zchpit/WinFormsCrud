using CommonLibrary.Dto;

namespace WinFormsCrud.Interface
{
    public interface IUserService
    {
        ValueTask<SimpleUserDto> Login(string username, string password);
    }
}

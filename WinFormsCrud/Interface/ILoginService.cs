using WinFormsCrud.Dto;

namespace WinFormsCrud.Interface
{
    public interface ILoginService
    {
        UserDto Login(string username, string password);

        void Logout(int userId);

        bool IsUserValid(string username);

        bool IsPasswordValid(string password);
    }
}

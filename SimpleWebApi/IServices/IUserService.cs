using CommonLibrary.Dto;

namespace SimpleWebApi.IServices
{
    public interface IUserService
    {
        ValueTask<SimpleUserDto> Login(string encryptedUsername, string encryptedPassword);
    }
}

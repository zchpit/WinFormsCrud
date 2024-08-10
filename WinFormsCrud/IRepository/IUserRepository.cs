using WinFormsCrud.Dto;

namespace WinFormsCrud.IRepository
{
    public interface IUserRepository
    {
        public SimpleUserDto GetSimpleUserDto(string username, string password);
    }
}

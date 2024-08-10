using WinFormsCrud.Dto;

namespace WinFormsCrud.IRepository
{
    public interface IUserRepository : IDisposable
    {
        public SimpleUserDto GetSimpleUserDto(string username, string password);
    }
}

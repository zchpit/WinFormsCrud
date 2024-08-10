using WinFormsCrud.Dto;

namespace WinFormsCrud.IRepository
{
    public interface IUserRepository : IDisposable
    {
        public ValueTask<SimpleUserDto> GetSimpleUserDto(string username, string password);
    }
}

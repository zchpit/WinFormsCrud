using SimpleWebApi.Dto;

namespace SimpleWebApi.IRepository
{
    public interface IUserRepository : IDisposable
    {
        public ValueTask<SimpleUserDto> GetSimpleUserDto(string username, string password);
    }
}

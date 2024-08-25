using CommonLibrary.Dto;

namespace SimpleWebApi.IRepository
{
    public interface IUserRepository : IRepositoryBase<Model.User>
    {
        public ValueTask<SimpleUserDto> GetSimpleUserDto(string username, string password);
    }
}

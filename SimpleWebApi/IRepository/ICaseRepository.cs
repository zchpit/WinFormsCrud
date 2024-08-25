using CommonLibrary.Dto;
using SimpleWebApi.Model;

namespace SimpleWebApi.IRepository
{
    public interface ICaseRepository : IRepositoryBase<Case>
    {
        ValueTask<List<Case>> GetUserCases(SimpleUserDto simpleUserDto);
    }
}

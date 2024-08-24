using CommonLibrary.Dto;
using SimpleWebApi.Model;
using SimpleWebApi.Repository;

namespace SimpleWebApi.IRepository
{
    public interface ICaseRepository : IRepositoryBase<Case>
    {
        ValueTask<List<Case>> GetUserCases(SimpleUserDto simpleUserDto);
        Task UpdateCase(Case caseDto);
    }
}

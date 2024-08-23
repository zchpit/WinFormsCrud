using CommonLibrary.Dto;
using SimpleWebApi.Model;
using SimpleWebApi.Repository;

namespace SimpleWebApi.IRepository
{
    public interface ICaseRepository : IRepositoryBase<Case>
    {
        ValueTask<List<Case>> GetAllCases();
        ValueTask<List<Case>> GetUserCases(SimpleUserDto simpleUserDto);
        Task AddCase(Case caseDto, int userId);
        Task UpdateCase(Case caseDto, int userId);
    }
}

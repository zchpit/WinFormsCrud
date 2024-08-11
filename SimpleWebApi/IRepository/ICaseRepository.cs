using SimpleWebApi.Dto;
using SimpleWebApi.Model;

namespace SimpleWebApi.IRepository
{
    public interface ICaseRepository : IDisposable
    {
        ValueTask<List<Case>> GetAllCases();
        ValueTask<List<Case>> GetUserCases(SimpleUserDto simpleUserDto);
        Task AddCase(Case caseDto, int userId);
        Task UpdateCase(Case caseDto, int userId);
    }
}

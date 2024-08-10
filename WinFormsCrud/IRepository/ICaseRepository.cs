using WinFormsCrud.Dto;
using WinFormsCrud.Model;

namespace WinFormsCrud.IRepository
{
    public interface ICaseRepository : IDisposable
    {
        List<Case> GetAllCases();
        List<Case> GetUserCases(SimpleUserDto simpleUserDto);
        Task AddCase(Case caseDto, int userId);
        Task UpdateCase(Case caseDto, int userId);
    }
}

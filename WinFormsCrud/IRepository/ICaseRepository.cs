using WinFormsCrud.Dto;
using WinFormsCrud.Model;

namespace WinFormsCrud.IRepository
{
    public interface ICaseRepository : IDisposable
    {
        List<Case> GetAllCases();
        List<Case> GetUserCases(SimpleUserDto simpleUserDto);
        void AddCase(Case caseDto, int userId);
        void UpdateCase(Case caseDto, int userId);
    }
}

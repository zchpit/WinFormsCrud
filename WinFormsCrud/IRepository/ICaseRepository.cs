using WinFormsCrud.Dto;
using WinFormsCrud.Model;

namespace WinFormsCrud.IRepository
{
    public interface ICaseRepository : IDisposable
    {
        List<Case> GetCases();
        List<Case> GetUserCases(int userId);
        void AddCase(Case caseDto, int userId);
        void UpdateCase(Case caseDto, int userId);
    }
}

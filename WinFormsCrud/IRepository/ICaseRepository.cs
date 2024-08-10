using WinFormsCrud.Dto;

namespace WinFormsCrud.IRepository
{
    public interface ICaseRepository
    {
        List<CaseDto> GetCases();
        List<CaseDto> GetUserCases(int userId);
        void AddCase(CaseDto caseDto, int userId);
        void UpdateCase(CaseDto caseDto, int userId);
    }
}

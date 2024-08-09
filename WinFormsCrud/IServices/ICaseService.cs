using WinFormsCrud.Dto;

namespace WinFormsCrud.Interface
{
    public interface ICaseService
    {
        bool IsValidCase(CaseDto caseDto);

        void UpdateCase(CaseDto caseDto, int userId);

        List<CaseDto> GetUserCases(int userId);
    }
}

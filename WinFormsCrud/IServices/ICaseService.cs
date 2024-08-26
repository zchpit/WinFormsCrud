using CommonLibrary.Dto;

namespace WinFormsCrud.Interface
{
    public interface ICaseService
    {
        bool IsValidCase(CaseCreateDto caseDto);
        ValueTask<List<CaseDto>> GetUserCases(int userId, RoleDto userRole);

        ValueTask<bool> CreateCase(CaseCreateDto caseCreateDto);

        ValueTask<bool> UpdateCase(CaseUpdateDto caseUpdateDto);

        ValueTask<bool> DeleteCase(int caseId, int userId);
    }
}

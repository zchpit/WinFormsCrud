using CommonLibrary.Dto;

namespace WinFormsCrud.Interface
{
    public interface ICaseService
    {
        bool IsValidCase(CaseDto caseDto);

        ValueTask<bool> UpdateCase(CaseDto caseDto, int userId);

        ValueTask<List<CaseDto>> GetUserCases(SimpleUserDto simpleUserDto);
    }
}

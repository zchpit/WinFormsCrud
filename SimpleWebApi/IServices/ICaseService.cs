using CommonLibrary.Dto;

namespace SimpleWebApi.IServices
{
    public interface ICaseService
    {
        bool IsValidCase(CaseDto caseDto);

        ValueTask CreateCase(CaseCreateDto caseCreateDto);
        ValueTask UpdateCase(CaseUpdateDto caseUpdateDto);
        ValueTask DeleteCase(CaseDeleteDto caseDeleteDto);

        ValueTask<List<CaseDto>> GetUserCases(SimpleUserDto simpleUserDto);
    }
}

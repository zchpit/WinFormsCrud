using SimpleWebApi.Dto;

namespace SimpleWebApi.Interface
{
    public interface ICaseService
    {
        bool IsValidCase(CaseDto caseDto);

        Task UpdateCase(CaseDto caseDto, int userId);

        ValueTask<List<CaseDto>> GetUserCases(SimpleUserDto simpleUserDto);
    }
}

using CommonLibrary.Dto;

namespace SimpleWebApi.IServices
{
    public interface ICaseService
    {
        bool IsValidCase(CaseDto caseDto);

        Task UpdateCase(CaseDto caseDto, int userId);

        ValueTask<List<CaseDto>> GetUserCases(SimpleUserDto simpleUserDto);
    }
}

using CommonLibrary.Dto;
using Flurl;
using Flurl.Http;
using WinFormsCrud.Helper;
using WinFormsCrud.Interface;

namespace WinFormsCrud.Services
{
    public class CaseService : ICaseService
    {
        public CaseService()
        {
        }

        public bool IsValidCase(CaseDto caseDto)
        {
            if (string.IsNullOrEmpty(caseDto.Header))
                return false;

            if (string.IsNullOrEmpty(caseDto.Description))
                return false;

            return true;
        }

        public async Task UpdateCase(CaseDto caseDto, int userId)
        {
            if (userId > 0)
            {
                var tmpResult = await ApiHelper
                .urlBase
                .AppendPathSegment(ApiHelper.caseControllerName)
                .SetQueryParams(new
                {
                    userId = userId,
                })
                .PostJsonAsync(caseDto);
            }
        }

        public async ValueTask<List<CaseDto>> GetUserCases(SimpleUserDto simpleUserDto)
        {
            var tmpResult = await ApiHelper
                    .urlBase
                    .AppendPathSegment(ApiHelper.caseControllerName)
                    .AppendPathSegment(simpleUserDto.Id)
                    .AppendPathSegment((int)simpleUserDto.UserRole)
                    .GetAsync()
                    .ReceiveJson<List<CaseDto>>();

            return tmpResult;
        }
    }
}

using CommonLibrary.Dto;
using Flurl;
using Flurl.Http;
using NLog;
using WinFormsCrud.Helper;
using WinFormsCrud.Interface;

namespace WinFormsCrud.Services
{
    public class CaseService : ICaseService
    {
        ILogger logger;

        public CaseService(ILogger logger)
        {
            this.logger = logger;
        }

        public bool IsValidCase(CaseDto caseDto)
        {
            if (string.IsNullOrEmpty(caseDto.Header))
                return false;

            if (string.IsNullOrEmpty(caseDto.Description))
                return false;

            return true;
        }

        public async ValueTask<bool> UpdateCase(CaseDto caseDto, int userId)
        {
            if (userId > 0)
            {
                try
                {
                    var tmpResult = await ApiHelper
                    .urlBase
                    .AppendPathSegment(ApiHelper.caseControllerName)
                    .SetQueryParams(new { userId = userId })
                    .PostJsonAsync(caseDto);

                    return true;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }

            return false;
        }

        public async ValueTask<List<CaseDto>> GetUserCases(SimpleUserDto simpleUserDto)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return null;
        }
    }
}

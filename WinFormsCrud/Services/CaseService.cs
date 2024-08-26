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

        public bool IsValidCase(CaseCreateDto caseDto)
        {
            if (string.IsNullOrEmpty(caseDto.Header))
                return false;

            if (string.IsNullOrEmpty(caseDto.Description))
                return false;

            if (caseDto.Priority == 0)
                return false;

            return true;
        }

        public async ValueTask<List<CaseDto>> GetUserCases(int userId, RoleDto userRole)
        {
            try
            {
                var tmpResult = await ApiHelper
                    .urlBase
                    .AppendPathSegment(ApiHelper.caseControllerName)
                    .AppendPathSegment(ApiHelper.caseGetUserCasesMethodName)
                    .AppendPathSegment(userId)
                    .AppendPathSegment((int)userRole)
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

        public async ValueTask<bool> CreateCase(CaseCreateDto caseCreateDto)
        {
            try
            {
                var tmpResult = await ApiHelper
                .urlBase
                .AppendPathSegment(ApiHelper.caseControllerName)
                .AppendPathSegment(ApiHelper.caseCreateCaseMethodName)
                .PostJsonAsync(caseCreateDto);

                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            
            return false;
        }

        public async ValueTask<bool> UpdateCase(CaseUpdateDto caseUpdateDto)
        {
            try
            {
                var tmpResult = await ApiHelper
                .urlBase
                .AppendPathSegment(ApiHelper.caseControllerName)
                .AppendPathSegment(ApiHelper.caseUpdateCaseMethodName)
                .PutJsonAsync(caseUpdateDto);

                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return false;
        }

        public async ValueTask<bool> DeleteCase(int caseId, int userId)
        {
            if (userId > 0)
            {
                try
                {
                    var tmpResult = await ApiHelper
                    .urlBase
                    .AppendPathSegment(ApiHelper.caseControllerName)
                    .AppendPathSegment(ApiHelper.caseDeleteCaseMethodName)
                    .AppendPathSegment(caseId.ToString())
                    .AppendPathSegment(userId.ToString())
                    .DeleteAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }

            return false;
        }
    }
}

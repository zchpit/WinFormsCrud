using CommonLibrary.Dto;
using WinFormsCrud.Helper;
using WinFormsCrud.Interface;

namespace WinFormsCrud.Services
{
    public class CaseService : ICaseService
    {
        //TODO: remove HttpClient as this object have prolem with socket release. Put IHttpClientFactory:  https://cezarywalenciuk.pl/blog/programing/ihttpclientfactory-na-problem-z-httpclient
        static HttpClient client = new HttpClient();


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
                //Case caseToUpdate = MapCaseDtoToCase(caseDto);
                if (caseDto.Id > 0)
                {

                    //await caseRepository.UpdateCase(caseToUpdate, userId);
                }
                else
                {
                    //await caseRepository.AddCase(caseToUpdate, userId);
                }
            }
        }

        public async ValueTask<List<CaseDto>> GetUserCases(SimpleUserDto simpleUserDto)
        {
            string path = string.Concat(ApiHelper.urlBase, ApiHelper.caseControllerName, "/", simpleUserDto.Id, "/", (int)simpleUserDto.UserRole);

            List<CaseDto> tmpResult = null;
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                tmpResult = await response.Content.ReadAsAsync<List<CaseDto>>();
            }

            return tmpResult;
        }
    }
}

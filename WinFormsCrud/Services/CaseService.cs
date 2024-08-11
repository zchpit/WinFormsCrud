using CommonLibrary.Dto;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using WinFormsCrud.Helper;
using WinFormsCrud.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                string path = string.Concat(ApiHelper.urlBase, ApiHelper.caseControllerName, "?userId=", userId);

                var myContent = JsonConvert.SerializeObject(caseDto);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync(path, byteContent);
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

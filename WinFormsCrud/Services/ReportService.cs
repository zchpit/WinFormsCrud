using CommonLibrary.Dto;
using WinFormsCrud.Helper;
using WinFormsCrud.IServices;

namespace WinFormsCrud.Services
{
    public class ReportService : IReportService
    {        
        //TODO: remove HttpClient as this object have prolem with socket release. Put IHttpClientFactory:  https://cezarywalenciuk.pl/blog/programing/ihttpclientfactory-na-problem-z-httpclient
        static HttpClient client = new HttpClient();
        public ReportService() 
        {

        }

        public async ValueTask<List<ReportDto>> GetReport(int managerId)
        {
            string path = string.Concat(ApiHelper.urlBase, ApiHelper.reportControllerName, "/", managerId);

            List<ReportDto> tmpResult = null;
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                tmpResult = await response.Content.ReadAsAsync<List<ReportDto>>();
            }

            return tmpResult;
        }
    }
}

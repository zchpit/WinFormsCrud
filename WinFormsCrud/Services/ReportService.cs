using CommonLibrary.Dto;
using Newtonsoft.Json;
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

        public void SaveReportToDisc(List<ReportDto> reports)
        {
            var filename = @"SimpleReport_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + ".json";

            using (StreamWriter file = File.CreateText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, reports);
            }
        }
    }
}

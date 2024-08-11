using CommonLibrary.Dto;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using WinFormsCrud.Helper;
using WinFormsCrud.IServices;

namespace WinFormsCrud.Services
{
    public class ReportService : IReportService
    {        
        public ReportService() 
        {

        }

        public async ValueTask<List<ReportDto>> GetReport(int managerId)
        {
            string path = string.Concat(ApiHelper.urlBase, ApiHelper.reportControllerName, "/", managerId);
            var tmpResult = await ApiHelper
                    .urlBase
                    .AppendPathSegment(ApiHelper.reportControllerName)
                    .AppendPathSegment(managerId.ToString())
                    .GetAsync()
                    .ReceiveJson<List<ReportDto>>();

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

using CommonLibrary.Dto;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using NLog;
using WinFormsCrud.Helper;
using WinFormsCrud.IServices;

namespace WinFormsCrud.Services
{
    public class ReportService : IReportService
    {
        ILogger logger;

        public ReportService(ILogger logger)
        {
            this.logger = logger;
        }

        public async ValueTask<List<ReportDto>> GetReport(int managerId)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return null;
        }

        public async ValueTask<string> SaveReportToDisc(List<ReportDto> reports, string fileName)
        {
            var filename = string.Concat(fileName, DateTime.Now.ToString("yyyy_MM_dd_HH_mm"), ".json");

            using (StreamWriter file = File.CreateText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, reports);
            }

            return filename;
        }

        public void CreateFolderIfNotExists(string folderPath)
        {
            bool exists = System.IO.Directory.Exists(folderPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(folderPath);
        }
    }
}

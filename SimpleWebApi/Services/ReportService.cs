using CommonLibrary.Dto;
using SimpleWebApi.IRepository;
using SimpleWebApi.IServices;

namespace SimpleWebApi.Services
{
    public class ReportService : IReportService
    {
        IReportRepository reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async ValueTask<List<ReportDto>> GetReport(int managerId)
        {
            var reports = await reportRepository.GetReport(managerId);

            return reports;
        }
    }
}

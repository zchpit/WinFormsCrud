using CommonLibrary.Dto;

namespace SimpleWebApi.IServices
{
    public interface IReportService
    {
        ValueTask<List<ReportDto>> GetReport(int managerId);
    }
}

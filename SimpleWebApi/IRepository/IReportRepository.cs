using CommonLibrary.Dto;

namespace SimpleWebApi.IRepository
{
    public interface IReportRepository
    {
        ValueTask<List<ReportDto>> GetReport(int managerId);
    }
}

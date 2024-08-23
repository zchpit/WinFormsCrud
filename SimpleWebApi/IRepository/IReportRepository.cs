using CommonLibrary.Dto;

namespace SimpleWebApi.IRepository
{
    public interface IReportRepository : IRepositoryBase<ReportDto>
    {
        ValueTask<List<ReportDto>> GetReport(int managerId);
    }
}

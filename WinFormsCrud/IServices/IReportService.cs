using CommonLibrary.Dto;

namespace WinFormsCrud.IServices
{
    public interface IReportService
    {
        ValueTask<List<ReportDto>> GetReport(int managerId);
        ValueTask<string> SaveReportToDisc(List<ReportDto> reports);
    }
}

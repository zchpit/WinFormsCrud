using CommonLibrary.Dto;

namespace WinFormsCrud.IServices
{
    public interface IReportService
    {
        ValueTask<List<ReportDto>> GetReport(int managerId);
        void SaveReportToDisc(List<ReportDto> reports);
    }
}

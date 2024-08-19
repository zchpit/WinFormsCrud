using CommonLibrary.Dto;
using SimpleWebApi.IServices;
using System.Diagnostics.CodeAnalysis;

namespace SimpleWebApiIntegrationTests.TestServices
{
    [ExcludeFromCodeCoverage]
    public class TestReportService : IReportService
    {
        public async ValueTask<List<ReportDto>> GetReport(int managerId)
        {
            if (managerId == 1)
            {
                List<ReportDto> managerList = new List<ReportDto>();
                managerList.Add(new ReportDto() { Name = "test", Month = "08-2020", NumOfCases = 1 });
                managerList.Add(new ReportDto() { Name = "test2", Month = "10-2022", NumOfCases = 4 });
                managerList.Add(new ReportDto() { Name = "test3", Month = "12-2023", NumOfCases = 2 });

                return managerList;
            }

            return new List<ReportDto>();
        }
    }
}

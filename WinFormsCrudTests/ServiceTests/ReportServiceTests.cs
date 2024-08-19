using CommonLibrary.Dto;
using FluentAssertions;
using Moq;
using SimpleWebApi.IRepository;
using SimpleWebApi.IServices;
using SimpleWebApi.Services;
using System.Diagnostics.CodeAnalysis;

namespace WinFormsCrudTests.ServiceTests
{
    [ExcludeFromCodeCoverage]
    public class ReportServiceTests
    {
        IReportService reportService;
        private readonly Mock<IReportRepository> mockReportRepository;

        public ReportServiceTests()
        {
            mockReportRepository = new Mock<IReportRepository>();
            var mockReportRepositoryObject = mockReportRepository.Object;

            reportService = new ReportService(mockReportRepositoryObject);
        }

        [Fact]
        public async void IsValidCase_GetReport_ReturnReportResult()
        {
            int managerId = It.IsAny<int>();
            List<ReportDto> reportListResult = new List<ReportDto>() { new ReportDto(){ Name = It.IsAny<string>(), Month = It.IsAny<string>(), NumOfCases = It.IsAny<int>() } };
            mockReportRepository.Setup(a => a.GetReport(managerId)).ReturnsAsync(reportListResult);

            var result = await reportService.GetReport(managerId);

            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            mockReportRepository.Verify(a => a.GetReport(managerId), Times.Once);
        }
    }
}
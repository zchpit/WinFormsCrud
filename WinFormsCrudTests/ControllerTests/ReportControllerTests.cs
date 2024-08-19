using CommonLibrary.Dto;
using FluentAssertions;
using Moq;
using SimpleWebApi.Controllers;
using SimpleWebApi.Helpers;
using SimpleWebApi.IServices;
using System.Diagnostics.CodeAnalysis;

namespace SimpleWebApiTests.ControllerTests
{
    [ExcludeFromCodeCoverage]
    public class ReportControllerTests
    {
        private readonly Mock<IReportService> reportService;
        private readonly Mock<ILoggerManager> loggerManager;
        private ReportController reportController;

        public ReportControllerTests()
        {
            reportService = new Mock<IReportService>();
            loggerManager = new Mock<ILoggerManager>();

            var mockReportServiceObject = reportService.Object;
            var mockLoggerManagereObject = loggerManager.Object;

            reportController = new ReportController(mockLoggerManagereObject, mockReportServiceObject);
        }

        [Fact]
        public async void GetUserCases_IsValidCase_CallGetReportOnce()
        {
            int managerId = It.IsAny<int>();
            List<ReportDto> reportListResult = new List<ReportDto>() { new ReportDto() { Name = It.IsAny<string>(), Month = It.IsAny<string>(), NumOfCases = It.IsAny<int>() } };
            reportService.Setup(a => a.GetReport(managerId)).ReturnsAsync(reportListResult);

            var result = await reportController.GetReport(managerId);

            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            reportService.Verify(a => a.GetReport(managerId), Times.Once);
        }
    }
}

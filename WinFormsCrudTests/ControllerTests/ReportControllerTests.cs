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
        private readonly Mock<IServiceManager> serviceManager;
        private readonly Mock<ILoggerManager> loggerManager;
        private ReportController reportController;

        public ReportControllerTests()
        {
            loggerManager = new Mock<ILoggerManager>();
            serviceManager = new Mock<IServiceManager>();
            serviceManager.Setup(a => a.ReportService).Returns(() => new Mock<IReportService>().Object);

            reportController = new ReportController(loggerManager.Object, serviceManager.Object);
        }

        [Fact]
        public async void GetUserCases_IsValidCase_CallGetReportOnce()
        {
            int managerId = It.IsAny<int>();
            List<ReportDto> reportListResult = new List<ReportDto>() { new ReportDto() { Name = It.IsAny<string>(), Month = It.IsAny<string>(), NumOfCases = It.IsAny<int>() } };
            serviceManager.Setup(a => a.ReportService.GetReport(managerId)).ReturnsAsync(reportListResult);

            var result = await reportController.GetReport(managerId);

            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            serviceManager.Verify(a => a.ReportService.GetReport(managerId), Times.Once);
        }
    }
}

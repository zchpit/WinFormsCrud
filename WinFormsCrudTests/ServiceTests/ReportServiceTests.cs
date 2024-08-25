using CommonLibrary.Dto;
using FluentAssertions;
using Moq;
using SimpleWebApi.IRepository;
using SimpleWebApi.IServices;
using SimpleWebApi.Services;
using System.Diagnostics.CodeAnalysis;

namespace SimpleWebApiTests.ServiceTests
{
    [ExcludeFromCodeCoverage]
    public class ReportServiceTests
    {
        IReportService reportService;
        private readonly Mock<IRepositoryWrapper> mockRepositoryWrapper;

        public ReportServiceTests()
        {
            mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            mockRepositoryWrapper.Setup(m => m.ReportRepository).Returns(() => new Mock<IReportRepository>().Object);

            reportService = new ReportService(mockRepositoryWrapper.Object);
        }

        [Fact]
        public async void IsValidCase_GetReport_ReturnReportResult()
        {
            int managerId = It.IsAny<int>();
            List<ReportDto> reportListResult = new List<ReportDto>() { new ReportDto() { Name = It.IsAny<string>(), Month = It.IsAny<string>(), NumOfCases = It.IsAny<int>() } };
            mockRepositoryWrapper.Setup(a => a.ReportRepository.GetReport(managerId)).ReturnsAsync(reportListResult);

            var result = await reportService.GetReport(managerId);

            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            mockRepositoryWrapper.Verify(a => a.ReportRepository.GetReport(managerId), Times.Once);
        }
    }
}
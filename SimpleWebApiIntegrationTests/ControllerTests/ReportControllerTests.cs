using CommonLibrary.Dto;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SimpleWebApi.IServices;
using SimpleWebApiIntegrationTests.Shared;
using SimpleWebApiIntegrationTests.TestServices;
using System.Diagnostics.CodeAnalysis;

namespace SimpleWebApiIntegrationTests.ControllerTests
{
    [ExcludeFromCodeCoverage]
    public class ReportControllerTests
    {
        private readonly TestApi _testApi;

        public ReportControllerTests()
        {
            _testApi = new TestApi(services => services.AddSingleton<IReportService, TestReportService>());
        }

        [Theory]
        [InlineData("/api/Report/GetReport/0")]
        public async Task GetReport_BadUserId_GetEmptyResponse(string url)
        {
            // Arrange
            var client = _testApi.Client;

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var result = await client.GetAsync(url);
            response.Content.Headers.ContentLength.Should().Be(2);
        }

        [Theory]
        [InlineData("/api/Report/GetReport/1")]
        public async Task GetReport_GoodUserId_GetUserDtoResponse(string url)
        {
            // Arrange
            var client = _testApi.Client;

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            response.Content.Headers.ContentLength.Should().BeGreaterThan(10);

            string responseBody = await response.Content.ReadAsStringAsync();
            List<ReportDto> reportDtos = null;
            try
            {
                reportDtos = JsonConvert.DeserializeObject<List<ReportDto>>(responseBody);
            }
            finally
            {
                reportDtos.Should().NotBeNull();
                reportDtos.Count().Should().BeGreaterThan(0);
            }
        }

    }
}

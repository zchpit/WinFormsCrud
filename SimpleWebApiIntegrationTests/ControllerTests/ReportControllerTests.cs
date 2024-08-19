using CommonLibrary.Consts;
using CommonLibrary.Dto;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SimpleWebApi;

namespace SimpleWebApiIntegrationTests.ControllerTests
{
    public class ReportControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ReportControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/Report/0")]
        public async Task GetReport_BadUserId_GetEmptyResponse(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var result = await client.GetAsync(url);
            response.Content.Headers.ContentLength.Should().Be(2);
        }

        [Theory]
        [InlineData("/Report/1")]
        public async Task GetReport_GoodUserId_GetUserDtoResponse(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

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

        [Theory]
        [InlineData("/Report/GetException")]
        public async Task GetException_MethodShouldThrowException_ThatIsHandledByMiddleware(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            string responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
            responseBody.Should().Contain(ConstStrings.MiddlewareExceptionText);
        }

        [Theory]
        [InlineData("/Report/GetAccessViolationException")]
        public async Task GetAccessViolationException_MethodShouldThrowException_ThatIsHandledByMiddleware(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            string responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
            responseBody.Should().Contain(ConstStrings.MiddlewareAccessViolationExceptionText);
        }
    }
}

using CommonLibrary.Consts;
using CommonLibrary.Dto;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SimpleWebApi;


namespace SimpleWebApiIntegrationTests.ControllerTests
{
    public class CaseControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CaseControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/Case/2/1")]
        public async Task GetUserCases_GoodUserId_GetCaseDtoResponse(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            response.Content.Headers.ContentLength.Should().BeGreaterThan(10);

            List<CaseDto> caseDtos = null;
            try
            {
                caseDtos = JsonConvert.DeserializeObject<List<CaseDto>>(responseBody);
            }
            finally 
            {
                caseDtos.Should().NotBeNull();
                caseDtos.Count().Should().BeGreaterThan(0);
            }
        }
    }
}

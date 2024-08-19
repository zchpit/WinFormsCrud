using CommonLibrary.Dto;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SimpleWebApi;
using System.Net.Http.Json;


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

        [Theory]
        [InlineData("/Case/2/1", "/Case?userId=2")]
        public async Task UpdateCase_SendRequestWithoutAnyChange_MakeSureApiMethodExists(string urlForTestCases, string urlForUpdate)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var responseGetCases = await client.GetAsync(urlForTestCases);
            string responseBodyGetCases = await responseGetCases.Content.ReadAsStringAsync();
            List<CaseDto> caseDtos = JsonConvert.DeserializeObject<List<CaseDto>>(responseBodyGetCases);
            CaseDto caseDtoToUpdate = caseDtos.First();

            var responsePost = await client.PostAsJsonAsync<CaseDto>(urlForUpdate, caseDtoToUpdate);
            string responseBodyPost = await responsePost.Content.ReadAsStringAsync();

            // Assert
            responsePost.EnsureSuccessStatusCode(); // Status Code 200-299
            responsePost.IsSuccessStatusCode.Should().BeTrue();
            responseBodyPost.Should().BeEmpty();
        }
    }
}

using CommonLibrary.Dto;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SimpleWebApi.IServices;
using SimpleWebApiIntegrationTests.Shared;
using SimpleWebApiIntegrationTests.TestServices;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;


namespace SimpleWebApiIntegrationTests.ControllerTests
{
    [ExcludeFromCodeCoverage]
    public class CaseControllerTests
    {
        private readonly TestApi _testApi;

        public CaseControllerTests()
        {
            _testApi = new TestApi(services => services.AddScoped<IUserService, TestUserService>());
        }

        [Theory]
        [InlineData("/Case/2/1")]
        public async Task GetUserCases_GoodUserId_GetCaseDtoResponse(string url)
        {
            // Arrange
            var client = _testApi.Client;

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
            var client = _testApi.Client;

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

        [Theory]
        [InlineData("/Case?userId=2")]
        public async Task UpdateCase_CheckValidation_BadRequest(string urlForUpdate)
        {
            // Arrange
            var client = _testApi.Client;

            // Act
            CaseDto caseDtoToUpdate = new CaseDto() { Id = 2, Header = null, Description = null };

            var responsePost = await client.PostAsJsonAsync<CaseDto>(urlForUpdate, caseDtoToUpdate);
            string responseBodyPost = await responsePost.Content.ReadAsStringAsync();

            // Assert
            responsePost.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            responsePost.IsSuccessStatusCode.Should().BeFalse();
            responseBodyPost.Should().Contain("One or more validation errors occurred.");
            responseBodyPost.Should().Contain("The Header field is required.");

        }
    }
}

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
            _testApi = new TestApi(services => services.AddSingleton<ICaseService, TestCaseService>());
        }

        [Theory]
        [InlineData("/api/Case/GetUserCases/2/1")]
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
        [InlineData("/api/Case/UpdateCase")]
        public async Task UpdateCase_SendRequestWithoutAnyChange_GetStatusCodeOk(string urlForUpdate)
        {
            // Arrange
            var client = _testApi.Client;
            CaseUpdateDto uaseUpdateDto = new CaseUpdateDto() { Id = 1, LastModifiedBy = 1, LastModifiedDate = DateTime.UtcNow };

            var responsePost = await client.PutAsJsonAsync<CaseUpdateDto>(urlForUpdate, uaseUpdateDto);

            // Assert
            responsePost.EnsureSuccessStatusCode(); // Status Code 200-299
            responsePost.IsSuccessStatusCode.Should().BeTrue();
        }

        [Theory]
        [InlineData("/api/Case/CreateCase")]
        public async Task CreateCase_CheckIfCreateMethodExists_GetStatusCodeOk(string urlForUpdate)
        {
            // Arrange
            var client = _testApi.Client;
            CaseCreateDto caseCreateDto = new CaseCreateDto()
            {
                Header = "test",
                CreateDate = DateTime.UtcNow,
                CreatedBy = 1,
                Description = "test",
                LastModifiedBy = 1,
                LastModifiedDate = DateTime.UtcNow,
                Priority = 1
            };

            // Act
            var responsePost = await client.PostAsJsonAsync<CaseCreateDto>(urlForUpdate, caseCreateDto);

            // Assert
            responsePost.EnsureSuccessStatusCode();
            responsePost.IsSuccessStatusCode.Should().BeTrue();
        }

        [Theory]
        [InlineData("/api/Case/DeleteCase/1/1")]
        public async Task DeleteCase_CheckValidation_CaseDeleted(string urlForUpdate)
        {
            // Arrange
            var client = _testApi.Client;

            // Act
            var responsePost = await client.DeleteAsync(urlForUpdate);

            // Assert
            responsePost.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            responsePost.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}

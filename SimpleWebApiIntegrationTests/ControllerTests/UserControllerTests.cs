using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebApi.IServices;
using SimpleWebApiIntegrationTests.Shared;
using SimpleWebApiIntegrationTests.TestServices;
using System.Diagnostics.CodeAnalysis;

namespace SimpleWebApiIntegrationTests.ControllerTests
{
    [ExcludeFromCodeCoverage]
    public class UserControllerTests
    {
        private readonly TestApi _testApi;

        public UserControllerTests()
        {
            _testApi = new TestApi(services => services.AddSingleton<IUserService, TestUserService>());
        }

        [Theory]
        [InlineData("/api/User/Login/test/test")]
        public async Task GetUserLogin_WrongUserPassword_GetEmptyResponse(string url)
        {
            // Arrange
            var client = _testApi.Client;

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().BeOneOf(System.Net.HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData("/api/User/Login/dGVzdA==/dGVzdA==")]
        [InlineData("/api/User/Login/bWFuYWdlcg==/bWFuYWdlcg==")]
        public async Task GetUserLogin_GoodUserPassword_GetUserDtoResponse(string url)
        {
            // Arrange
            var client = _testApi.Client;

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            response.IsSuccessStatusCode.Should().BeTrue();
            response.Content.Headers.ContentLength.Should().NotBe(0);
        }
    }
}

using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebApi.IServices;
using SimpleWebApiIntegrationTests.Shared;
using SimpleWebApiIntegrationTests.TestServices;

namespace SimpleWebApiIntegrationTests.ControllerTests
{
    public class UserControllerTests
    {
        private readonly TestApi _testApi;

        public UserControllerTests()
        {
            _testApi = new TestApi(services => services.AddScoped<IUserService, TestUserService>());
        }

        [Theory]
        [InlineData("/User/test/test")]
        public async Task GetUserLogin_WrongUserPassword_GetEmptyResponse(string url)
        {
            // Arrange
            var client = _testApi.Client;

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            response.Content.Headers.ContentLength.Should().Be(0);
        }

        [Theory]
        [InlineData("/User/dGVzdA==/dGVzdA==")]
        [InlineData("/User/bWFuYWdlcg==/bWFuYWdlcg==")]
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

using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SimpleWebApi;

namespace SimpleWebApiIntegrationTests.ControllerTests
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UserControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/User/test/test")]
        public async Task GetUserLogin_WrongUserPassword_GetEmptyResponse(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

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
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            response.IsSuccessStatusCode.Should().BeTrue();
            response.Content.Headers.ContentLength.Should().NotBe(0);
        }
    }
}

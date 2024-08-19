using CommonLibrary.Consts;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SimpleWebApi;

namespace SimpleWebApiIntegrationTests.ControllerTests
{
    public class ExceptionTestControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ExceptionTestControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/ExceptionTest/GetException")]
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
        [InlineData("/ExceptionTest/GetAccessViolationException")]
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

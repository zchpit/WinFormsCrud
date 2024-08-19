using CommonLibrary.Consts;
using FluentAssertions;
using SimpleWebApiIntegrationTests.Shared;

namespace SimpleWebApiIntegrationTests.ControllerTests
{
    public class ExceptionTestControllerTests
    {
        private readonly TestApi _testApi;

        public ExceptionTestControllerTests()
        {
            _testApi = new TestApi();
        }

        [Theory]
        [InlineData("/ExceptionTest/GetException")]
        public async Task GetException_MethodShouldThrowException_ThatIsHandledByMiddleware(string url)
        {
            // Arrange
            var client = _testApi.Client;

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
            var client = _testApi.Client;

            // Act
            var response = await client.GetAsync(url);

            string responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
            responseBody.Should().Contain(ConstStrings.MiddlewareAccessViolationExceptionText);
        }
    }
}

using CommonLibrary.Dto;
using FluentAssertions;
using Moq;
using SimpleWebApi.Controllers;
using SimpleWebApi.Helpers;
using SimpleWebApi.IServices;
using System.Diagnostics.CodeAnalysis;

namespace SimpleWebApiTests.ControllerTests
{
    [ExcludeFromCodeCoverage]
    public class UserControllerTests
    {
        private readonly Mock<IServiceManager> serviceManager;
        private readonly Mock<ILoggerManager> loggerManager;
        private UserController userController;

        public UserControllerTests()
        {
            loggerManager = new Mock<ILoggerManager>();
            serviceManager = new Mock<IServiceManager>();
            serviceManager.Setup(a => a.UserService).Returns(() => new Mock<IUserService>().Object);

            userController = new UserController(loggerManager.Object, serviceManager.Object);
        }

        [Fact]
        public async void Login_IsValidCase_CallLoginOnce()
        {
            var simpleUserDto = It.IsAny<SimpleUserDto>();

            serviceManager.Setup(a => a.UserService.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(simpleUserDto);

            var result = await userController.Login(It.IsAny<string>(), It.IsAny<string>());

            result.Should().NotBeNull();
            serviceManager.Verify(a => a.UserService.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            loggerManager.Verify(a => a.LogInfo(It.IsAny<string>()), Times.Once);
        }
    }
}

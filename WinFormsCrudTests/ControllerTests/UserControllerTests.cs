using CommonLibrary.Dto;
using FluentAssertions;
using Moq;
using SimpleWebApi.Controllers;
using SimpleWebApi.Helpers;
using SimpleWebApi.Interface;

namespace SimpleWebApiTests.ControllerTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> userService;
        private readonly Mock<ILoggerManager> loggerManager;
        private UserController userController;

        public UserControllerTests()
        {
            userService = new Mock<IUserService>();
            loggerManager = new Mock<ILoggerManager>();

            var mockUserServiceObject = userService.Object;
            var mockLoggerManagereObject = loggerManager.Object;

            userController = new UserController(mockLoggerManagereObject, mockUserServiceObject);
        }

        [Fact]
        public async void Login_IsValidCase_CallLoginOnce()
        {
            var simpleUserDto = It.IsAny<SimpleUserDto>();
            
            userService.Setup(a => a.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(simpleUserDto);

            var result = await userController.Login(It.IsAny<string>(), It.IsAny<string>());

            result.Should().NotBeNull();
            userService.Verify(a => a.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            loggerManager.Verify(a => a.LogInfo(It.IsAny<string>()), Times.Once);
        }
    }
}

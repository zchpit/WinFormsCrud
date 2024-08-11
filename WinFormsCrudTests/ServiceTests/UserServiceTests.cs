using FluentAssertions;
using Moq;
using SimpleWebApi.Dto;
using SimpleWebApi.Interface;
using SimpleWebApi.IRepository;
using SimpleWebApi.Services;
using SimpleWebApi.Strategy;

namespace WinFormsCrudTests.ServiceTests
{
    public class UserServiceTests
    {
        IUserService userService;
        private readonly Mock<IEncryptStrategy> mockEncryptStrategy;
        private readonly Mock<IUserRepository> mockUserRepository;

        public UserServiceTests()
        {
            mockEncryptStrategy = new Mock<IEncryptStrategy>();
            mockUserRepository = new Mock<IUserRepository>();

            var mockEncryptStrategyObject = mockEncryptStrategy.Object;
            var mockUserRepositoryObject = mockUserRepository.Object;

            userService = new UserService(mockEncryptStrategyObject, mockUserRepositoryObject);
        }

        [Fact]
        public void IsUserValid_Empty_ReturnFalse()
        {
            string empty = string.Empty;

            var result = userService.IsUserValid(empty);

            result.Should().BeFalse();
        }

        [Fact]
        public void IsUserValid_HaveValue_ReturnTrue()
        {
            string empty = "test";

            var result = userService.IsUserValid(empty);

            result.Should().BeTrue();
        }

        [Fact]
        public void IsPasswordValid_Empty_ReturnFalse()
        {
            string empty = string.Empty;

            var result = userService.IsPasswordValid(empty);

            result.Should().BeFalse();
        }

        [Fact]
        public void IsPasswordValid_HaveLessThan5_ReturnFalse()
        {
            string empty = "ta";

            var result = userService.IsPasswordValid(empty);

            result.Should().BeFalse();
        }

        [Fact]
        public void IsPasswordValid_HaveMoreThan5_ReturnTrue()
        {
            string empty = "testsasasa";

            var result = userService.IsPasswordValid(empty);

            result.Should().BeTrue();
        }

        [Fact]
        public void Login_UserPasswordCorrect_ReturnSimleUserDto()
        {
            string username = "goodUserName";
            string password =  "goodPassword";
            string encryptedPassword = "encryptedPassword";
            SimpleUserDto simpleUserDto = new SimpleUserDto() { Id = 1, UserRole = RoleDto.User };

            mockEncryptStrategy.Setup(a => a.Encrypt(password)).Returns(encryptedPassword);
            mockUserRepository.Setup(a => a.GetSimpleUserDto(username, encryptedPassword)).Returns(ValueTask.FromResult(simpleUserDto));

            var result = userService.Login(username, password);

            result.Should().NotBeNull();
            result.Result.Id.Should().Be(1);
            mockEncryptStrategy.Verify(a =>  a.Encrypt(password), Times.Once);
            mockUserRepository.Verify(a => a.GetSimpleUserDto(username, encryptedPassword), Times.Once);
        }

        [Fact]
        public void Login_UserPasswordInCorrect_ReturnNull()
        {
            string username = "goodUserName";
            string password = "wrongPassword";
            string encryptedPassword = "wrongPassword";
            SimpleUserDto simpleUserDto = null;

            mockEncryptStrategy.Setup(a => a.Encrypt(password)).Returns(encryptedPassword);
            mockUserRepository.Setup(a => a.GetSimpleUserDto(username, encryptedPassword)).Returns(ValueTask.FromResult(simpleUserDto));

            var result = userService.Login(username, password);

            result.Result.Should().BeNull();
            mockEncryptStrategy.Verify(a => a.Encrypt(password), Times.Once);
            mockUserRepository.Verify(a => a.GetSimpleUserDto(username, encryptedPassword), Times.Once);
        }
    }
}

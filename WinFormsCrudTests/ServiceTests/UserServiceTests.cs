using CommonLibrary.Dto;
using CommonLibrary.Strategy;
using FluentAssertions;
using Moq;
using SimpleWebApi.IRepository;
using SimpleWebApi.IServices;
using SimpleWebApi.Services;
using System.Diagnostics.CodeAnalysis;

namespace WinFormsCrudTests.ServiceTests
{
    [ExcludeFromCodeCoverage]
    public class UserServiceTests
    {
        IUserService userService;
        private readonly Mock<IEncryptStrategy> mockEncryptStrategy;
        private readonly Mock<ITransferStrategy> mockTransferStrategy;
        private readonly Mock<IRepositoryWrapper> mockRepositoryWrapper;

        public UserServiceTests()
        {
            mockEncryptStrategy = new Mock<IEncryptStrategy>();
            mockTransferStrategy = new Mock<ITransferStrategy>();
            mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            mockRepositoryWrapper.Setup(m => m.UserRepository).Returns(() => new Mock<IUserRepository>().Object);

            userService = new UserService(mockRepositoryWrapper.Object, mockEncryptStrategy.Object, mockTransferStrategy.Object);
        }

        [Fact]
        public void Login_UserPasswordCorrect_ReturnSimleUserDto()
        {
            string username = "goodUserName";
            string password = "goodPassword";
            string encryptedPassword = "encryptedPassword";
            SimpleUserDto simpleUserDto = new SimpleUserDto() { Id = 1, UserRole = RoleDto.User };

            mockEncryptStrategy.Setup(a => a.Encrypt(password)).Returns(encryptedPassword);
            mockTransferStrategy.Setup(a => a.Decrypt(username)).Returns(username);
            mockTransferStrategy.Setup(a => a.Decrypt(password)).Returns(password);

            mockRepositoryWrapper.Setup(a => a.UserRepository.GetSimpleUserDto(username, encryptedPassword)).Returns(ValueTask.FromResult(simpleUserDto));

            var result = userService.Login(username, password);

            result.Should().NotBeNull();
            result.Result.Id.Should().Be(1);
            mockEncryptStrategy.Verify(a => a.Encrypt(password), Times.Once);
            mockRepositoryWrapper.Verify(a => a.UserRepository.GetSimpleUserDto(username, encryptedPassword), Times.Once);
        }

        [Fact]
        public void Login_UserPasswordInCorrect_ReturnNull()
        {
            string username = "goodUserName";
            string password = "wrongPassword";
            string encryptedPassword = "wrongPassword";
            SimpleUserDto simpleUserDto = null;

            mockEncryptStrategy.Setup(a => a.Encrypt(password)).Returns(encryptedPassword);
            mockTransferStrategy.Setup(a => a.Decrypt(username)).Returns(username);
            mockTransferStrategy.Setup(a => a.Decrypt(password)).Returns(password);
            mockRepositoryWrapper.Setup(a => a.UserRepository.GetSimpleUserDto(username, encryptedPassword)).Returns(ValueTask.FromResult(simpleUserDto));

            var result = userService.Login(username, password);

            result.Result.Should().BeNull();
            mockEncryptStrategy.Verify(a => a.Encrypt(password), Times.Once);
            mockRepositoryWrapper.Verify(a => a.UserRepository.GetSimpleUserDto(username, encryptedPassword), Times.Once);
        }
    }
}

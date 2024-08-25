using CommonLibrary.Dto;
using CommonLibrary.Strategy;
using FluentAssertions;
using Microsoft.VisualBasic.ApplicationServices;
using Moq;
using SimpleWebApi.IRepository;
using SimpleWebApi.IServices;
using SimpleWebApi.Model;
using SimpleWebApi.Services;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

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
        public async void Login_UserPasswordCorrect_ReturnSimleUserDto()
        {
            string username = "goodUserName";
            string password = "goodPassword";
            string encryptedPassword = "encryptedPassword";
            SimpleUserDto simpleUserDto = new SimpleUserDto() { };
            SimpleWebApi.Model.User user = new SimpleWebApi.Model.User() { Id = 1, UserRole = RoleDto.User, IsActive = true, Name = username, Password = encryptedPassword };

            mockEncryptStrategy.Setup(a => a.Encrypt(password)).Returns(encryptedPassword);
            mockTransferStrategy.Setup(a => a.Decrypt(username)).Returns(username);
            mockTransferStrategy.Setup(a => a.Decrypt(password)).Returns(password);
            mockRepositoryWrapper.Setup(a =>  a.UserRepository.GetFirstWithNoTracking(It.IsAny<Expression<Func<SimpleWebApi.Model.User, bool>>>())).ReturnsAsync(user);

            SimpleUserDto result = await userService.Login(username, password);

            result.Should().NotBeNull();
            result.Id.Should().Be(user.Id);
            result.UserRole.Should().Be(user.UserRole);

            mockEncryptStrategy.Verify(a => a.Encrypt(password), Times.Once);
            mockRepositoryWrapper.Verify(a => a.UserRepository.GetFirstWithNoTracking(It.IsAny<Expression<Func<SimpleWebApi.Model.User, bool>>>()), Times.Once);
        }

        [Fact]
        public void Login_UserPasswordInCorrect_ReturnNull()
        {
            string username = "goodUserName";
            string password = "wrongPassword";
            string encryptedPassword = "wrongPassword";
            SimpleUserDto simpleUserDto = null;
            SimpleWebApi.Model.User user = null;

            mockEncryptStrategy.Setup(a => a.Encrypt(password)).Returns(encryptedPassword);
            mockTransferStrategy.Setup(a => a.Decrypt(username)).Returns(username);
            mockTransferStrategy.Setup(a => a.Decrypt(password)).Returns(password);
            mockRepositoryWrapper.Setup(a => a.UserRepository.GetFirstWithNoTracking(It.IsAny<Expression<Func<SimpleWebApi.Model.User, bool>>>())).ReturnsAsync(user);

            var result = userService.Login(username, password);

            result.Result.Should().BeNull();
            mockEncryptStrategy.Verify(a => a.Encrypt(password), Times.Once);
            mockRepositoryWrapper.Verify(a => a.UserRepository.GetFirstWithNoTracking(It.IsAny<Expression<Func<SimpleWebApi.Model.User, bool>>>()), Times.Once);
        }

        [Fact]
        public void Login_UserValidationFails_ReturnNull()
        {
            string username = "";
            string password = "wrongPassword";
            string encryptedPassword = "wrongPassword";
            SimpleUserDto simpleUserDto = null;
            SimpleWebApi.Model.User user = null;

            mockEncryptStrategy.Setup(a => a.Encrypt(password)).Returns(encryptedPassword);
            mockTransferStrategy.Setup(a => a.Decrypt(username)).Returns(username);
            mockTransferStrategy.Setup(a => a.Decrypt(password)).Returns(password);
            mockRepositoryWrapper.Setup(a => a.UserRepository.GetFirstWithNoTracking(It.IsAny<Expression<Func<SimpleWebApi.Model.User, bool>>>())).ReturnsAsync(user);

            var result = userService.Login(username, password);

            result.Result.Should().BeNull();
            mockEncryptStrategy.Verify(a => a.Encrypt(password), Times.Never);
            mockRepositoryWrapper.Verify(a => a.UserRepository.GetFirstWithNoTracking(It.IsAny<Expression<Func<SimpleWebApi.Model.User, bool>>>()), Times.Never);
        }

        [Fact]
        public void Login_PasswordValidationFails_ReturnNull()
        {
            string username = "goodUserName";
            string password = "";
            string encryptedPassword = "wrongPassword";
            SimpleUserDto simpleUserDto = null;
            SimpleWebApi.Model.User user = null;

            mockEncryptStrategy.Setup(a => a.Encrypt(password)).Returns(encryptedPassword);
            mockTransferStrategy.Setup(a => a.Decrypt(username)).Returns(username);
            mockTransferStrategy.Setup(a => a.Decrypt(password)).Returns(password);
            mockRepositoryWrapper.Setup(a => a.UserRepository.GetFirstWithNoTracking(It.IsAny<Expression<Func<SimpleWebApi.Model.User, bool>>>())).ReturnsAsync(user);

            var result = userService.Login(username, password);

            result.Result.Should().BeNull();
            mockEncryptStrategy.Verify(a => a.Encrypt(password), Times.Never);
            mockRepositoryWrapper.Verify(a => a.UserRepository.GetFirstWithNoTracking(It.IsAny<Expression<Func<SimpleWebApi.Model.User, bool>>>()), Times.Never);
        }
    }
}

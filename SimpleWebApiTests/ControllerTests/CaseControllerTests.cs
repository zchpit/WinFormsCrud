using CommonLibrary.Dto;
using Moq;
using SimpleWebApi.Controllers;
using SimpleWebApi.Helpers;
using SimpleWebApi.IServices;
using System.Diagnostics.CodeAnalysis;

namespace SimpleWebApiTests.ControllerTests
{
    [ExcludeFromCodeCoverage]
    public class CaseControllerTests
    {
        private readonly Mock<IServiceManager> serviceManager;
        private readonly Mock<ILoggerManager> loggerManager;
        private CaseController caseController;

        public CaseControllerTests()
        {
            loggerManager = new Mock<ILoggerManager>();
            serviceManager = new Mock<IServiceManager>();
            serviceManager.Setup(a => a.CaseService).Returns(() => new Mock<ICaseService>().Object);

            caseController = new CaseController(loggerManager.Object, serviceManager.Object);
        }

        [Fact]
        public async void GetUserCases_IsValidCase_CallGetUserCases()
        {
            RoleDto userRole = It.IsAny<RoleDto>();
            int id = It.IsAny<int>();
            SimpleUserDto simpleUserDto = It.IsAny<SimpleUserDto>();

            serviceManager.Setup(a => a.CaseService.GetUserCases(simpleUserDto));
            await caseController.GetUserCases(id, userRole);

            serviceManager.Verify(a => a.CaseService.GetUserCases(It.IsAny<SimpleUserDto>()), Times.Once);
        }

        [Fact]
        public async void CreateCase_IsValidCase_CallCreateCaseOnce()
        {
            CaseCreateDto caseCreateDto = It.IsAny<CaseCreateDto>();
            int user = It.IsAny<int>();

            serviceManager.Setup(a => a.CaseService.CreateCase(It.IsAny<CaseCreateDto>()));
            await caseController.CreateCase(caseCreateDto);

            serviceManager.Verify(a => a.CaseService.CreateCase(It.IsAny<CaseCreateDto>()), Times.Once);
        }

        [Fact]
        public async void UpdateCase_IsValidCase_CallUpdateCaseOnce()
        {
            CaseUpdateDto caseUpdateDto = It.IsAny<CaseUpdateDto>();
            int user = It.IsAny<int>();

            serviceManager.Setup(a => a.CaseService.UpdateCase(It.IsAny<CaseUpdateDto>()));
            await caseController.UpdateCase(caseUpdateDto);

            serviceManager.Verify(a => a.CaseService.UpdateCase(It.IsAny<CaseUpdateDto>()), Times.Once);
        }

        [Fact]
        public async void DeleteCase_IsValidCase_CallDeleteCaseOnce()
        {
            serviceManager.Setup(a => a.CaseService.DeleteCase(It.IsAny<CaseDeleteDto>()));
            await caseController.DeleteCase(It.IsAny<int>(), It.IsAny<int>());

            serviceManager.Verify(a => a.CaseService.DeleteCase(It.IsAny<CaseDeleteDto>()), Times.Once);
        }
    }
}

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
        public async void GetReport_IsValidCase_CallUpdateCaseOnce()
        {
            CaseDto caseDto = It.IsAny<CaseDto>();
            int user = It.IsAny<int>();

            serviceManager.Setup(a => a.CaseService.UpdateCase(It.IsAny<CaseDto>(), It.IsAny<int>()));
            await caseController.UpdateCase(user, caseDto);

            serviceManager.Verify(a => a.CaseService.UpdateCase(It.IsAny<CaseDto>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void GetUserCases_IsValidCase_CallGetUserCases()
        {
            int userRole = It.IsAny<int>();
            int id = It.IsAny<int>();
            SimpleUserDto simpleUserDto = It.IsAny<SimpleUserDto>();

            serviceManager.Setup(a => a.CaseService.GetUserCases(simpleUserDto));
            await caseController.GetUserCases(id, userRole);

            serviceManager.Verify(a => a.CaseService.GetUserCases(It.IsAny<SimpleUserDto>()), Times.Once);
        }
    }
}

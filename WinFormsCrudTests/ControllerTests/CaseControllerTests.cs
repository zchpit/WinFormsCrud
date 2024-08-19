using CommonLibrary.Dto;
using Moq;
using SimpleWebApi.Controllers;
using SimpleWebApi.Helpers;
using SimpleWebApi.IServices;

namespace SimpleWebApiTests.ControllerTests
{
    public class CaseControllerTests
    {
        private readonly Mock<ICaseService> caseService;
        private readonly Mock<ILoggerManager> loggerManager;
        private CaseController caseController;

        public CaseControllerTests()
        {
            caseService = new Mock<ICaseService>();
            loggerManager = new Mock<ILoggerManager>();

            var mockReportServiceObject = caseService.Object;
            var mockLoggerManagereObject = loggerManager.Object;

            caseController = new CaseController(mockLoggerManagereObject, mockReportServiceObject);
        }

        [Fact]
        public async void GetReport_IsValidCase_CallUpdateCaseOnce()
        {
            CaseDto caseDto = It.IsAny<CaseDto>();
            int user = It.IsAny<int>();

            caseService.Setup(a => a.UpdateCase(It.IsAny<CaseDto>(), It.IsAny<int>()));
            await caseController.UpdateCase(user, caseDto);

            caseService.Verify(a => a.UpdateCase(It.IsAny<CaseDto>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void GetUserCases_IsValidCase_CallGetUserCases()
        {
            int userRole = It.IsAny<int>();
            int id = It.IsAny<int>();
            SimpleUserDto simpleUserDto = It.IsAny<SimpleUserDto>();

            caseService.Setup(a => a.GetUserCases(simpleUserDto));
            await caseController.GetUserCases(id, userRole);

            caseService.Verify(a => a.GetUserCases(It.IsAny<SimpleUserDto>()), Times.Once);
        }
    }
}

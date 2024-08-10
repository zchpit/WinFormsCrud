using FluentAssertions;
using Moq;
using WinFormsCrud.Dto;
using WinFormsCrud.Interface;
using WinFormsCrud.IRepository;
using WinFormsCrud.Services;

namespace WinFormsCrudTests.ServiceTests
{
    public class CaseServiceTests
    {
        ICaseService caseService;
        private readonly Mock<ICaseRepository> mockCaseRepository;

        public CaseServiceTests()
        {
            mockCaseRepository = new Mock<ICaseRepository>();
            var mockUserRepositoryObject = mockCaseRepository.Object;
            caseService = new CaseService(mockUserRepositoryObject);
        }

        [Fact]
        public void GetUserCases_GetCase_MakeSureThatRepoWasCalled()
        {
            int userId = 1;
            List<CaseDto> caseList = new List<CaseDto>();
            mockCaseRepository.Setup(a => a.GetUserCases(userId)).Returns(caseList);

            var result = caseService.GetUserCases(userId);

            result.Should().NotBeNull();
            mockCaseRepository.Verify(a => a.GetUserCases(userId), Times.Once);
        }

        [Fact]
        public void IsValidCase_HeaderEmpty_ReturnFalse()
        {
            CaseDto caseDto = new CaseDto() { Header = string.Empty, Description = "Description" };

            var result = caseService.IsValidCase(caseDto);

            result.Should().BeFalse();
        }

        [Fact]
        public void IsValidCase_HeaderNull_ReturnFalse()
        {
            CaseDto caseDto = new CaseDto() { Header = null, Description = "Description" };

            var result = caseService.IsValidCase(caseDto);

            result.Should().BeFalse();
        }

        [Fact]
        public void IsValidCase_DescriptionEmpty_ReturnFalse()
        {
            CaseDto caseDto = new CaseDto() { Header = "Header", Description = string.Empty };

            var result = caseService.IsValidCase(caseDto);

            result.Should().BeFalse();
        }

        [Fact]
        public void IsValidCase_DescriptionNull_ReturnFalse()
        {
            CaseDto caseDto = new CaseDto() { Header = "Header", Description = null };

            var result = caseService.IsValidCase(caseDto);

            result.Should().BeFalse();
        }

        [Fact]
        public void UpdateCase_NewCase_CreateRecord()
        {
            CaseDto caseDto = new CaseDto() { Header = "Header", Description = "Description", Id = 0 };
            int userId = 1;

            caseService.UpdateCase(caseDto, userId);

            mockCaseRepository.Verify(a => a.UpdateCase(caseDto, userId), Times.Never);
            mockCaseRepository.Verify(a => a.AddCase(caseDto, userId), Times.Once);
        }

        [Fact]
        public void UpdateCase_ExistingCase_UpdateRecord()
        {
            CaseDto caseDto = new CaseDto() { Header = "Header", Description = "Description", Id = 1 };
            int userId = 1;

            caseService.UpdateCase(caseDto, userId);

            mockCaseRepository.Verify(a => a.UpdateCase(caseDto, userId), Times.Once);
            mockCaseRepository.Verify(a => a.AddCase(caseDto, userId), Times.Never);
        }

        [Fact]
        public void UpdateCase_UserUnknown_DontUpdate()
        {
            CaseDto caseDto = new CaseDto() { Header = "Header", Description = "Description", Id = 1 };
            int userId = 0;

            caseService.UpdateCase(caseDto, userId);

            mockCaseRepository.Verify(a => a.UpdateCase(caseDto, userId), Times.Never);
            mockCaseRepository.Verify(a => a.AddCase(caseDto, userId), Times.Never);
        }
    }
}

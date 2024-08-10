using AutoMapper;
using FluentAssertions;
using Moq;
using WinFormsCrud.Dto;
using WinFormsCrud.Helpers;
using WinFormsCrud.Interface;
using WinFormsCrud.IRepository;
using WinFormsCrud.Model;
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
            Mapper mapper = MapperConfig.InitializeAutomapper();

            caseService = new CaseService(mockUserRepositoryObject, mapper);
        }

        [Fact]
        public void GetUserCases_GetCase_MakeSureThatRepoWasCalled()
        {
            SimpleUserDto simpleUserDto = new SimpleUserDto();
            List<Case> caseList = new List<Case>();
            mockCaseRepository.Setup(a => a.GetUserCases(simpleUserDto)).Returns(caseList);

            var result = caseService.GetUserCases(simpleUserDto);

            result.Should().NotBeNull();
            mockCaseRepository.Verify(a => a.GetUserCases(simpleUserDto), Times.Once);
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
        public async void UpdateCase_NewCase_CreateRecord()
        {
            CaseDto caseDto = new CaseDto() { Header = "Header", Description = "Description", Id = 0 };
            int userId = 1;

            await caseService.UpdateCase(caseDto, userId);

            mockCaseRepository.Verify(a => a.UpdateCase(It.IsAny<Case>(), It.IsAny<int>()), Times.Never);
            mockCaseRepository.Verify(a => a.AddCase(It.IsAny<Case>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void UpdateCase_ExistingCase_UpdateRecord()
        {
            CaseDto caseDto = new CaseDto() { Header = "Header", Description = "Description", Id = 1 };
            int userId = 1;

            //mockMapper.Setup(a => a.)

            await caseService.UpdateCase(caseDto, userId);

            mockCaseRepository.Verify(a => a.UpdateCase(It.IsAny<Case>(), It.IsAny<int>()), Times.Once);
            mockCaseRepository.Verify(a => a.AddCase(It.IsAny<Case>(),  It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async void UpdateCase_UserUnknown_DontUpdate()
        {
            CaseDto caseDto = new CaseDto() { Header = "Header", Description = "Description", Id = 1 };
            int userId = 0;

            await caseService.UpdateCase(caseDto, userId);

            mockCaseRepository.Verify(a => a.UpdateCase(It.IsAny<Case>(), It.IsAny<int>()), Times.Never);
            mockCaseRepository.Verify(a => a.AddCase(It.IsAny<Case>(), It.IsAny<int>()), Times.Never);
        }
    }
}

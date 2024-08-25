using AutoMapper;
using CommonLibrary.Dto;
using FluentAssertions;
using Moq;
using SimpleWebApi.Helpers;
using SimpleWebApi.IRepository;
using SimpleWebApi.IServices;
using SimpleWebApi.Model;
using SimpleWebApi.Services;
using System.Diagnostics.CodeAnalysis;

namespace WinFormsCrudTests.ServiceTests
{
    [ExcludeFromCodeCoverage]
    public class CaseServiceTests
    {
        ICaseService caseService;
        private readonly Mock<IRepositoryWrapper> mockRepositoryWrapper;

        public CaseServiceTests()
        {
            mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            mockRepositoryWrapper.Setup(m => m.CaseRepository).Returns(() => new Mock<ICaseRepository>().Object);

            Mapper mapper = MapperConfig.InitializeAutomapper();

            caseService = new CaseService(mockRepositoryWrapper.Object, mapper);
        }

        [Fact]
        public void GetUserCases_GetCase_MakeSureThatRepoWasCalled()
        {
            var caseList = ValueTask.FromResult(It.IsAny<List<Case>>());
            mockRepositoryWrapper.Setup(a => a.CaseRepository.GetUserCases(It.IsAny<SimpleUserDto>())).Returns(caseList);

            var result = caseService.GetUserCases(It.IsAny<SimpleUserDto>());

            result.Should().NotBeNull();
            mockRepositoryWrapper.Verify(a => a.CaseRepository.GetUserCases(It.IsAny<SimpleUserDto>()), Times.Once);
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
            Case caseObj = new Case() { Header = "Header", Description = "Description", Id = 0 };
            int userId = 1;
            mockRepositoryWrapper.Setup(a => a.CaseRepository.GetFirstWithTracking(a => a.Id == caseDto.Id)).Returns(ValueTask.FromResult(caseObj));
            mockRepositoryWrapper.Setup(a => a.CaseRepository.Create(It.IsAny<Case>()));

            await caseService.UpdateCase(caseDto, userId);

            mockRepositoryWrapper.Verify(a => a.CaseRepository.GetFirstWithTracking(a => a.Id == caseDto.Id), Times.Never);
            mockRepositoryWrapper.Verify(a => a.CaseRepository.Create(It.IsAny<Case>()), Times.Once);
        }

        [Fact]
        public async void UpdateCase_ExistingCase_UpdateRecord()
        {
            CaseDto caseDto = new CaseDto() { Header = "Header", Description = "Description", Id = 1, Priority = 2, LastModifiedBy = 1, LastModifiedDate = new DateTime(2022, 01, 01), IsDeleted = false };
            Case caseObj = new Case() { Header = "", Description = "", Id = 1 };
            int userId = 1;

            mockRepositoryWrapper.Setup(a => a.CaseRepository.GetFirstWithTracking(a => a.Id == caseDto.Id)).Returns(ValueTask.FromResult(caseObj));
            mockRepositoryWrapper.Setup(a => a.CaseRepository.Create(It.IsAny<Case>()));

            await caseService.UpdateCase(caseDto, userId);

            mockRepositoryWrapper.Verify(a => a.CaseRepository.GetFirstWithTracking(a => a.Id == caseDto.Id), Times.Once);
            mockRepositoryWrapper.Verify(a => a.CaseRepository.Create(It.IsAny<Case>()), Times.Never);

            caseObj.Header.Should().Be(caseDto.Header);
            caseObj.Description.Should().Be(caseDto.Description);
            caseObj.Priority.Should().Be(caseDto.Priority);
            caseObj.LastModifiedBy.Should().Be(caseDto.LastModifiedBy);
            caseObj.LastModifiedDate.Should().Be(caseDto.LastModifiedDate);
            caseObj.IsDeleted.Should().Be(false);
            caseObj.DeletedDate.Should().BeNull();
            caseObj.DeletedBy.Should().BeNull();
        }

        [Fact]
        public async void UpdateCase_ExistingCase_DeleteRecord()
        {
            CaseDto caseDto = new CaseDto() { Header = "Header", Description = "Description", Id = 1, Priority = 2, LastModifiedBy = 1, LastModifiedDate = new DateTime(2022, 01, 01), IsDeleted = true, DeletedBy = 2, DeletedDate = new DateTime(2022, 01, 01) };
            Case caseObj = new Case() { Header = "", Description = "", Id = 1 };
            int userId = 1;

            mockRepositoryWrapper.Setup(a => a.CaseRepository.GetFirstWithTracking(a => a.Id == caseDto.Id)).Returns(ValueTask.FromResult(caseObj));
            mockRepositoryWrapper.Setup(a => a.CaseRepository.Create(It.IsAny<Case>()));

            await caseService.UpdateCase(caseDto, userId);

            mockRepositoryWrapper.Verify(a => a.CaseRepository.GetFirstWithTracking(a => a.Id == caseDto.Id), Times.Once);
            mockRepositoryWrapper.Verify(a => a.CaseRepository.Create(It.IsAny<Case>()), Times.Never);

            caseObj.Header.Should().Be(caseDto.Header);
            caseObj.Description.Should().Be(caseDto.Description);
            caseObj.Priority.Should().Be(caseDto.Priority);
            caseObj.LastModifiedBy.Should().Be(caseDto.LastModifiedBy);
            caseObj.LastModifiedDate.Should().Be(caseDto.LastModifiedDate);
            caseObj.IsDeleted.Should().Be(caseDto.IsDeleted);
            caseObj.DeletedDate.Should().Be(caseDto.DeletedDate);
            caseObj.DeletedBy.Should().Be(caseDto.DeletedBy);
        }

        [Fact]
        public async void UpdateCase_UserUnknown_DontUpdate()
        {
            CaseDto caseDto = new CaseDto() { Header = "Header", Description = "Description", Id = 1 };
            Case caseObj = new Case() { Header = "Header", Description = "Description", Id = 1 };
            int userId = 0;

            mockRepositoryWrapper.Setup(a => a.CaseRepository.GetFirstWithTracking(a => a.Id == caseDto.Id)).Returns(ValueTask.FromResult(caseObj));
            mockRepositoryWrapper.Setup(a => a.CaseRepository.Create(It.IsAny<Case>()));

            await caseService.UpdateCase(caseDto, userId);

            mockRepositoryWrapper.Verify(a => a.CaseRepository.GetFirstWithTracking(a => a.Id == caseDto.Id), Times.Never);
            mockRepositoryWrapper.Verify(a => a.CaseRepository.Create(It.IsAny<Case>()), Times.Never);
        }
    }
}

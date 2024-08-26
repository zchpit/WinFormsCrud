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
using System.Linq.Expressions;

namespace SimpleWebApiTests.ServiceTests
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
        public async void CreateCase_NewCase_CreateRecord()
        {
            CaseCreateDto caseCreateDto = new CaseCreateDto() 
            { 
                Header = "Header", 
                Description = "Description", 
                Priority = 1, 
                CreateDate = new DateTime(2024,01,01), 
                CreatedBy = 1, 
                LastModifiedBy = 1, 
                LastModifiedDate = new DateTime(2024, 01, 01) 
            };
            
            Case caseObj = new Case() { Header = "Header", Description = "Description", Id = 0 };
            mockRepositoryWrapper.Setup(a => a.CaseRepository.GetFirstWithTracking(It.IsAny<Expression<Func<Case, bool>>>())).Returns(ValueTask.FromResult(caseObj));
            mockRepositoryWrapper.Setup(a => a.CaseRepository.Create(It.IsAny<Case>()));

            await caseService.CreateCase(caseCreateDto);

            mockRepositoryWrapper.Verify(a => a.CaseRepository.GetFirstWithTracking(It.IsAny<Expression<Func<Case, bool>>>()), Times.Never);
            mockRepositoryWrapper.Verify(a => a.CaseRepository.Create(It.IsAny<Case>()), Times.Once);
        }

        [Fact]
        public async void UpdateCase_ExistingCase_UpdateRecord()
        {
            CaseUpdateDto caseUpdateDto = new CaseUpdateDto() { Header = "Header", Description = "Description", Id = 1, Priority = 2, LastModifiedBy = 1, LastModifiedDate = new DateTime(2022, 01, 01)};
            Case caseObj = new Case() { Header = "", Description = "", Id = 1 };

            mockRepositoryWrapper.Setup(a => a.CaseRepository.GetFirstWithTracking(It.IsAny<Expression<Func<Case, bool>>>())).Returns(ValueTask.FromResult(caseObj));
            mockRepositoryWrapper.Setup(a => a.CaseRepository.Create(It.IsAny<Case>()));

            await caseService.UpdateCase(caseUpdateDto);

            mockRepositoryWrapper.Verify(a => a.CaseRepository.GetFirstWithTracking(It.IsAny<Expression<Func<Case, bool>>>()), Times.Once);
            mockRepositoryWrapper.Verify(a => a.CaseRepository.Create(It.IsAny<Case>()), Times.Never);

            caseObj.Header.Should().Be(caseUpdateDto.Header);
            caseObj.Description.Should().Be(caseUpdateDto.Description);
            caseObj.Priority.Should().Be(caseUpdateDto.Priority);
            caseObj.LastModifiedBy.Should().Be(caseUpdateDto.LastModifiedBy);
            caseObj.LastModifiedDate.Should().Be(caseUpdateDto.LastModifiedDate);
            caseObj.IsDeleted.Should().Be(false);
            caseObj.DeletedDate.Should().BeNull();
            caseObj.DeletedBy.Should().BeNull();
        }

        [Fact]
        public async void DeleteCase_ExistingCase_DeleteRecord()
        {
            CaseDeleteDto caseDeleteDto = new CaseDeleteDto() { Id = 1, DeletedBy = 2, DeletedDate = new DateTime(2022, 01, 01) };
            Case caseObj = new Case() { Header = "", Description = "", Id = 1 };

            mockRepositoryWrapper.Setup(a => a.CaseRepository.GetFirstWithTracking(It.IsAny<Expression<Func<Case, bool>>>())).ReturnsAsync(caseObj);
            mockRepositoryWrapper.Setup(a => a.CaseRepository.Create(It.IsAny<Case>()));

            await caseService.DeleteCase(caseDeleteDto);

            mockRepositoryWrapper.Verify(a => a.CaseRepository.GetFirstWithTracking(It.IsAny<Expression<Func<Case, bool>>>()), Times.Once);
            mockRepositoryWrapper.Verify(a => a.CaseRepository.Create(It.IsAny<Case>()), Times.Never);

            caseObj.IsDeleted.Should().Be(true);
            caseObj.DeletedDate.Should().Be(caseDeleteDto.DeletedDate);
            caseObj.DeletedBy.Should().Be(caseDeleteDto.DeletedBy);
            caseObj.LastModifiedDate.Should().Be(caseDeleteDto.DeletedDate);
            caseObj.LastModifiedBy.Should().Be(caseDeleteDto.DeletedBy);
        }

        [Fact]
        public async void UpdateCase_UserUnknown_DontUpdate()
        {
            CaseUpdateDto caseUpdateDto = new CaseUpdateDto() { Header = "Header", Description = "Description", Id = 1, Priority = 2, LastModifiedBy = 0, LastModifiedDate = new DateTime(2022, 01, 01) };
            Case caseObj = new Case() { Header = "Header", Description = "Description", Id = 1 };

            mockRepositoryWrapper.Setup(a => a.CaseRepository.GetFirstWithTracking(It.IsAny<Expression<Func<Case, bool>>>())).Returns(ValueTask.FromResult(caseObj));
            mockRepositoryWrapper.Setup(a => a.CaseRepository.Create(It.IsAny<Case>()));

            await caseService.UpdateCase(caseUpdateDto);

            mockRepositoryWrapper.Verify(a => a.CaseRepository.GetFirstWithTracking(It.IsAny<Expression<Func<Case, bool>>>()), Times.Never);
            mockRepositoryWrapper.Verify(a => a.CaseRepository.Create(It.IsAny<Case>()), Times.Never);
        }
    }
}

using AutoMapper;
using CommonLibrary.Dto;
using SimpleWebApi.IRepository;
using SimpleWebApi.IServices;
using SimpleWebApi.Model;

namespace SimpleWebApi.Services
{
    public class CaseService : ICaseService
    {
        private IRepositoryWrapper repository;
        IMapper mapper;

        public CaseService(IRepositoryWrapper repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public bool IsValidCase(CaseDto caseDto)
        {
            if (string.IsNullOrEmpty(caseDto.Header))
                return false;

            if (string.IsNullOrEmpty(caseDto.Description))
                return false;

            return true;
        }

        public async ValueTask CreateCase(CaseCreateDto caseCreateDto)
        {
            Case newCase = mapper.Map<Case>(caseCreateDto);
            newCase.UserCases = new List<UserCase>() { new UserCase() { UserId = caseCreateDto.CreatedBy } };

            repository.CaseRepository.Create(newCase);
            await repository.SaveAsync();
        }

        public async ValueTask UpdateCase(CaseUpdateDto caseUpdateDto)
        {
            if (caseUpdateDto?.LastModifiedBy == 0)
                return;

            var caseToUpdate = await repository.CaseRepository.GetFirstWithTracking(a => a.Id == caseUpdateDto.Id);
            if (caseUpdateDto?.Header != null)
            {
                caseToUpdate.Header = caseUpdateDto.Header;
            }
            if (caseUpdateDto?.Description != null)
            {
                caseToUpdate.Description = caseUpdateDto.Description;
            }
            if (caseUpdateDto?.Priority != null)
            {
                caseToUpdate.Priority = caseUpdateDto.Priority.Value;
            }
            if (caseUpdateDto?.LastModifiedBy != null)
            {
                caseToUpdate.LastModifiedBy = caseUpdateDto.LastModifiedBy;
            }

            if (caseUpdateDto?.LastModifiedDate != null)
            {
                caseToUpdate.LastModifiedDate = caseUpdateDto.LastModifiedDate;
            }

            await repository.SaveAsync();
        }

        public async ValueTask DeleteCase(CaseDeleteDto caseDeleteDto)
        {
            var caseToUpdate = await repository.CaseRepository.GetFirstWithTracking(a => a.Id == caseDeleteDto.Id);
            if (caseToUpdate != null)
            {
                caseToUpdate.IsDeleted = true;
                caseToUpdate.LastModifiedBy = caseDeleteDto.DeletedBy;
                caseToUpdate.LastModifiedDate = caseDeleteDto.DeletedDate;
                caseToUpdate.DeletedBy = caseDeleteDto.DeletedBy;
                caseToUpdate.DeletedDate = caseDeleteDto.DeletedDate;

                await repository.SaveAsync();
            }
        }

        public async ValueTask<List<CaseDto>> GetUserCases(SimpleUserDto simpleUserDto)
        {
            var tmpResult = await repository.CaseRepository.GetUserCases(simpleUserDto);
            var result = tmpResult.Select(a => MapCaseToCaseDto(a)).ToList();

            return result;
        }

        public CaseDto MapCaseToCaseDto(Case caseDto)
        {
            var result = mapper.Map<CaseDto>(caseDto);
            return result;
        }
    }
}

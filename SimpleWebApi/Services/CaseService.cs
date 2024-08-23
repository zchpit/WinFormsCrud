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

        public async Task UpdateCase(CaseDto caseDto, int userId)
        {
            if (userId > 0)
            {
                Case caseToUpdate = MapCaseDtoToCase(caseDto);
                if (caseDto.Id > 0)
                {

                    await repository.CaseRepository.UpdateCase(caseToUpdate, userId);
                }
                else
                {
                    await repository.CaseRepository.AddCase(caseToUpdate, userId);
                }
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

        public Case MapCaseDtoToCase(CaseDto caseDto)
        {
            var result = mapper.Map<Case>(caseDto);
            return result;
        }
    }
}

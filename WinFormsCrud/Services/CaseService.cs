using WinFormsCrud.Dto;
using WinFormsCrud.Interface;
using WinFormsCrud.IRepository;
using AutoMapper;
using WinFormsCrud.Model;

namespace WinFormsCrud.Services
{
    public class CaseService : ICaseService
    {
        ICaseRepository caseRepository;
        IMapper mapper;

        public CaseService(ICaseRepository caseRepository, IMapper mapper)
        {
            this.caseRepository = caseRepository;
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

                    await caseRepository.UpdateCase(caseToUpdate, userId);
                }
                else
                {
                    await caseRepository.AddCase(caseToUpdate, userId);
                }
            }
        }

        public async ValueTask<List<CaseDto>> GetUserCases(SimpleUserDto simpleUserDto)
        {
            var tmpResult = await caseRepository.GetUserCases(simpleUserDto);
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

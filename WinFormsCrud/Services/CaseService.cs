using WinFormsCrud.Dto;
using WinFormsCrud.Interface;
using WinFormsCrud.IRepository;

namespace WinFormsCrud.Services
{
    public class CaseService : ICaseService
    {
        ICaseRepository caseRepository;

        public CaseService(ICaseRepository caseRepository)
        {
            this.caseRepository = caseRepository;
        }

        public bool IsValidCase(CaseDto caseDto)
        {
            if (string.IsNullOrEmpty(caseDto.Header))
                return false;

            if (string.IsNullOrEmpty(caseDto.Description))
                return false;

            return true;
        }

        public void UpdateCase(CaseDto caseDto, int userId)
        {
            if (userId > 0)
            {
                if (caseDto.Id > 0)
                {
                    caseRepository.UpdateCase(caseDto, userId);
                }
                else
                {
                    caseRepository.AddCase(caseDto, userId);
                }
            }
        }

        public List<CaseDto> GetUserCases(int userId)
        {
            return caseRepository.GetUserCases(userId);
        }
    }
}

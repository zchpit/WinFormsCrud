using WinFormsCrud.Dto;
using WinFormsCrud.Interface;

namespace WinFormsCrud.Services
{
    public class CaseService : ICaseService
    {
        public CaseService()
        {
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
                //Case caseToUpdate = MapCaseDtoToCase(caseDto);
                if (caseDto.Id > 0)
                {

                    //await caseRepository.UpdateCase(caseToUpdate, userId);
                }
                else
                {
                    //await caseRepository.AddCase(caseToUpdate, userId);
                }
            }
        }

        public async ValueTask<List<CaseDto>> GetUserCases(SimpleUserDto simpleUserDto)
        {
            //var tmpResult = await caseRepository.GetUserCases(simpleUserDto);
            //var result = tmpResult.Select(a => MapCaseToCaseDto(a)).ToList();

            return null;
        }

    }
}

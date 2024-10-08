﻿using CommonLibrary.Dto;
using SimpleWebApi.IServices;
using System.Diagnostics.CodeAnalysis;

namespace SimpleWebApiIntegrationTests.TestServices
{
    [ExcludeFromCodeCoverage]
    public class TestCaseService : ICaseService
    {
        public ValueTask<List<CaseDto>> GetUserCases(SimpleUserDto simpleUserDto)
        {
            List<CaseDto> caseDtos = new List<CaseDto>();
            if (simpleUserDto.Id == 2 && simpleUserDto.UserRole == RoleDto.User)
            {
                caseDtos = new List<CaseDto>();
                caseDtos.Add(new CaseDto { Id = 1, CreateDate = new DateTime(2020, 02, 02), Description = "desc1", Header = "header", CreatedBy = 1, Priority = 1, LastModifiedBy = 1, LastModifiedDate = new DateTime(2020, 02, 02) });
                caseDtos.Add(new CaseDto { Id = 2, CreateDate = new DateTime(2022, 02, 02), Description = "desc2", Header = "header2", CreatedBy = 1, Priority = 2, LastModifiedBy = 1, LastModifiedDate = new DateTime(2022, 02, 02) });
                caseDtos.Add(new CaseDto { Id = 3, CreateDate = new DateTime(2024, 02, 02), Description = "desc3", Header = "header3", CreatedBy = 1, Priority = 3, LastModifiedBy = 1, LastModifiedDate = new DateTime(2024, 02, 02) });
            }

            return new ValueTask<List<CaseDto>>(caseDtos);
        }

        public async ValueTask CreateCase(CaseCreateDto caseCreateDto)
        {
            var test = caseCreateDto;
        }
        public async ValueTask UpdateCase(CaseUpdateDto caseUpdateDto)
        {
            var test = caseUpdateDto;
        }
        public async ValueTask DeleteCase(CaseDeleteDto caseDeleteDto)
        {
            var test = caseDeleteDto;
        }

        public bool IsValidCase(CaseDto caseDto)
        {
            if (caseDto != null && caseDto.Id == 1) { return true; }

            return false;
        }
    }
}

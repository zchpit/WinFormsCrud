using CommonLibrary.Dto;
using SimpleWebApi.Interface;

namespace SimpleWebApiIntegrationTests.TestServices
{
    internal class TestUserService : IUserService 
    {
        public TestUserService() 
        {

        }

        public async ValueTask<SimpleUserDto> Login(string encryptedUsername, string encryptedPassword)
        {
            if (encryptedUsername == "dGVzdA==" && encryptedPassword == "dGVzdA==")
                return new SimpleUserDto() { Id = 2, UserRole = RoleDto.User };

            if (encryptedUsername == "bWFuYWdlcg==" && encryptedPassword == "bWFuYWdlcg==")
                return new SimpleUserDto() { Id = 1, UserRole = RoleDto.Manager };

            return null;
        }

        public async Task UpdateCase(int userId, CaseDto caseDto)
        {
            if (userId == 2 && caseDto == null)
                return; 


        }
    }
}

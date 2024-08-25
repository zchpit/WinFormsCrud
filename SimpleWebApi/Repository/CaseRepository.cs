using CommonLibrary.Dto;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi.IRepository;
using SimpleWebApi.Model;

namespace SimpleWebApi.Repository
{
    public class CaseRepository : RepositoryBase<Case>, ICaseRepository
    {
        public CaseRepository(SimpleDbContext repositoryContext)
                    : base(repositoryContext)
        {
        }

        public async ValueTask<List<Case>> GetUserCases(SimpleUserDto simpleUserDto)
        {
            List<int> userToCheck = new List<int>();
            userToCheck.Add(simpleUserDto.Id);

            if (simpleUserDto.UserRole == RoleDto.Manager)
            {
                var managerUses = await repositoryContext.Users.Where(a => a.ManagerId == simpleUserDto.Id).Select(a => a.Id).ToListAsync();
                userToCheck.AddRange(managerUses);
            }

            var userCases = repositoryContext.UserCases.Include(a => a.Case).Where(a => userToCheck.Contains(a.UserId)).Select(a => a.CaseId);
            var result = await repositoryContext.Cases.Where(a => userCases.Contains(a.Id) && !a.IsDeleted).ToListAsync();

            return result;
        }
    }
}

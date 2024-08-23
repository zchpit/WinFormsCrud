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

        public async ValueTask<List<Case>> GetAllCases()
        {
            return await repositoryContext.Cases.Where(a => !a.IsDeleted).ToListAsync();
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

        public async Task AddCase(Case caseDto, int userId)
        {
            repositoryContext.Cases.Add(caseDto);
            await repositoryContext.SaveChangesAsync();

            UserCase newUserCase = new UserCase() { CaseId = caseDto.Id, UserId = userId };
            repositoryContext.UserCases.Add(newUserCase);

            await repositoryContext.SaveChangesAsync();
        }

        public async Task UpdateCase(Case caseDto, int userId)
        {
            var toUpdate = await repositoryContext.Cases.FirstOrDefaultAsync(x => x.Id == caseDto.Id);
            if (toUpdate != null)
            {
                toUpdate.Header = caseDto.Header;
                toUpdate.Description = caseDto.Description;
                toUpdate.Priority = caseDto.Priority;
                toUpdate.LastModifiedBy = caseDto.LastModifiedBy;
                toUpdate.LastModifiedDate = caseDto.LastModifiedDate;

                if (caseDto.IsDeleted)
                {
                    toUpdate.IsDeleted = caseDto.IsDeleted;
                    toUpdate.DeletedDate = caseDto.DeletedDate;
                    toUpdate.DeletedBy = caseDto.DeletedBy;
                }

                await repositoryContext.SaveChangesAsync();
            }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    repositoryContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

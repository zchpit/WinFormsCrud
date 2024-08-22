using CommonLibrary.Dto;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi.IRepository;
using SimpleWebApi.Model;

namespace SimpleWebApi.Repository
{
    public class CaseRepository : ICaseRepository
    {
        private SimpleDbContext caseContext;

        public CaseRepository(SimpleDbContext context)
        {
            this.caseContext = context;
        }

        public async ValueTask<List<Case>> GetAllCases()
        {
            return await caseContext.Cases.Where(a => !a.IsDeleted).ToListAsync();
        }

        public async ValueTask<List<Case>> GetUserCases(SimpleUserDto simpleUserDto)
        {
            List<int> userToCheck = new List<int>();
            userToCheck.Add(simpleUserDto.Id);

            if (simpleUserDto.UserRole == RoleDto.Manager)
            {
                var managerUses = await caseContext.Users.Where(a => a.ManagerId == simpleUserDto.Id).Select(a => a.Id).ToListAsync();
                userToCheck.AddRange(managerUses);
            }

            var userCases = caseContext.UserCases.Include(a => a.Case).Where(a => userToCheck.Contains(a.UserId)).Select(a => a.CaseId);
            var result = await caseContext.Cases.Where(a => userCases.Contains(a.Id) && !a.IsDeleted).ToListAsync();

            return result;
        }

        public async Task AddCase(Case caseDto, int userId)
        {
            caseContext.Cases.Add(caseDto);
            await caseContext.SaveChangesAsync();

            UserCase newUserCase = new UserCase() { CaseId = caseDto.Id, UserId = userId };
            caseContext.UserCases.Add(newUserCase);

            await caseContext.SaveChangesAsync();
        }

        public async Task UpdateCase(Case caseDto, int userId)
        {
            var toUpdate = await caseContext.Cases.FirstOrDefaultAsync(x => x.Id == caseDto.Id);
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

                await caseContext.SaveChangesAsync();
            }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    caseContext.Dispose();
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

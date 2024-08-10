using System.Data.Entity;
using WinFormsCrud.Dto;
using WinFormsCrud.IRepository;
using WinFormsCrud.Model;

namespace WinFormsCrud.Repository
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
            if (simpleUserDto.UserRole == RoleDto.Manager)
            {
                return await GetAllCases();
            }
            else
            {
                var toUpdate = await caseContext.Cases.Where(a => a.CreatedBy == simpleUserDto.Id).ToListAsync();
                return toUpdate;
            }
        }

        public async Task AddCase(Case caseDto, int userId)
        {
            caseContext.Cases.Add(caseDto);
            await caseContext.SaveChangesAsync();
        }

        public async Task UpdateCase(Case caseDto, int userId)
        {
            var toUpdate = await caseContext.Cases.FirstOrDefaultAsync(x => x.Id == caseDto.Id);
            if (toUpdate != null)
            {
                toUpdate.Description = caseDto.Description;
                toUpdate.Priority = caseDto.Priority;
                toUpdate.LastModifiedBy = caseDto.LastModifiedBy;
                toUpdate.LastModifiedDate = caseDto.LastModifiedDate;

                if(caseDto.IsDeleted)
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

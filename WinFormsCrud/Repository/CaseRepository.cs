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

        public List<Case> GetAllCases()
        {
            return caseContext.Cases.Where(a => !a.IsDeleted).ToList();
        }

        public List<Case> GetUserCases(SimpleUserDto simpleUserDto)
        {

            if (simpleUserDto.UserRole == RoleDto.Manager)
            {
                return GetAllCases();
            }
            else
            {
                var toUpdate = caseContext.Cases.Where(a => a.CreatedBy == simpleUserDto.Id).ToList();
                return toUpdate;
            }
        }

        public void AddCase(Case caseDto, int userId)
        {
            caseContext.Cases.Add(caseDto);
            caseContext.SaveChanges();
        }

        public void UpdateCase(Case caseDto, int userId)
        {
            var toUpdate = caseContext.Cases.FirstOrDefault(x => x.Id == caseDto.Id);
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
            }

            caseContext.SaveChanges();
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

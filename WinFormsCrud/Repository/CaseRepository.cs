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

        /*
        public List<CaseDto> caseDtos = new List<CaseDto>(){
               new CaseDto() { Id = 1, Header = "Case1", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", Priority = 1, CreateDate = new DateTime(2022, 02, 02) },
               new CaseDto() { Id = 2, Header = "Case2", Description = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", Priority = 2, CreateDate = new DateTime(2023, 10, 02) },
               new CaseDto() { Id = 3, Header = "Case3", Description = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur", Priority = 4, CreateDate = new DateTime(2024, 06, 02) },
               new CaseDto() { Id = 4, Header = "Case4", Description = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\"", Priority = 5, CreateDate = new DateTime(2021, 01, 04) }
        };*/

        public List<Case> GetCases()
        {
            return caseContext.Cases.Where(a => !a.IsDeleted).ToList();
        }

        public List<Case> GetUserCases(int userId)
        {
            //TODO: check with case beleongs to user/manager
            return GetCases();
        }

        public void AddCase(Case caseDto, int userId)
        {
            var maxNumber = caseContext.Cases.Select(a => a.Id).Max(a => a);
            caseDto.Id = maxNumber + 1;

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

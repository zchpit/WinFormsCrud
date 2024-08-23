using SimpleWebApi.IRepository;
using SimpleWebApi.Model;

namespace SimpleWebApi.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private SimpleDbContext repoContext;
        private ICaseRepository? caseRepository;
        private IReportRepository? reportRepository;
        private IUserRepository? userRepository;

        public ICaseRepository CaseRepository
        {
            get
            {
                if (caseRepository == null)
                {
                    caseRepository = new CaseRepository(repoContext);
                }
                return caseRepository;
            }
        }

        public IReportRepository ReportRepository
        {
            get
            {
                if (reportRepository == null)
                {
                    reportRepository = new ReportRepository(repoContext);
                }
                return reportRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(repoContext);
                }
                return userRepository;
            }
        }

        public RepositoryWrapper(SimpleDbContext repoContext)
        {
            this.repoContext = repoContext;
        }

        public void Save()
        {
            repoContext.SaveChanges();
        }

        public async ValueTask SaveAsync()
        {
            await repoContext.SaveChangesAsync();
        }
    }
}

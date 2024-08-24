using Microsoft.EntityFrameworkCore;
using SimpleWebApi.IRepository;
using SimpleWebApi.Model;

namespace SimpleWebApi.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper, IDisposable
    {
        private SimpleDbContext context;
        private ICaseRepository? caseRepository;
        private IReportRepository? reportRepository;
        private IUserRepository? userRepository;

        public ICaseRepository CaseRepository
        {
            get
            {
                if (caseRepository == null)
                {
                    caseRepository = new CaseRepository(context);
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
                    reportRepository = new ReportRepository(context);
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
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public RepositoryWrapper(SimpleDbContext context)
        {
            this.context = context;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async ValueTask SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}

using CommonLibrary.Dto;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi.IRepository;
using SimpleWebApi.Model;

namespace SimpleWebApi.Repository
{
    public class ReportRepository : RepositoryBase<ReportDto>, IReportRepository
    {
        public ReportRepository(SimpleDbContext repositoryContext)
                    : base(repositoryContext)
        {
        }

        public async ValueTask<List<ReportDto>> GetReport(int managerId)
        {
            var result = await repositoryContext.Database.SqlQuery<ReportDto>($"MonthlyNumOfCasesPerUserReport @ManagerId = {managerId}").ToListAsync();

            return result;
        }
    }
}

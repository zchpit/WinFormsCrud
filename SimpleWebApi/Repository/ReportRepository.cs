using CommonLibrary.Dto;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi.IRepository;
using SimpleWebApi.Model;

namespace SimpleWebApi.Repository
{
    public class ReportRepository : IReportRepository
    {
        private SimpleDbContext reportContext;

        public ReportRepository(SimpleDbContext context)
        {
            this.reportContext = context;
        }

        public async ValueTask<List<ReportDto>> GetReport(int managerId)
        {
            var result = await reportContext.Database.SqlQuery<ReportDto>($"MonthlyNumOfCasesPerUserReport @ManagerId = {managerId}").ToListAsync();

            return result;
        }
    }
}

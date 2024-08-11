using CommonLibrary.Dto;
using SimpleWebApi.Model;
using System.Data.SqlClient;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi.IRepository;

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
            var result = reportContext.Database.SqlQuery<ReportDto>($"SimpleReport @ManagerId = {managerId}").ToList();

            return result;
        }
    }
}

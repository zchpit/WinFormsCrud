using CommonLibrary.Dto;
using SimpleWebApi.IRepository;
using SimpleWebApi.IServices;

namespace SimpleWebApi.Services
{
    public class ReportService : IReportService
    {
        IRepositoryWrapper repository;

        public ReportService(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async ValueTask<List<ReportDto>> GetReport(int managerId)
        {
            var reports = await repository.ReportRepository.GetReport(managerId);

            return reports;
        }
    }
}

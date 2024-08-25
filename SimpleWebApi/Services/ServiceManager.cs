using AutoMapper;
using CommonLibrary.Strategy;
using SimpleWebApi.IRepository;
using SimpleWebApi.IServices;

namespace SimpleWebApi.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> lazyUserService;
        private readonly Lazy<ICaseService> lazyCaseService;
        private readonly Lazy<IReportService> lazyReportService;

        public ServiceManager(IRepositoryWrapper repositoryWrapper, IEncryptStrategy encryptStrategy, ITransferStrategy transferStrategy,IMapper mapper)
        {
            lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryWrapper, encryptStrategy, transferStrategy));
            lazyCaseService = new Lazy<ICaseService>(() => new CaseService(repositoryWrapper, mapper));
            lazyReportService = new Lazy<IReportService>(() => new ReportService(repositoryWrapper));
        }

        public IUserService UserService => lazyUserService.Value;
        public ICaseService CaseService => lazyCaseService.Value;
        public IReportService ReportService => lazyReportService.Value;
    }
}

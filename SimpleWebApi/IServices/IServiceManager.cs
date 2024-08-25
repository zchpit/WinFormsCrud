namespace SimpleWebApi.IServices
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        ICaseService CaseService { get; }
        IReportService ReportService { get; }

    }
}

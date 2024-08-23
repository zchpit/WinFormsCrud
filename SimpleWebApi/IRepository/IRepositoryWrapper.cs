namespace SimpleWebApi.IRepository
{
    public interface IRepositoryWrapper
    {
        ICaseRepository CaseRepository { get; }
        IReportRepository ReportRepository { get; }
        IUserRepository UserRepository { get; }

        void Save();

        ValueTask SaveAsync();
    }
}

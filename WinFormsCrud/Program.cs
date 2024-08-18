using CommonLibrary.Strategy;
using WinFormsCrud.Interface;
using WinFormsCrud.IServices;
using WinFormsCrud.Services;

namespace WinFormsCrud
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            ITransferStrategy transferStrategy = new Base64TransferStrategy();
            IUserService userService = new UserService(transferStrategy);
            ICaseService caseService = new CaseService();
            IReportService reportService = new ReportService();

            Application.Run(new SimpleTestForm(userService, caseService, reportService));
        }
    }
}
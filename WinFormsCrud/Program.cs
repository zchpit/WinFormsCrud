using CommonLibrary.Strategy;
using NLog;
using WinFormsCrud.Interface;
using WinFormsCrud.IServices;
using WinFormsCrud.Services;
using NLog;

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


            ILogger logger = LogManager.GetCurrentClassLogger();
            ITransferStrategy transferStrategy = new Base64TransferStrategy();
            IUserService userService = new UserService(transferStrategy, logger);
            ICaseService caseService = new CaseService(logger);
            IReportService reportService = new ReportService(logger);

            Application.Run(new SimpleTestForm(userService, caseService, reportService, logger));
        }
    }
}
using WinFormsCrud.Interface;
using WinFormsCrud.IRepository;
using WinFormsCrud.Repository;
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
            ICaseRepository caseRepository = new CaseRepository();
            ILoginService loginService = new LoginService();
            ICaseService caseService = new CaseService(caseRepository);

            Application.Run(new SimpleTestForm(loginService, caseService));
        }
    }
}
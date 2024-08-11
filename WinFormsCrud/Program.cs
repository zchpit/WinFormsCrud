using CommonLibrary.Strategy;
using WinFormsCrud.Interface;
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

            ITransferStrategy transferStrategy = new Base64EncryptStrategy();

            IUserService userService = new UserService(transferStrategy);
            ICaseService caseService = new CaseService();

            Application.Run(new SimpleTestForm(userService, caseService));
        }
    }
}
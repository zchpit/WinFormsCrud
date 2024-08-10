using WinFormsCrud.Interface;
using WinFormsCrud.IRepository;
using WinFormsCrud.Repository;
using WinFormsCrud.Services;
using WinFormsCrud.Strategy;

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
            IEncryptStrategy encryptStrategy = new Rfc2898EncryptStrategy();
            ICaseRepository caseRepository = new CaseRepository();
            IUserRepository userRepository = new UserRepository();
            IUserService userService = new UserService(encryptStrategy, userRepository);
            ICaseService caseService = new CaseService(caseRepository);

            Application.Run(new SimpleTestForm(userService, caseService));
        }
    }
}
using AutoMapper;
using WinFormsCrud.Helpers;
using WinFormsCrud.Interface;
using WinFormsCrud.IRepository;
using WinFormsCrud.Model;
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
            var dbContext = new SimpleDbContext();

            Mapper mapper = MapperConfig.InitializeAutomapper();
            IEncryptStrategy encryptStrategy = new Rfc2898EncryptStrategy();
            ICaseRepository caseRepository = new CaseRepository(dbContext);
            IUserRepository userRepository = new UserRepository(dbContext);
            IUserService userService = new UserService(encryptStrategy, userRepository);
            ICaseService caseService = new CaseService(caseRepository, mapper);

            Application.Run(new SimpleTestForm(userService, caseService));
        }
    }
}
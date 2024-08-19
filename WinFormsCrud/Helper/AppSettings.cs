using Microsoft.Extensions.Configuration;

namespace WinFormsCrud.Helper
{
    public class AppSettings
    {
        private static ConfigurationBuilder _configBuilder;

        public static ReportFileSettings ReportFileSettings()
        {
            Build();
            return InitOptions<ReportFileSettings>("ReportFileSettings");
        }

        private static IConfigurationRoot Build()
        {
            if (_configBuilder is not null) return _configBuilder.Build();
            _configBuilder = new ConfigurationBuilder();
            _configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            return _configBuilder.Build();
        }
        public static T InitOptions<T>(string section) where T : new()
            => Build().GetSection(section).Get<T>();
    }
}

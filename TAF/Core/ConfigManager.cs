using Microsoft.Extensions.Configuration;
using System.IO;

namespace TAF.Core
{
    public static class ConfigManager
    {
        private static IConfiguration _configuration;

        static ConfigManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public static string GetConfig(string key)
        {
            return _configuration[key];
        }
    }
}

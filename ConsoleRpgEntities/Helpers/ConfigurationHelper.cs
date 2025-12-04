using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ConsoleRpg.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static void ConfigureDbContextOptions(DbContextOptionsBuilder options, string connectionString)
        {
            options.UseSqlServer(connectionString);
        }
    }
}

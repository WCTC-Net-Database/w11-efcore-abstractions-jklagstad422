using ConsoleRpg.Helpers;
using ConsoleRpg.Services;
using ConsoleRpgEntities.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NReco.Logging.File;

namespace ConsoleRpg
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var configuration = ConfigurationHelper.GetConfiguration();

            var fileLoggerOptions = new FileLoggerOptions();
            configuration.GetSection("Logging:File").Bind(fileLoggerOptions);

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddProvider(new FileLoggerProvider("Logs/log.txt", fileLoggerOptions));
            });

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<GameContext>(options =>
            {
                ConfigurationHelper.ConfigureDbContextOptions(options, connectionString);
            });

            services.AddTransient<GameEngine>();
            services.AddTransient<MenuManager>();
            services.AddSingleton<OutputManager>();
        }
    }
}

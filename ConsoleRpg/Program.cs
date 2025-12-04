using ConsoleRpg.Services;
using ConsoleRpgEntities.Data;
using ConsoleRpg.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRpg
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var services = new ServiceCollection();
            Startup.ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            // Seed the database
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<GameContext>();
                SeedDatabase.EnsureSeeded(context);
            }

            // Start the game
            var engine = serviceProvider.GetService<GameEngine>();
            engine?.Start();
        }
    }
}

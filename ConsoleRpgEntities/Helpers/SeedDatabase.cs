using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Items;
using System.Linq;

namespace ConsoleRpg.Helpers
{
    public static class SeedDatabase
    {
        public static void EnsureSeeded(GameContext context)
        {
            // Seed Player
            if (!context.Players.Any())
            {
                var player = new Player
                {
                    Name = "Hero",
                    Health = 100,
                    Equipment = new Equipment
                    {
                        Weapon = new Weapon { Name = "Sword", AttackPower = 10 },
                        Armor = new Armor { Name = "Leather Armor", DefenseRating = 5 }
                    }
                };
                context.Players.Add(player);
            }

            // Seed Goblin
            if (!context.Monsters.OfType<Goblin>().Any())
            {
                var goblin = new Goblin
                {
                    Name = "Goblin Grunt",
                    Health = 30,
                    Attack = 5
                };
                context.Monsters.Add(goblin);
            }

            context.SaveChanges();
        }
    }
}

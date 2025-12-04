using System.Linq;
using System.Threading;
using ConsoleRpg.Helpers;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using Microsoft.EntityFrameworkCore;

namespace ConsoleRpg.Services
{
    public class GameEngine
    {
        private readonly GameContext _context;
        private readonly MenuManager _menuManager;
        private readonly OutputManager _outputManager;

        private Player _player;
        private Monster _goblin;

        public GameEngine(GameContext context, MenuManager menuManager, OutputManager outputManager)
        {
            _context = context;
            _menuManager = menuManager;
            _outputManager = outputManager;
        }

        public void Start()
        {
            LoadPlayer();
            LoadMonsters();

            if (_player == null || _goblin == null)
            {
                _outputManager.WriteLine("Cannot start game. Missing player or monster.");
                return;
            }

            Thread.Sleep(500);
            GameLoop();
        }

        private void LoadPlayer()
        {
            try
            {
                _player = _context.Players
                    .Include(p => p.Equipment)
                        .ThenInclude(e => e.Weapon)
                    .Include(p => p.Equipment)
                        .ThenInclude(e => e.Armor)
                    .Include(p => p.Abilities)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (_player == null)
                {
                    _outputManager.WriteLine("No player found in the database. Ensure a player is seeded.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                _outputManager.WriteLine($"Error loading player: {ex.Message}");
            }
        }

        private void LoadMonsters()
        {
            try
            {
                _goblin = _context.Monsters.OfType<Goblin>().FirstOrDefault();

                if (_goblin == null)
                    _outputManager.WriteLine("No Goblin found in the database. Ensure a Goblin is seeded.");
            }
            catch (System.InvalidOperationException ex)
            {
                _outputManager.WriteLine($"Error loading monster: {ex.Message}");
            }
        }

        private void GameLoop()
        {
            _outputManager.WriteLine("Combat Demo Start!");

            AttackCharacter(_player, _goblin);
            AttackCharacter(_goblin, _player);

            _outputManager.WriteLine("Combat Demo End!");
        }

        private void AttackCharacter(ITargetable attacker, ITargetable defender)
        {
            int baseDamage = 1;

            if (attacker is Player p && p.Equipment?.Weapon != null)
                baseDamage += p.Equipment.Weapon.AttackPower;

            if (attacker is Monster m)
            {
                // Assuming Monster has an Attack property or method.
                // If it's a method, you need to call it (e.g., m.Attack())
                // If it's a property, use m.Attack
                // Here, let's assume it's a property (int Attack { get; })
                var attackValue = (int)m.GetType().GetProperty("Attack")?.GetValue(m) ?? 0;
                if (attackValue <= 0)
                {
                    baseDamage = 1;
                }
                else
                {
                    baseDamage = attackValue;
                }
            }

            int netDamage = baseDamage;

            if (defender is Player dp)
            {
                int defense = dp.Equipment?.Armor?.DefenseRating ?? 0;
                netDamage = System.Math.Max(0, baseDamage - defense);
                dp.Health = System.Math.Max(0, dp.Health - netDamage);

                _outputManager.WriteLine(
                    $"{attacker.GetName()} hits {dp.Name} for {netDamage} damage (raw {baseDamage} - armor {defense})."
                );
            }
            else if (defender is Monster dm)
            {
                dm.Health = System.Math.Max(0, dm.Health - netDamage);
                _outputManager.WriteLine(
                    $"{attacker.GetName()} hits {dm.Name} for {netDamage} damage."
                );
            }
            else
            {
                _outputManager.WriteLine("Unknown target type!");
            }
        }

        internal void Run() => Start();
    }
}

// Extension for getting names
public static class AttackExtensions
{
    public static string GetName(this ITargetable t)
    {
        return t is Player p ? p.Name :
               t is Monster m ? m.Name :
               "Unknown";
    }
}

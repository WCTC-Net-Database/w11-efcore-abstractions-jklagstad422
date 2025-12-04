using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Items;
using System.Collections.Generic;

namespace ConsoleRpgEntities.Models.Characters
{
    public class Player : ITargetable, IPlayer
    {
        public int Experience { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public virtual IEnumerable<Ability> Abilities { get; set; }

        public Equipment Equipment { get; set; }

        public void Attack(ITargetable target)
        {
            int attack = 1;

            if (Equipment?.Weapon != null)
                attack += Equipment.Weapon.AttackPower;

            if (target is ConsoleRpgEntities.Models.Characters.Monsters.Monster m)
            {
                m.Health -= attack;
                System.Console.WriteLine($"{Name} attacks {m.Name} for {attack}. {m.Name}'s health is now {m.Health}.");
            }
        }

        public void UseAbility(IAbility ability, ITargetable target)
        {
            if (Abilities != null && Abilities.Contains(ability))
                ability.Activate(this, target);
            else
                System.Console.WriteLine($"{Name} does not have {ability.Name}!");
        }
    }
}

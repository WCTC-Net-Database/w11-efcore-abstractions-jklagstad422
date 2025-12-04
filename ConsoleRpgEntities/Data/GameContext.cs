using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace ConsoleRpgEntities.Data
{
    public class GameContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Ability> Abilities { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Armor> Armors { get; set; }
        public DbSet<Equipment> Equipment { get; set; }

        public GameContext(DbContextOptions<GameContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasDiscriminator<string>("ItemType")
                .HasValue<Weapon>("Weapon")
                .HasValue<Armor>("Armor");

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Equipment)
                .WithOne()
                .HasForeignKey<Equipment>(e => e.Id)
                .IsRequired(false);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Abilities)
                .WithMany(a => a.Players)
                .UsingEntity(j => j.ToTable("PlayerAbilities"));

            base.OnModelCreating(modelBuilder);
        }
    }
}

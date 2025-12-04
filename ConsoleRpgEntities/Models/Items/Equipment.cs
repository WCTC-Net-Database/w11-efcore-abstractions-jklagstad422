namespace ConsoleRpgEntities.Models.Items
{
    public class Equipment
    {
        public int Id { get; set; }

        public int? WeaponId { get; set; }
        public Weapon Weapon { get; set; }

        public int? ArmorId { get; set; }
        public Armor Armor { get; set; }
    }
}

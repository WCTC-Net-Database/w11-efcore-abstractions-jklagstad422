namespace ConsoleRpgEntities.Models.Items
{
    public class Weapon : Item
    {
        public int AttackPower { get; set; }
        public int Durability { get; set; } = 100;

        public override void Use(object user)
        {
            Durability = System.Math.Max(0, Durability - 1);
        }
    }
}

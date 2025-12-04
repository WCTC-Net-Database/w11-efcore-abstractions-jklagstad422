namespace ConsoleRpgEntities.Models.Items
{
    public class Armor : Item
    {
        public int DefenseRating { get; set; }
        public int Durability { get; set; } = 100;

        public override void Use(object user)
        {
            Durability = System.Math.Max(0, Durability - 1);
        }
    }
}

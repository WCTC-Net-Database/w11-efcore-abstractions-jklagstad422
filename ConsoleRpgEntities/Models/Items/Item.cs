using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleRpgEntities.Models.Items
{
    public abstract class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }

        public abstract void Use(object user);
    }
}

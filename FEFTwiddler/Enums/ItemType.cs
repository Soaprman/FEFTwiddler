namespace FEFTwiddler.Enums
{
    public enum ItemType
    {
        Unknown,
        Sword,
        Lance,
        Axe,
        Shuriken,
        Bow,
        Tome,
        Staff,
        Stone,
        Consumable,
        Held,
        NPC
    }

    public static class ItemTypeExtensions
    {
        public static bool HasForges(this ItemType type)
        {
            return type == ItemType.Sword    ||
                   type == ItemType.Lance    ||
                   type == ItemType.Axe      ||
                   type == ItemType.Shuriken ||
                   type == ItemType.Bow      ||
                   type == ItemType.Tome     ||
                   type == ItemType.Stone;
        }

        public static bool HasCharges(this ItemType type)
        {
            return type == ItemType.Staff    ||
                   type == ItemType.Consumable;
        }
    }
}

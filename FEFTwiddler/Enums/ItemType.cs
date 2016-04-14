namespace FEFTwiddler.Enums
{
    public static class Extensions
    {
        public static bool hasForges(this ItemType type)
        {
            return type == ItemType.Sword    ||
                   type == ItemType.Lance    ||
                   type == ItemType.Axe      ||
                   type == ItemType.Shuriken ||
                   type == ItemType.Bow      ||
                   type == ItemType.Tome     ||
                   type == ItemType.Stone;
        }

        public static bool hasCharges(this ItemType type)
        {
            return type == ItemType.Staff    ||
                   type == ItemType.Consumable;
        }
    }

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
}

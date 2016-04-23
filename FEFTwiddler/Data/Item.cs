namespace FEFTwiddler.Data
{
    public class Item
    {
        public Enums.Item ItemID { get; set; }
        public string DisplayName { get; set; }
        public Enums.ItemType Type { get; set; }
        public Enums.WeaponRank WeaponRank { get; set; }

        /// <summary>
        /// The maximum number of uses for this item. 0 = no maximum
        /// </summary>
        public byte MaximumUses { get; set; }

        /// <summary>
        /// Whether the item is normally only seen in the hands of an enemy or NPC
        /// </summary>
        public bool IsNpcOnly { get; set; }

        /// <summary>
        /// Whether the item disappears when leaving a map (example: Chest Key)
        /// </summary>
        public bool IsMapOnly { get; set; }
    }
}

using System.Drawing;

namespace FEFTwiddler.Data
{
    public class Character
    {
        public Enums.Character CharacterID { get; set; }
        public string DisplayName { get; set; }

        public Model.Stat BaseStats;
        public Model.Stat Modifiers;
        public Model.Stat GrowthRates;

        public bool CanUseStones { get; set; }
        public bool IsCorrin { get; set; }
        public bool IsChild { get; set; }

        public byte MainSupportCount { get; set; }
        public byte FamilySupportCount { get; set; }

        public byte EndBlockType { get; set; }

        public Color HairColor { get; set; }
        public bool IsPrisoner { get; set; }
        public bool IsFemale { get; set; }
        public bool IsManakete { get; set; }
        public bool IsBeast { get; set; }
        public bool CanUseDragonVein { get; set; }
    }
}

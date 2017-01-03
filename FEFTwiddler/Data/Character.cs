using System.Drawing;
using System.Collections.Generic;

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

        public byte EndBlockType { get; set; }

        public Color HairColor { get; set; }
        public bool IsPrisoner { get; set; }
        public bool IsFemale { get; set; }
        public bool IsManakete { get; set; }
        public bool IsBeast { get; set; }
        public bool CanUseDragonVein { get; set; }
        public bool IsCustomDLC { get; set; }

        public class Support
        {
            public Enums.Character CharacterID { get; set; }
            public sbyte C { get; set; }
            public sbyte B { get; set; }
            public sbyte A { get; set; }
            public sbyte S { get; set; }

            public bool HasSSupport
            {
                get { return S == -1; }
            }
        }
        public Support[] SupportPool;
    }
}

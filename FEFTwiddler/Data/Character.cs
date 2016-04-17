namespace FEFTwiddler.Data
{
    public class Character
    {
        public Enums.Character CharacterID { get; set; }
        public string DisplayName { get; set; }

        public sbyte Base_HP { get; set; }
        public sbyte Base_Str { get; set; }
        public sbyte Base_Mag { get; set; }
        public sbyte Base_Skl { get; set; }
        public sbyte Base_Spd { get; set; }
        public sbyte Base_Lck { get; set; }
        public sbyte Base_Def { get; set; }
        public sbyte Base_Res { get; set; }

        public sbyte Modifier_HP { get; set; }
        public sbyte Modifier_Str { get; set; }
        public sbyte Modifier_Mag { get; set; }
        public sbyte Modifier_Skl { get; set; }
        public sbyte Modifier_Spd { get; set; }
        public sbyte Modifier_Lck { get; set; }
        public sbyte Modifier_Def { get; set; }
        public sbyte Modifier_Res { get; set; }

        public bool CanUseStones { get; set; }
    }
}

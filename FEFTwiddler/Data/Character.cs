namespace FEFTwiddler.Data
{
    public class Character
    {
        public Enums.Character CharacterID { get; set; }
        public string DisplayName { get; set; }

        public byte Base_HP { get; set; }
        public byte Base_Str { get; set; }
        public byte Base_Mag { get; set; }
        public byte Base_Skl { get; set; }
        public byte Base_Spd { get; set; }
        public byte Base_Lck { get; set; }
        public byte Base_Def { get; set; }
        public byte Base_Res { get; set; }
    }
}

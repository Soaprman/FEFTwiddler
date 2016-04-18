namespace FEFTwiddler.Data
{
    public class Character
    {
        public Enums.Character CharacterID { get; set; }
        public string DisplayName { get; set; }

        public Model.Stat BaseStats;
        public Model.Stat Modifiers;

        public bool CanUseStones { get; set; }
        public bool IsCorrin { get; set; }
        public bool IsChild { get; set; }
    }
}

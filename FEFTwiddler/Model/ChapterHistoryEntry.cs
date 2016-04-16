namespace FEFTwiddler.Model
{
    public class ChapterHistoryEntry
    {
        public Enums.Chapter ChapterID { get; set; }
        public byte TurnCount { get; set; }
        public Enums.Character HeroCharacterID_1 { get; set; }
        public Enums.Character HeroCharacterID_2 { get; set; }

        public override string ToString()
        {
            return ChapterID.ToString() + " / Turns: " + TurnCount.ToString() + " / Heroes: " + HeroCharacterID_1.ToString() + ", " + HeroCharacterID_2.ToString();
        }
    }
}

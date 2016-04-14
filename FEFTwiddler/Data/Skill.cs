namespace FEFTwiddler.Data
{
    public class Skill
    {
        public Enums.Skill SkillID { get; set; }
        public string DisplayName { get; set; }
        public bool IsEnemyOnly { get; set; }
        public bool IsDLC { get; set; }
        public bool IsPersonal { get; set; }
        public bool IsUnlearnable { get; set; }
        public byte LearnedSkillByteOffset { get; set; }
        public byte LearnedSkillBitMask { get; set; }
    }
}

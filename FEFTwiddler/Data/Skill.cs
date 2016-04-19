namespace FEFTwiddler.Data
{
    public class Skill
    {
        public Enums.Skill SkillID { get; set; }
        public string DisplayName { get; set; }
        public bool IsPersonal { get; set; }
        public bool IsUnlearnable { get; set; }
        public byte LearnedSkillByteOffset { get; set; }
        public byte LearnedSkillBitMask { get; set; }
        public bool IsNormalClassSkill { get; set; }
        public bool IsCorrinOnlySkill { get; set; }
        public bool IsAzuraOnlySkill { get; set; }
        public bool IsKitsuneOnlySkill { get; set; }
        public bool IsWolfskinOnlySkill { get; set; }
        public bool IsVillagerOnlySkill { get; set; }
        public bool IsPathBonusClassSkill { get; set; }
        public bool IsDlcClassSkill { get; set; }
        public bool IsAmiiboClassSkill { get; set; }
        public bool IsEnemyAndNpcSkill { get; set; }
    }
}

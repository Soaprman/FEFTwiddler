using System;
using FEFTwiddler.Enums;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model
{
    public class Character
    {
        /// <summary>
        /// This character joined before the chapter 6 split, left, and hasn't rejoined yet
        /// </summary>
        public bool IsAbsent { get; set; }

        // There are 64 flags here.
        // public bool _IsCorrin { get; set; }
        public bool IsManakete { get; set; }
        public bool IsBeast { get; set; }
        public bool CanUseDragonVein { get; set; }

        public static byte MaxEternalSealsUsed = 0x2F;
        public byte Level { get; set; }
        public byte Experience { get; set; }
        public byte InternalLevel { get; set; }
        public byte EternalSealsUsed { get; set; }
        public Enums.Character CharacterID { get; set; }
        public Enums.Class ClassID { get; set; }
        public byte Unknown011 { get; set; }

        public byte[] UnknownBytesBeforeStatBytes1 { get; set; }
        public byte[] UnknownBytesBeforeStatBytes2 { get; set; }
        public sbyte[] StatBytes1 { get; set; }
        public byte[] StatueBonuses { get; set; }
        public byte[] UnknownBytesBetweenStatBytes { get; set; }
        public sbyte[] StatBytes2 { get; set; }

        public static byte MinWeaponExperience = 0x01;
        public static byte MaxWeaponExperience = 0xFB;
        public byte WeaponExperience_Sword { get; set; }
        public byte WeaponExperience_Lance { get; set; }
        public byte WeaponExperience_Axe { get; set; }
        public byte WeaponExperience_Shuriken { get; set; }
        public byte WeaponExperience_Bow { get; set; }
        public byte WeaponExperience_Tome { get; set; }
        public byte WeaponExperience_Staff { get; set; }
        public byte WeaponExperience_Stone { get; set; }

        public byte MaximumHP { get; set; }

        public byte Position_FromLeft { get; set; }
        public byte Position_FromTop { get; set; }

        public Enums.Skill EquippedSkill_1 { get; set; }
        public Enums.Skill EquippedSkill_2 { get; set; }
        public Enums.Skill EquippedSkill_3 { get; set; }
        public Enums.Skill EquippedSkill_4 { get; set; }
        public Enums.Skill EquippedSkill_5 { get; set; }

        public InventoryItem Item_1 { get; set; }
        public InventoryItem Item_2 { get; set; }
        public InventoryItem Item_3 { get; set; }
        public InventoryItem Item_4 { get; set; }
        public InventoryItem Item_5 { get; set; }

        public bool IsDeployed { get; set; }
        public bool IsDead { get; set; }
        public bool IsEinherjar { get; set; }
        public bool IsRecruited { get; set; }

        public byte[] MainSupports { get; set; }

        public static byte MaxBoots = 0x02;
        public byte Boots { get; set; }
        public byte[] DLCClassFlags { get; set; }
        public byte[] HairColor { get; set; }
        /// <summary>
        /// Some value that seems to always be 0x75, 0x76, or 0x77
        /// </summary>
        public byte UnknownSeventySomething { get; set; }
        public LearnedSkills LearnedSkills { get; set; }

        public Enums.Headwear Headwear { get; set; }
        public Enums.Facewear Facewear { get; set; }
        public Enums.Armwear Armwear { get; set; }
        public Enums.Underwear Underwear { get; set; }

        public ushort BattleCount { get; set; }
        public ushort VictoryCount { get; set; }

        public Enums.Chapter DeathChapter { get; set; }

        public byte[] CorrinName { get; set; }

        public override string ToString()
        {
            return CharacterID.ToString() + ": " + ClassID.ToString() +  " // lv" + Level.ToString() + " exp" + Experience.ToString();
        }

        private Data.Character GetData()
        {
            return Data.Database.Characters.GetByID(CharacterID);
        }

        #region Boundary Enforcement

        /// <summary>
        /// Get this character's max level, unmodified by eternal seals
        /// </summary>
        public byte GetBaseMaxLevel()
        {
            if (!ClassID.IsPromoted()) return 20;
            else return (byte)(IsPrepromote() ? 40 : 20);
        }

        /// <summary>
        /// Get this character's max level after taking eternal seals into consideration
        /// </summary>
        /// <returns></returns>
        public byte GetModifiedMaxLevel()
        {
            byte maxLevel = GetBaseMaxLevel();
            if (!ClassID.IsPromoted()) return maxLevel;
            maxLevel += (byte)(EternalSealsUsed * 5);
            return maxLevel;
        }

        /// <summary>
        /// Get the max possible level this character could attain
        /// </summary>
        public byte GetTheoreticalMaxLevel()
        {
            if (!ClassID.IsPromoted()) return 20;
            else return (byte)(IsPrepromote() ? 255 : 235);
        }

        public byte FixLevel()
        {
            byte maxLevel = GetModifiedMaxLevel();
            if (Level > maxLevel) return maxLevel;
            else return Level;
        }

        public byte GetMaxEternalSealsUsed()
        {
            return (byte)(IsPrepromote() ? MaxEternalSealsUsed - 4 : MaxEternalSealsUsed);
        }

        public byte FixEternalSealsUsed()
        {
            byte maxEternalSealsUsed = GetMaxEternalSealsUsed();
            if (EternalSealsUsed > maxEternalSealsUsed) return maxEternalSealsUsed;
            else return EternalSealsUsed;
        }

        public byte GetMinimumEternalSealsForCurrentLevel()
        {
            var baseMaxLevel = GetBaseMaxLevel();
            if (Level <= baseMaxLevel) return 0;
            else return (byte)((Level - baseMaxLevel + 4) / 5);
        }

        public static byte FixWeaponExperience(byte weaponExp)
        {
            if (weaponExp < MinWeaponExperience) return MinWeaponExperience;
            if (weaponExp > MaxWeaponExperience) return MaxWeaponExperience;
            return weaponExp;
        }

        public static byte FixBoots(byte boots)
        {
            if (boots > MaxBoots) return MaxBoots;
            return boots;
        }

        #endregion

        #region Cheats

        public void LearnAllSkills()
        {
            // Learn the skills, but leave existing learned skills outside this range intact
            LearnedSkills.Add(new byte[]
            { 0xDF, 0xFF, 0x7F, 0xFB,
              0xFF, 0xFF, 0xFF, 0xFF,
              0xFF, 0xFE, 0xFF, 0xEF,
              0xFF, 0xBF, 0x01, 0x00,
              0x00, 0x00, 0x00, 0x00 });
        }

        public void LearnAllSkillsDLC()
        {
            // TODO: Change array to include only DLC skills
            LearnedSkills.Add(new byte[]
            { 0xFF, 0xFF, 0xFF, 0xFF,
              0xFF, 0xFF, 0xFF, 0xFF,
              0xFF, 0xFE, 0xFF, 0xFF,
              0xFF, 0xFF, 0x81, 0x03,
              0xFF, 0xFF, 0xFF, 0xFF });
        }

        public void LearnAllSkillsEnemy()
        {
            // TODO: Change array to include only enemy skills
            LearnedSkills.Add(new byte[]
            { 0xFF, 0xFF, 0xFF, 0xFF,
              0xFF, 0xFF, 0xFF, 0xFF,
              0xFF, 0xFF, 0xFF, 0xFF,
              0xFF, 0xFF, 0xFF, 0xFF,
              0xFF, 0xFF, 0xFF, 0xFF });
        }

        public void MaximizeStatues()
        {
            BattleCount = Math.Max(BattleCount, (ushort)100);
            VictoryCount = Math.Max(VictoryCount, (ushort)100);
        }

        public void SRankAllWeapons()
        {
            WeaponExperience_Sword = MaxWeaponExperience;
            WeaponExperience_Lance = MaxWeaponExperience;
            WeaponExperience_Axe = MaxWeaponExperience;
            WeaponExperience_Shuriken = MaxWeaponExperience;
            WeaponExperience_Bow = MaxWeaponExperience;
            WeaponExperience_Tome = MaxWeaponExperience;
            WeaponExperience_Staff = MaxWeaponExperience;
            WeaponExperience_Stone = (GetData().CanUseStones ? MaxWeaponExperience : MinWeaponExperience);
        }

        #endregion

        #region Character enum lookup stuff

        /// <summary>
        /// This character starts as a promoted class, and can level to 40 without using eternal seals.
        /// </summary>
        public bool IsPrepromote()
        {
            return InternalLevel < 10 && ClassID.IsPromoted();
        }

        #endregion
    }
}
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

        public int GetBlockSize()
        {
            // All Einherjar units have the same support-free block size, regardless of which character it is
            if (this.IsEinherjar) return 0xEB;

            switch (this.CharacterID)
            {
                case Enums.Character.Corrin_F: // guess
                case Enums.Character.Corrin_M:
                    return 0x15A;
                case Enums.Character.Azura:
                case Enums.Character.Felicia:
                case Enums.Character.Jakob:
                case Enums.Character.Kaze:
                case Enums.Character.Mozu:
                case Enums.Character.Silas:
                    return 0x103;
                case Enums.Character.Arthur:
                case Enums.Character.Azama:
                case Enums.Character.Beruka:
                case Enums.Character.Camilla:
                case Enums.Character.Charlotte:
                case Enums.Character.Effie:
                case Enums.Character.Elise:
                case Enums.Character.Hana:
                case Enums.Character.Hayato:
                case Enums.Character.Hinata:
                case Enums.Character.Hinoka:
                case Enums.Character.Kaden:
                case Enums.Character.Kagero:
                case Enums.Character.Keaton:
                case Enums.Character.Laslow:
                case Enums.Character.Leo:
                case Enums.Character.Niles:
                case Enums.Character.Nyx:
                case Enums.Character.Oboro:
                case Enums.Character.Odin:
                case Enums.Character.Orochi:
                case Enums.Character.Peri:
                case Enums.Character.Rinkah:
                case Enums.Character.Ryoma:
                case Enums.Character.Saizo:
                case Enums.Character.Sakura:
                case Enums.Character.Selena:
                case Enums.Character.Setsuna:
                case Enums.Character.Subaki:
                case Enums.Character.Takumi:
                case Enums.Character.Xander:
                    return 0xFE;
                case Enums.Character.Benny:
                    return 0xFD;
                case Enums.Character.Kana_F:
                case Enums.Character.Kana_M: // guess
                case Enums.Character.Ophelia:
                case Enums.Character.Nina:
                    return 0x123;
                case Enums.Character.Asugi:
                case Enums.Character.Caeldori:
                case Enums.Character.Forrest:
                case Enums.Character.Hisame:
                case Enums.Character.Ignatius:
                case Enums.Character.Kiragi:
                case Enums.Character.Mitama:
                case Enums.Character.Percy:
                case Enums.Character.Rhajat:
                case Enums.Character.Selkie:
                case Enums.Character.Shigure:
                case Enums.Character.Shiro:
                case Enums.Character.Siegbert:
                case Enums.Character.Soleil:
                case Enums.Character.Velouria:
                    return 0x124;
                case Enums.Character.Dwyer:
                case Enums.Character.Midori:
                case Enums.Character.Sophie:
                    return 0x125;
                case Enums.Character.Flora:
                case Enums.Character.Fuga:
                case Enums.Character.Gunter:
                    return 0xEE;
                case Enums.Character.Anna: // guess
                case Enums.Character.Izana: // guess
                case Enums.Character.Reina:
                case Enums.Character.Scarlet: // guess
                case Enums.Character.Shura:
                case Enums.Character.Yukimura: // guess
                    return 0xED;
                case Enums.Character.Banba: // guess
                case Enums.Character.Candace:
                case Enums.Character.Daichi:
                case Enums.Character.Daniela: // guess
                case Enums.Character.Funke: // guess
                case Enums.Character.Gazak:
                case Enums.Character.Haitaka: // guess
                case Enums.Character.Kumagera: // guess
                case Enums.Character.Llewelyn: // guess
                case Enums.Character.Lloyd: // guess
                case Enums.Character.Nichol:
                case Enums.Character.Senno:
                case Enums.Character.Zhara:
                    return 0xEB;
                default: // lazily handle the generics
                    return 0xEB;
            }
        }

        public int GetSupportBlockSize()
        {
            var supportSize = GetBlockSize();

            // TODO
            supportSize -= 9;

            // Character main data
            supportSize -= 8;

            // TODO
            supportSize -= 70;

            // Skills
            supportSize -= 10;

            // Inventory
            supportSize -= 25;

            // Unknown stuff right after support block
            supportSize -= 17;

            // Everything else
            supportSize -= 90;

            // Corrin has 44 bytes of stuff at the end
            if (this.CharacterID.IsCorrin())
            {
                supportSize -= 44;
            }

            // Gen 2 units have supports split into two blocks
            if (this.CharacterID.IsKana() || this.CharacterID.IsGen2Ordinary())
            {
                supportSize -= 42;
            }

            return supportSize;
        }

        public int GetSupportBlock2Size()
        {
            // Gen 2 units have supports split into two blocks
            if (this.CharacterID.IsKana() || this.CharacterID.IsGen2Ordinary())
            {
                return 42;
            }
            else
            {
                return 0;
            }
        }

        public int GetEndBlockSize()
        {
            if (this.CharacterID.IsCorrin())
            {
                return 44;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}
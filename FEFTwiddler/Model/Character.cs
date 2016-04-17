using System;
using System.Linq;
using FEFTwiddler.Enums;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model
{
    public class Character
    {
        /// <summary>
        /// Create a new blank character
        /// </summary>
        public static Character Create()
        {
            var character = new Character();
            character.RawBlock1 = new byte[RawBlock1Length];
            character.RawInventory = new byte[RawInventoryLength];
            character.RawNumberOfSupports = 0x00;
            character.RawSupports = new byte[0x00];
            character.RawBlock2 = new byte[RawBlock2Length];
            character.RawLearnedSkills = new byte[RawLearnedSkillsLength];
            character.RawDeployedUnitInfo = new byte[(character.IsDeployed ? RawDeployedUnitInfoLengthIfDeployed : RawDeployedUnitInfoLengthIfNotDeployed)];
            character.RawBlock3 = new byte[RawBlock3Length];
            character.RawEndBlockType = 0x00;
            character.RawEndBlock = new byte[0x00];
            return character;
        }

        #region Raw Data

        public byte[] Raw
        {
            get
            {
                return RawBlock1
                    .Concat(RawInventory)
                    .Concat(RawNumberOfSupports.Yield())
                    .Concat(RawSupports)
                    .Concat(RawBlock2)
                    .Concat(RawLearnedSkills)
                    .Concat(RawDeployedUnitInfo)
                    .Concat(RawBlock3)
                    .Concat(RawEndBlockType.Yield())
                    .Concat(RawEndBlock)
                    .ToArray();
            }
        }

        public const int RawBlock1Length = 0x63;
        private byte[] _rawBlock1;
        public byte[] RawBlock1
        {
            get { return _rawBlock1; }
            set
            {
                if (value.Length != RawBlock1Length) throw new ArgumentException("Character block 1 must be " + RawBlock1Length + " bytes in length");
                _rawBlock1 = value;
            }
        }

        public const int RawInventoryLength = 0x19;
        public byte[] RawInventory
        {
            get
            {
                return ((byte)(0x00)).Yield().Concat(Item_1.Raw)
                    .Concat(((byte)(0x00)).Yield()).Concat(Item_2.Raw)
                    .Concat(((byte)(0x00)).Yield()).Concat(Item_3.Raw)
                    .Concat(((byte)(0x00)).Yield()).Concat(Item_4.Raw)
                    .Concat(((byte)(0x00)).Yield()).Concat(Item_5.Raw).ToArray();
            }
            set
            {
                if (value.Length != RawInventoryLength) throw new ArgumentException("Character inventory block must be " + RawInventoryLength + " bytes in length");
                Item_1 = new InventoryItem(value.Skip(0x01).Take(0x04).ToArray());
                Item_2 = new InventoryItem(value.Skip(0x06).Take(0x04).ToArray());
                Item_3 = new InventoryItem(value.Skip(0x0B).Take(0x04).ToArray());
                Item_4 = new InventoryItem(value.Skip(0x10).Take(0x04).ToArray());
                Item_5 = new InventoryItem(value.Skip(0x15).Take(0x04).ToArray());
            }
        }

        public byte RawNumberOfSupports { get; set; }

        private byte[] _rawSupports;
        public byte[] RawSupports
        {
            get { return _rawSupports; }
            set
            {
                if (value.Length != RawNumberOfSupports) throw new ArgumentException("Character support block is not equal to RawNumberOfSupports");
                _rawSupports = value;
            }
        }

        public const int RawBlock2Length = 0x46;
        private byte[] _rawBlock2;
        public byte[] RawBlock2
        {
            get { return _rawBlock2; }
            set
            {
                if (value.Length != RawBlock2Length) throw new ArgumentException("Character block 2 must be " + RawBlock2Length + " bytes in length");
                _rawBlock2 = value;
            }
        }

        public const int RawLearnedSkillsLength = 0x14;
        public byte[] RawLearnedSkills
        {
            get
            {
                return LearnedSkills.Raw;
            }
            set
            {
                if (value.Length != RawLearnedSkillsLength) throw new ArgumentException("Character learned skills block must be " + RawLearnedSkillsLength + " bytes in length");
                LearnedSkills = new Model.LearnedSkills(value);
            }
        }

        public const int RawDeployedUnitInfoLengthIfNotDeployed = 0x00;
        public const int RawDeployedUnitInfoLengthIfDeployed = 0x36;
        private byte[] _rawDeployedUnitInfo;
        public byte[] RawDeployedUnitInfo
        {
            get { return _rawDeployedUnitInfo; }
            set
            {
                var length = (IsDeployed ? RawDeployedUnitInfoLengthIfDeployed : RawDeployedUnitInfoLengthIfNotDeployed);
                if (value.Length != length) throw new ArgumentException("Character deployed unit info block must be " + length + " bytes in length");
                _rawDeployedUnitInfo = value;
            }
        }

        public const int RawBlock3Length = 0x13;
        private byte[] _rawBlock3;
        public byte[] RawBlock3
        {
            get { return _rawBlock3; }
            set
            {
                if (value.Length != RawBlock3Length) throw new ArgumentException("Character block 3 must be " + RawBlock3Length + " bytes in length");
                _rawBlock3 = value;
            }
        }

        public byte RawEndBlockType { get; set; }

        public int GetRawEndBlockSize()
        {
            return GetRawEndBlockSizeByType(RawEndBlockType);
        }
        public int GetRawEndBlockSizeByType(byte rawEndBlockType)
        {
            switch (rawEndBlockType)
            {
                case 0x04: return 0x2C;
                case 0x01: return 0x2A;
                default: return 0x00;
            }
        }

        private byte[] _rawEndBlock;
        public byte[] RawEndBlock
        {
            get { return _rawEndBlock; }
            set
            {
                if (value.Length != GetRawEndBlockSizeByType(RawEndBlockType)) throw new ArgumentException("End block size is incorrect given its type");
                _rawEndBlock = value;
            }
        }

        #endregion

        #region Meta Properties

        /* These are properties that are either:
         * - Derived from other properties that exist in the character data, but have no bits themselves
         * - Do not exist in the character data, and are set by the calling code
         * */

        /// <summary>
        /// This character joined before the chapter 6 split, left, and hasn't rejoined yet
        /// </summary>
        public bool IsAbsent { get; set; }

        /// <summary>
        /// Whether this character is deployed to the map
        /// </summary>
        /// <remarks>There might be an actual flag for this, but this works too</remarks>
        public bool IsDeployed
        {
            get { return Position_FromLeft != 0xFF || Position_FromTop != 0xFF; }
        }

        /// <summary>
        /// This character starts as a promoted class, and can level to 40 without using eternal seals.
        /// </summary>
        public bool IsPrepromote()
        {
            return InternalLevel < 10 && ClassID.IsPromoted();
        }

        #endregion

        #region Block 1 Properties

        // First byte (0x00), always 07 I think

        // Eight "flag" bytes (0x01 through 0x08)

        public bool IsManakete
        {
            get { return _rawBlock1[0x03].GetFlag(0x80); }
            set { _rawBlock1[0x03] = _rawBlock1[0x03].SetFlag(0x80, value); }
        }

        public bool IsBeast
        {
            get { return _rawBlock1[0x04].GetFlag(0x01); }
            set { _rawBlock1[0x04] = _rawBlock1[0x04].SetFlag(0x01, value); }
        }

        public bool CanUseDragonVein
        {
            get { return _rawBlock1[0x05].GetFlag(0x08); }
            set { _rawBlock1[0x05] = _rawBlock1[0x05].SetFlag(0x08, value); }
        }

        // End "flag" bytes

        public byte Level
        {
            get { return _rawBlock1[0x09]; }
            set { _rawBlock1[0x09] = value; }
        }

        public byte Experience
        {
            get { return _rawBlock1[0x0A]; }
            set { _rawBlock1[0x0A] = value; }
        }

        public byte InternalLevel
        {
            get { return _rawBlock1[0x0B]; }
            set { _rawBlock1[0x0B] = value; }
        }

        public byte EternalSealsUsed
        {
            get { return _rawBlock1[0x0C]; }
            set { _rawBlock1[0x0C] = value; }
        }

        public Enums.Character CharacterID
        {
            get { return (Enums.Character)(ushort)((_rawBlock1[0x0E] * 0x100) + _rawBlock1[0x0D]); }
            set
            {
                _rawBlock1[0x0D] = (byte)(((ushort)value) & 0xFF);
                _rawBlock1[0x0E] = (byte)(((ushort)value >> 8) & 0xFF);
            }
        }

        public Enums.Class ClassID
        {
            get { return (Enums.Class)_rawBlock1[0x0F]; }
            set { _rawBlock1[0x0F] = (byte)value; }
        }

        // One unknown byte (0x10) (might be upper byte for ClassID)

        // Two unknown bytes (0x11 through 0x12)

        // Twelve unknown bytes (0x13 through 0x1E)

        public sbyte Stat_HP_Gained
        {
            get { return (sbyte)_rawBlock1[0x1F]; }
            set { _rawBlock1[0x1F] = (byte)value; }
        }

        public sbyte Stat_Str_Gained
        {
            get { return (sbyte)_rawBlock1[0x20]; }
            set { _rawBlock1[0x20] = (byte)value; }
        }

        public sbyte Stat_Mag_Gained
        {
            get { return (sbyte)_rawBlock1[0x21]; }
            set { _rawBlock1[0x21] = (byte)value; }
        }

        public sbyte Stat_Skl_Gained
        {
            get { return (sbyte)_rawBlock1[0x22]; }
            set { _rawBlock1[0x22] = (byte)value; }
        }

        public sbyte Stat_Spd_Gained
        {
            get { return (sbyte)_rawBlock1[0x23]; }
            set { _rawBlock1[0x23] = (byte)value; }
        }

        public sbyte Stat_Lck_Gained
        {
            get { return (sbyte)_rawBlock1[0x24]; }
            set { _rawBlock1[0x24] = (byte)value; }
        }

        public sbyte Stat_Def_Gained
        {
            get { return (sbyte)_rawBlock1[0x25]; }
            set { _rawBlock1[0x25] = (byte)value; }
        }

        public sbyte Stat_Res_Gained
        {
            get { return (sbyte)_rawBlock1[0x26]; }
            set { _rawBlock1[0x26] = (byte)value; }
        }

        public byte Stat_HP_StatueBonus
        {
            get { return _rawBlock1[0x27]; }
            set { _rawBlock1[0x27] = value; }
        }

        public byte Stat_Str_StatueBonus
        {
            get { return _rawBlock1[0x28]; }
            set { _rawBlock1[0x28] = value; }
        }

        public byte Stat_Mag_StatueBonus
        {
            get { return _rawBlock1[0x29]; }
            set { _rawBlock1[0x29] = value; }
        }

        public byte Stat_Skl_StatueBonus
        {
            get { return _rawBlock1[0x2A]; }
            set { _rawBlock1[0x2A] = value; }
        }

        public byte Stat_Spd_StatueBonus
        {
            get { return _rawBlock1[0x2B]; }
            set { _rawBlock1[0x2B] = value; }
        }

        public byte Stat_Lck_StatueBonus
        {
            get { return _rawBlock1[0x2C]; }
            set { _rawBlock1[0x2C] = value; }
        }

        public byte Stat_Def_StatueBonus
        {
            get { return _rawBlock1[0x2D]; }
            set { _rawBlock1[0x2D] = value; }
        }

        public byte Stat_Res_StatueBonus
        {
            get { return _rawBlock1[0x2E]; }
            set { _rawBlock1[0x2E] = value; }
        }

        // Eight unknown bytes (0x2F through 0x36)

        public sbyte Stat_HP_ExtraGained
        {
            get { return (sbyte)_rawBlock1[0x37]; }
            set { _rawBlock1[0x37] = (byte)value; }
        }

        public sbyte Stat_Str_ExtraGained
        {
            get { return (sbyte)_rawBlock1[0x38]; }
            set { _rawBlock1[0x38] = (byte)value; }
        }

        public sbyte Stat_Mag_ExtraGained
        {
            get { return (sbyte)_rawBlock1[0x39]; }
            set { _rawBlock1[0x39] = (byte)value; }
        }

        public sbyte Stat_Skl_ExtraGained
        {
            get { return (sbyte)_rawBlock1[0x3A]; }
            set { _rawBlock1[0x3A] = (byte)value; }
        }

        public sbyte Stat_Spd_ExtraGained
        {
            get { return (sbyte)_rawBlock1[0x3B]; }
            set { _rawBlock1[0x3B] = (byte)value; }
        }

        public sbyte Stat_Lck_ExtraGained
        {
            get { return (sbyte)_rawBlock1[0x3C]; }
            set { _rawBlock1[0x3C] = (byte)value; }
        }

        public sbyte Stat_Def_ExtraGained
        {
            get { return (sbyte)_rawBlock1[0x3D]; }
            set { _rawBlock1[0x3D] = (byte)value; }
        }

        public sbyte Stat_Res_ExtraGained
        {
            get { return (sbyte)_rawBlock1[0x3E]; }
            set { _rawBlock1[0x3E] = (byte)value; }
        }

        public byte WeaponExperience_Sword
        {
            get { return _rawBlock1[0x3F]; }
            set { _rawBlock1[0x3F] = value; }
        }

        public byte WeaponExperience_Lance
        {
            get { return _rawBlock1[0x40]; }
            set { _rawBlock1[0x40] = value; }
        }

        public byte WeaponExperience_Axe
        {
            get { return _rawBlock1[0x41]; }
            set { _rawBlock1[0x41] = value; }
        }

        public byte WeaponExperience_Shuriken
        {
            get { return _rawBlock1[0x42]; }
            set { _rawBlock1[0x42] = value; }
        }

        public byte WeaponExperience_Bow
        {
            get { return _rawBlock1[0x43]; }
            set { _rawBlock1[0x43] = value; }
        }

        public byte WeaponExperience_Tome
        {
            get { return _rawBlock1[0x44]; }
            set { _rawBlock1[0x44] = value; }
        }

        public byte WeaponExperience_Staff
        {
            get { return _rawBlock1[0x45]; }
            set { _rawBlock1[0x45] = value; }
        }

        public byte WeaponExperience_Stone
        {
            get { return _rawBlock1[0x46]; }
            set { _rawBlock1[0x46] = value; }
        }

        // May be CurrentHP - more research is needed
        public byte MaximumHP
        {
            get { return _rawBlock1[0x47]; }
            set { _rawBlock1[0x47] = value; }
        }

        /// <summary>Map position (battle prep only)</summary>
        public byte Position_FromLeft
        {
            get { return _rawBlock1[0x48]; }
            set { _rawBlock1[0x48] = value; }
        }

        /// <summary>Map position (battle prep only)</summary>
        public byte Position_FromTop
        {
            get { return _rawBlock1[0x49]; }
            set { _rawBlock1[0x49] = value; }
        }

        // Two unknown bytes (0x4A through 0x4B)
        // Seems to always be FF FF

        // Five bytes of flags (0x4C through 0x50)

        public bool IsDead
        {
            get { return _rawBlock1[0x4C].GetFlag(0x18); }
            set { _rawBlock1[0x4C] = _rawBlock1[0x4C].SetFlag(0x18, value); }
        }

        public bool IsEinherjar
        {
            get { return _rawBlock1[0x4F].GetFlag(0x08); }
            set { _rawBlock1[0x4F] = _rawBlock1[0x4F].SetFlag(0x08, value); }
        }

        public bool IsRecruited
        {
            get { return _rawBlock1[0x4F].GetFlag(0x10); }
            set { _rawBlock1[0x4F] = _rawBlock1[0x4F].SetFlag(0x10, value); }
        }

        // Six unknown bytes (0x51 through 0x56)
        // Seems to always be 00 00 00 FF FF 00

        // Two unknown bytes (0x57 through 0x58)

        public Enums.Skill EquippedSkill_1
        {
            get { return (Enums.Skill)_rawBlock1[0x59]; }
            set { _rawBlock1[0x59] = (byte)value; }
        }

        // One byte (may be Skill upper byte)

        public Enums.Skill EquippedSkill_2
        {
            get { return (Enums.Skill)_rawBlock1[0x5B]; }
            set { _rawBlock1[0x5B] = (byte)value; }
        }

        // One byte (may be Skill upper byte)

        public Enums.Skill EquippedSkill_3
        {
            get { return (Enums.Skill)_rawBlock1[0x5D]; }
            set { _rawBlock1[0x5D] = (byte)value; }
        }

        // One byte (may be Skill upper byte)

        public Enums.Skill EquippedSkill_4
        {
            get { return (Enums.Skill)_rawBlock1[0x5F]; }
            set { _rawBlock1[0x5F] = (byte)value; }
        }

        // One byte (may be Skill upper byte)

        public Enums.Skill EquippedSkill_5
        {
            get { return (Enums.Skill)_rawBlock1[0x61]; }
            set { _rawBlock1[0x61] = (byte)value; }
        }

        // One byte (may be Skill upper byte)

        #endregion

        #region Inventory Properties

        // One byte (seems to always be 00)
        public InventoryItem Item_1 { get; set; }
        // One byte (seems to always be 00)
        public InventoryItem Item_2 { get; set; }
        // One byte (seems to always be 00)
        public InventoryItem Item_3 { get; set; }
        // One byte (seems to always be 00)
        public InventoryItem Item_4 { get; set; }
        // One byte (seems to always be 00)
        public InventoryItem Item_5 { get; set; }

        #endregion

        #region Support Properties

        public byte NumberOfSupports
        {
            get { return _rawBlock1[0x7C]; }
            set { _rawBlock1[0x7C] = value; }
        }

        #endregion

        #region Block 2 Properties

        // Seven unknown bytes (0x00 through 0x06)

        public byte Boots
        {
            get { return _rawBlock2[0x07]; }
            set { _rawBlock2[0x07] = value; }
        }

        // Eight unknown bytes (0x08 through 0x0F)

        public byte[] DLCClassFlags
        {
            get { return _rawBlock2.Skip(0x0F).Take(0x03).ToArray(); }
            set
            {
                if (value.Length != 0x03) throw new ArgumentException("DLCClassFlags must be 0x03 bytes long");
                Array.Copy(value, 0x00, _rawBlock2, 0x0F, 0x03);
            }
        }

        public byte[] HairColor
        {
            get { return _rawBlock2.Skip(0x13).Take(0x04).ToArray(); }
            set
            {
                if (value.Length != 0x04) throw new ArgumentException("HairColor must be 0x04 bytes long");
                Array.Copy(value, 0x00, _rawBlock2, 0x13, 0x04);
            }
        }

        // Thirteen unknown bytes (0x17 through 0x23)

        /// <summary>Its purpose is unknown, but it always seems to be either 75, 76, or 77</summary>
        public byte UnknownSeventySomething
        {
            get { return _rawBlock2[0x24]; }
            set { _rawBlock2[0x24] = value; }
        }

        // Thirty-three unknown bytes (0x25 through 0x45)

        #endregion

        #region Learned Skills Properties

        public Model.LearnedSkills LearnedSkills { get; set; }

        #endregion

        #region Deployed Unit Info Properties

        // Extra data on deployed characters in battle prep saves.
        // Might contain debuffs, status effects, and other battle-specific status info
        // This is what it looks like in my "030_Chapter1" save:
        // 00 00 00 00 00 FF FF 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF

        #endregion

        #region Block 3 Properties

        // First byte (0x00) is always 0x02 (commonly used to denote the end of skills block)

        // Four unknown bytes (0x01 through 0x04)

        public Enums.Headwear Headwear
        {
            get { return (Enums.Headwear)_rawBlock3[0x05]; }
            set { _rawBlock3[0x05] = (byte)value; }
        }

        public Enums.Facewear Facewear
        {
            get { return (Enums.Facewear)_rawBlock3[0x06]; }
            set { _rawBlock3[0x06] = (byte)value; }
        }

        public Enums.Armwear Armwear
        {
            get { return (Enums.Armwear)_rawBlock3[0x07]; }
            set { _rawBlock3[0x07] = (byte)value; }
        }

        public Enums.Underwear Underwear
        {
            get { return (Enums.Underwear)_rawBlock3[0x08]; }
            set { _rawBlock3[0x08] = (byte)value; }
        }

        // One unknown byte (0x09)

        public ushort BattleCount
        {
            get { return (ushort)((_rawBlock3[0x0B] * 0x100) + _rawBlock3[0x0A]); }
            set
            {
                _rawBlock3[0x0A] = (byte)(((ushort)value) & 0xFF);
                _rawBlock3[0x0B] = (byte)(((ushort)value >> 8) & 0xFF);
            }
        }

        public ushort VictoryCount
        {
            get { return (ushort)((_rawBlock3[0x0D] * 0x100) + _rawBlock3[0x0C]); }
            set
            {
                _rawBlock3[0x0C] = (byte)(((ushort)value) & 0xFF);
                _rawBlock3[0x0D] = (byte)(((ushort)value >> 8) & 0xFF);
            }
        }

        // Two unknown bytes (0x0E through 0x0F)

        public Enums.Chapter DeathChapter
        {
            get { return (Enums.Chapter)_rawBlock3[0x10]; }
            set { _rawBlock3[0x10] = (byte)value; }
        }

        // Two unknown bytes (0x11 through 0x12)

        #endregion

        #region End Block Properties (Corrin)

        public string CorrinName
        {
            get
            {
                try { VerifyEndBlockSizeIfCorrin(); } catch (MissingFieldException) { return null; }
                return Utils.TypeConverter.ToString(_rawEndBlock.Take(0x18).ToArray());
            }
            set
            {
                VerifyEndBlockSizeIfCorrin();
                Array.Copy(Utils.TypeConverter.ToByteArray(value, 0x0C), 0x00, _rawEndBlock, 0x00, 0x0C);
            }
        }

        private void VerifyEndBlockSizeIfCorrin()
        {
            if (_rawEndBlock.Length != GetRawEndBlockSizeByType(0x04)) throw new MissingFieldException("Field does not exist in this character's end block");
        }

        #endregion

        #region End Block Properties (Child)

        // It's all unknown for now. Hooray!

        private void VerifyEndBlockSizeIfChild()
        {
            if (_rawEndBlock.Length != GetRawEndBlockSizeByType(0x01)) throw new MissingFieldException("Field does not exist in this character's end block");
        }

        #endregion

        #region Helpers

        public override string ToString()
        {
            return CharacterID.ToString() + ": " + ClassID.ToString() +  " // lv" + Level.ToString() + " exp" + Experience.ToString();
        }

        private Data.Character GetData()
        {
            return Data.Database.Characters.GetByID(CharacterID);
        }

        #endregion

        #region Boundary Enforcement

        public static byte MaxEternalSealsUsed = 0x2F;
        public static byte MinWeaponExperience = 0x01;
        public static byte MaxWeaponExperience = 0xFB;
        public static byte MaxBoots = 0x02;

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
    }
}
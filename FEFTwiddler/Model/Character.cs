using System;
using System.Linq;
using FEFTwiddler.Enums;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model
{
    public class Character
    {
        #region Creation

        /// <summary>
        /// Create a new blank character
        /// </summary>
        public static Character Create()
        {
            var character = new Character();
            character.RawBlock1 = new byte[RawBlock1Length];
            character.RawInventory = new byte[RawInventoryLength];
            character.RawNumberOfSupports = 0x00;
            character.RawSupports = new byte[character.RawNumberOfSupports];
            character.RawBlock2 = new byte[RawBlock2Length];
            character.RawLearnedSkills = new byte[RawLearnedSkillsLength];
            character.RawBlock3 = new byte[RawBlock3Length];
            character.RawEndBlockType = 0x00;
            character.RawEndBlock = new byte[0x00];
            character.Initialize();
            // These go after Initialize because it sets stuff like IsDeployed
            character.RawDeployedUnitInfo = new byte[(character.IsDeployed ? RawDeployedUnitInfoLengthIfDeployed : RawDeployedUnitInfoLengthIfNotDeployed)];
            return character;
        }

        /// <summary>
        /// Create a new "base" version of the given character
        /// </summary>
        public static Character Create(Enums.Character characterId)
        {
            var characterData = Data.Database.Characters.GetByID(characterId);

            var character = new Character();
            character.RawBlock1 = new byte[RawBlock1Length];
            character.CharacterID = characterId;
            character.RawInventory = new byte[RawInventoryLength];
            character.RawNumberOfSupports = characterData.MainSupportCount;
            character.RawSupports = new byte[character.RawNumberOfSupports];
            character.RawBlock2 = new byte[RawBlock2Length];
            character.RawLearnedSkills = new byte[RawLearnedSkillsLength];
            character.RawBlock3 = new byte[RawBlock3Length];
            character.RawEndBlockType = characterData.EndBlockType;
            character.RawEndBlock = new byte[GetRawEndBlockSizeByType(character.RawEndBlockType)];
            character.Initialize();
            // These go after Initialize because it sets stuff like IsDeployed
            character.RawDeployedUnitInfo = new byte[(character.IsDeployed ? RawDeployedUnitInfoLengthIfDeployed : RawDeployedUnitInfoLengthIfNotDeployed)];
            return character;
        }

        /// <summary>
        /// Applies values to a character that are known to be the same among all characters
        /// </summary>
        public void Initialize()
        {
            _rawBlock1[0x00] = 0x07;

            var characterData = Data.Database.Characters.GetByID(CharacterID);

            Level = 1;

            // These eleven bytes are random as far as I can tell
            var randomBytes1 = new byte[0x0B];
            (new Random()).NextBytes(randomBytes1);
            Array.Copy(randomBytes1, 0x00, _rawBlock1, 0x13, 0x0B);

            WeaponExperience_Sword = 1;
            WeaponExperience_Lance = 1;
            WeaponExperience_Axe = 1;
            WeaponExperience_Shuriken = 1;
            WeaponExperience_Bow = 1;
            WeaponExperience_Tome = 1;
            WeaponExperience_Staff = 1;
            WeaponExperience_Stone = 1;

            MaximumHP = (byte)characterData.BaseStats.HP;

            _rawBlock1[0x48] = 0xFF;
            _rawBlock1[0x49] = 0xFF;
            _rawBlock1[0x4A] = 0xFF;
            _rawBlock1[0x4B] = 0xFF;

            _rawBlock1[0x54] = 0xFF;
            _rawBlock1[0x55] = 0xFF;

            HairColor[0] = characterData.HairColor.R;
            HairColor[1] = characterData.HairColor.G;
            HairColor[2] = characterData.HairColor.B;
            HairColor[3] = characterData.HairColor.A;

            _rawBlock2[0x17] = 0x00;
            _rawBlock2[0x18] = 0x24;
            _rawBlock2[0x19] = 0x82;
            _rawBlock2[0x1A] = 0x25;
            _rawBlock2[0x1B] = 0x21;
            _rawBlock2[0x1C] = 0x24;
            _rawBlock2[0x1D] = 0xC3;
            _rawBlock2[0x1E] = 0x4A;
            _rawBlock2[0x1F] = 0x16;

            // These four bytes are random as far as I can tell
            var randomBytes2 = new byte[0x04];
            (new Random()).NextBytes(randomBytes2);
            Array.Copy(randomBytes2, 0x00, _rawBlock2, 0x20, 0x04);

            _rawBlock2[0x09] = 0x02;

            _rawBlock2[0x28] = 0x02;
            _rawBlock2[0x29] = 0x07;

            _rawBlock2[0x41] = 0x01;
            _rawBlock2[0x42] = 0x14;

            _rawBlock3[0x00] = 0x02;
        }

        #endregion

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
            get
            {
                var blah = (_rawBlock1.Take(0x1F))
                    .Concat(_gainedStats.Raw)
                    .Concat(_statueBonusStats.Raw)
                    .Concat(_rawBlock1.Skip(0x2F).Take(0x08))
                    .Concat(_extraGainedStats.Raw)
                    .Concat(_rawBlock1.Skip(0x3F))
                    .ToArray();
                return blah;
            }
            set
            {
                if (value.Length != RawBlock1Length) throw new ArgumentException("Character block 1 must be " + RawBlock1Length + " bytes in length");
                _rawBlock1 = value;
                _gainedStats = new Stat(_rawBlock1.Skip(0x1F).Take(0x08).ToArray());
                _statueBonusStats = new Stat(_rawBlock1.Skip(0x27).Take(0x08).ToArray());
                _extraGainedStats = new Stat(_rawBlock1.Skip(0x37).Take(0x08).ToArray());
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
        public static int GetRawEndBlockSizeByType(byte rawEndBlockType)
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
        // These changed seemingly at random after completing a chapter
        // They even seem random on new files (no chapters played, not even prologue)
        // It's entirely possible this is RNG-related, to preserve random numbers after a save to mitigate mid-battle save scumming

        // 0x1F through 0x26
        private Stat _gainedStats;
        public Stat GainedStats
        {
            get { return _gainedStats; }
            set { _gainedStats = value; }
        }

        // 0x27 through 0x2E
        private Stat _statueBonusStats;
        public Stat StatueBonusStats
        {
            get { return _statueBonusStats; }
            set { _statueBonusStats = value; }
        }

        // Eight unknown bytes (0x2F through 0x36)

        // 0x37 through 0x3E
        private Stat _extraGainedStats;
        public Stat ExtraGainedStats
        {
            get { return _extraGainedStats; }
            set { _extraGainedStats = value; }
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

        //public byte NumberOfSupports
        //{
        //    get { return _rawBlock1[0x7C]; }
        //    set { _rawBlock1[0x7C] = value; }
        //}

        #endregion

        #region Block 2 Properties

        // Seven unknown bytes (0x00 through 0x06)

        public byte Boots
        {
            get { return _rawBlock2[0x07]; }
            set { _rawBlock2[0x07] = value; }
        }

        // Six unknown bytes (0x08 through 0x0D)

        /// <summary>
        /// Prisoner (generic captured unit) ID
        /// </summary>
        public byte PrisonerID
        {
            get { return _rawBlock2[0xE]; }
            set { _rawBlock2[0xE] = value; }
        }

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
        // First one seems to always be 00? Need to double-check
        // Next eight seem constant on a per-character basis. Corrin (M) and Sakura have the same values in my BR and RV saves
        // In fact, those eight are the same on all characters I've seen. Values: 24 82 25 21 24 C3 4A 16
        // Next four seem random...ish. Could be a separate RNG? Maybe for voice clips or something.

        /// <summary>Its purpose is unknown, but it always seems to be either 75, 76, or 77</summary>
        public byte UnknownSeventySomething
        {
            get { return _rawBlock2[0x24]; }
            set { _rawBlock2[0x24] = value; }
        }

        /// <summary>
        /// Stat bonuses via Tonic items.
        /// </summary>
        public Stat TonicBonusStats
        {
            get
            {
                Stat stats = new Stat()
                {
                    HP = _rawBlock2[0x2B].GetFlag(0x01) ? (sbyte)5 : (sbyte)0,
                    Str = _rawBlock2[0x2B].GetFlag(0x02) ? (sbyte)2 : (sbyte)0,
                    Mag = _rawBlock2[0x2B].GetFlag(0x04) ? (sbyte)2 : (sbyte)0,
                    Skl = _rawBlock2[0x2B].GetFlag(0x08) ? (sbyte)2 : (sbyte)0,
                    Spd = _rawBlock2[0x2B].GetFlag(0x10) ? (sbyte)2 : (sbyte)0,
                    Lck = _rawBlock2[0x2B].GetFlag(0x20) ? (sbyte)4 : (sbyte)0,
                    Def = _rawBlock2[0x2B].GetFlag(0x40) ? (sbyte)2 : (sbyte)0,
                    Res = _rawBlock2[0x2B].GetFlag(0x80) ? (sbyte)2 : (sbyte)0
                };
                return stats;
            }
            set
            {
                if (value.HP == 5) _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x01, true); else _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x01, false);
                if (value.Str == 2) _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x02, true); else _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x02, false);
                if (value.Mag == 2) _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x04, true); else _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x04, false);
                if (value.Skl == 2) _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x08, true); else _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x08, false);
                if (value.Spd == 2) _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x10, true); else _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x10, false);
                if (value.Lck == 4) _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x20, true); else _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x20, false);
                if (value.Def == 2) _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x40, true); else _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x40, false);
                if (value.Res == 2) _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x80, true); else _rawBlock2[0x2B] = _rawBlock2[0x2B].SetFlag(0x80, false);
            }
        }

        /// <summary>
        /// Random bonuses in My Castle.
        /// </summary>
        public Stat StatusBonusStats
        {
            get
            {
                Stat stats = new Stat()
                {
                    HP = 0,
                    Str = _rawBlock2[0x2C].GetFlag(0x02) ? (sbyte)4 : (sbyte)0,
                    Mag = _rawBlock2[0x2C].GetFlag(0x04) ? (sbyte)4 : (sbyte)0,
                    Skl = _rawBlock2[0x2C].GetFlag(0x08) ? (sbyte)4 : (sbyte)0,
                    Spd = _rawBlock2[0x2C].GetFlag(0x10) ? (sbyte)4 : (sbyte)0,
                    Lck = _rawBlock2[0x2C].GetFlag(0x20) ? (sbyte)4 : (sbyte)0,
                    Def = _rawBlock2[0x2C].GetFlag(0x40) ? (sbyte)4 : (sbyte)0,
                    Res = _rawBlock2[0x2C].GetFlag(0x80) ? (sbyte)4 : (sbyte)0
                };
                return stats;
            }
            set
            {
                //if (value.HP == 4) _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x01, true); else _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x01, false);
                if (value.Str == 4) _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x02, true); else _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x02, false);
                if (value.Mag == 4) _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x04, true); else _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x04, false);
                if (value.Skl == 4) _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x08, true); else _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x08, false);
                if (value.Spd == 4) _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x10, true); else _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x10, false);
                if (value.Lck == 4) _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x20, true); else _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x20, false);
                if (value.Def == 4) _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x40, true); else _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x40, false);
                if (value.Res == 4) _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x80, true); else _rawBlock2[0x2C] = _rawBlock2[0x2C].SetFlag(0x80, false);
            }
        }

        /// <summary>
        /// Stat bonuses via Mess Hall
        /// </summary>
        public Stat MealBonusStats
        {
            get
            {
                Stat stats = new Stat();
                if (_rawBlock2[0x2E].GetFlag(0x10)) stats.Str += 2;
                if (_rawBlock2[0x2E].GetFlag(0x20)) stats.Str -= 1;
                if (_rawBlock2[0x2E].GetFlag(0x40)) stats.Mag += 2;
                if (_rawBlock2[0x2E].GetFlag(0x80)) stats.Mag -= 1;
                if (_rawBlock2[0x2F].GetFlag(0x01)) stats.Skl += 2;
                if (_rawBlock2[0x2F].GetFlag(0x02)) stats.Skl -= 1;
                if (_rawBlock2[0x2F].GetFlag(0x04)) stats.Spd += 2;
                if (_rawBlock2[0x2F].GetFlag(0x08)) stats.Spd -= 1;
                if (_rawBlock2[0x2F].GetFlag(0x10)) stats.Lck += 2;
                if (_rawBlock2[0x2F].GetFlag(0x20)) stats.Lck -= 1;
                if (_rawBlock2[0x2F].GetFlag(0x40)) stats.Def += 2;
                if (_rawBlock2[0x2F].GetFlag(0x80)) stats.Def -= 1;
                return stats;
            }
            set
            {
                SetMealFlag(value.Str, 0x2E, 0x10, 0x20);
                SetMealFlag(value.Mag, 0x2E, 0x40, 0x80);
                SetMealFlag(value.Skl, 0x2F, 0x01, 0x02);
                SetMealFlag(value.Spd, 0x2F, 0x04, 0x08);
                SetMealFlag(value.Lck, 0x2F, 0x10, 0x20);
                SetMealFlag(value.Def, 0x2F, 0x40, 0x80);
            }
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

        public Enums.Accessory Headwear
        {
            get { return (Enums.Accessory)_rawBlock3[0x05]; }
            set { _rawBlock3[0x05] = (byte)value; }
        }

        public Enums.Accessory Facewear
        {
            get { return (Enums.Accessory)_rawBlock3[0x06]; }
            set { _rawBlock3[0x06] = (byte)value; }
        }

        public Enums.Accessory Armwear
        {
            get { return (Enums.Accessory)_rawBlock3[0x07]; }
            set { _rawBlock3[0x07] = (byte)value; }
        }

        public Enums.Accessory Underwear
        {
            get { return (Enums.Accessory)_rawBlock3[0x08]; }
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

        public Enums.Stat Boon
        {
            get
            {
                try { VerifyEndBlockSizeIfCorrin(); } catch (MissingFieldException) { return Enums.Stat.None; }
                return (Enums.Stat)_rawEndBlock[0x1A];
            }
            set
            {
                VerifyEndBlockSizeIfCorrin();
                _rawEndBlock[0x1A] = (byte)value;
            }
        }

        public Enums.Stat Bane
        {
            get
            {
                try { VerifyEndBlockSizeIfCorrin(); } catch (MissingFieldException) { return Enums.Stat.None; }
                return (Enums.Stat)_rawEndBlock[0x1B];
            }
            set
            {
                VerifyEndBlockSizeIfCorrin();
                _rawEndBlock[0x1B] = (byte)value;
            }
        }

        public byte[] AvatarHairColor
        {
            get
            {
                try { VerifyEndBlockSizeIfCorrin(); } catch (MissingFieldException) { return null; }
                return _rawEndBlock.Skip(0x22).Take(0x04).ToArray();
            }
            set
            {
                if (value.Length != 0x04) throw new ArgumentException("AvatarHairColor must be 0x04 bytes long");
                Array.Copy(value, 0x00, _rawEndBlock, 0x22, 0x04);
            }
        }

        private void VerifyEndBlockSizeIfCorrin()
        {
            if (_rawEndBlock.Length != GetRawEndBlockSizeByType(0x04)) throw new MissingFieldException("Field does not exist in this character's end block");
        }

        #endregion

        #region End Block Properties (Child)

        public Enums.Character FatherID
        {
            get
            {
                try { VerifyEndBlockSizeIfChild(); } catch (MissingFieldException) { return Enums.Character.None; }
                return (Enums.Character)((_rawEndBlock[0x4] << 8) | _rawEndBlock[0x3]);
            }
            set
            {
                VerifyEndBlockSizeIfChild();
                _rawEndBlock[0x3] = (byte)((ushort)value & 0xFF);
                _rawEndBlock[0x4] = (byte)(((ushort)value >> 8) & 0xFF);
            }
        }

        public Enums.Stat FatherBoon
        {
            get
            {
                try { VerifyEndBlockSizeIfChild(); } catch (MissingFieldException) { return Enums.Stat.None; }
                return (Enums.Stat)_rawEndBlock[0x5];
            }
            set
            {
                VerifyEndBlockSizeIfChild();
                _rawEndBlock[0x5] = (byte)value;
            }
        }

        public Enums.Stat FatherBane
        {
            get
            {
                try { VerifyEndBlockSizeIfChild(); } catch (MissingFieldException) { return Enums.Stat.None; }
                return (Enums.Stat)_rawEndBlock[0x6];
            }
            set
            {
                VerifyEndBlockSizeIfChild();
                _rawEndBlock[0x6] = (byte)value;
            }
        }

        public Enums.Character MotherID
        {
            get
            {
                try { VerifyEndBlockSizeIfChild(); } catch (MissingFieldException) { return Enums.Character.None; }
                return (Enums.Character)((_rawEndBlock[0x12] << 8) | _rawEndBlock[0x11]);
            }
            set
            {
                VerifyEndBlockSizeIfChild();
                _rawEndBlock[0x11] = (byte)((ushort)value & 0xFF);
                _rawEndBlock[0x12] = (byte)(((ushort)value >> 8) & 0xFF);
            }
        }

        public Enums.Stat MotherBoon
        {
            get
            {
                try { VerifyEndBlockSizeIfChild(); } catch (MissingFieldException) { return Enums.Stat.None; }
                return (Enums.Stat)_rawEndBlock[0x13];
            }
            set
            {
                VerifyEndBlockSizeIfChild();
                _rawEndBlock[0x13] = (byte)value;
            }
        }

        public Enums.Stat MotherBane
        {
            get
            {
                try { VerifyEndBlockSizeIfChild(); } catch (MissingFieldException) { return Enums.Stat.None; }
                return (Enums.Stat)_rawEndBlock[0x14];
            }
            set
            {
                VerifyEndBlockSizeIfChild();
                _rawEndBlock[0x14] = (byte)value;
            }
        }

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

        private void SetMealFlag(sbyte value, int offset, byte mask1, byte mask2)
        {
            switch (value)
            {
                case 2:
                    _rawBlock2[offset] = _rawBlock2[offset].SetFlag(mask1, true);
                    _rawBlock2[offset] = _rawBlock2[offset].SetFlag(mask2, false);
                    break;
                case 1:
                    _rawBlock2[offset] = _rawBlock2[offset].SetFlag(mask1, true);
                    _rawBlock2[offset] = _rawBlock2[offset].SetFlag(mask2, true);
                    break;
                case -1:
                    _rawBlock2[offset] = _rawBlock2[offset].SetFlag(mask1, false);
                    _rawBlock2[offset] = _rawBlock2[offset].SetFlag(mask2, true);
                    break;
                default:
                    _rawBlock2[offset] = _rawBlock2[offset].SetFlag(mask1, false);
                    _rawBlock2[offset] = _rawBlock2[offset].SetFlag(mask2, false);
                    break;
            }
        }

        #endregion

        #region Boundary Enforcement

        public static byte MaxEternalSealsUsed = 0x2F;
        public static byte MinWeaponExperience = 0x01;
        public static byte WeaponExperienceForRankE = 1;
        public static byte WeaponExperienceForRankD = 21;
        public static byte WeaponExperienceForRankC = 51;
        public static byte WeaponExperienceForRankB = 96;
        public static byte WeaponExperienceForRankA = 161;
        public static byte WeaponExperienceForRankS = 251;
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

        #region Skill Helpers and Cheats

        public void UnequipUnlearnedSkills()
        {
            if (!LearnedSkills.Contains(EquippedSkill_1)) EquippedSkill_1 = Enums.Skill.None;
            if (!LearnedSkills.Contains(EquippedSkill_2)) EquippedSkill_2 = Enums.Skill.None;
            if (!LearnedSkills.Contains(EquippedSkill_3)) EquippedSkill_3 = Enums.Skill.None;
            if (!LearnedSkills.Contains(EquippedSkill_4)) EquippedSkill_4 = Enums.Skill.None;
            if (!LearnedSkills.Contains(EquippedSkill_5)) EquippedSkill_5 = Enums.Skill.None;

            CollapseEquippedSkills();
        }

        /// <summary>
        /// Shifts Nones from the middle of the skill list to the bottom
        /// </summary>
        public void CollapseEquippedSkills()
        {
            if (EquippedSkill_4 == Enums.Skill.None)
            {
                EquippedSkill_4 = EquippedSkill_5;
                EquippedSkill_5 = Enums.Skill.None;
            }
            if (EquippedSkill_3 == Enums.Skill.None)
            {
                EquippedSkill_3 = EquippedSkill_4;
                EquippedSkill_4 = EquippedSkill_5;
                EquippedSkill_5 = Enums.Skill.None;
            }
            if (EquippedSkill_2 == Enums.Skill.None)
            {
                EquippedSkill_2 = EquippedSkill_3;
                EquippedSkill_3 = EquippedSkill_4;
                EquippedSkill_4 = EquippedSkill_5;
                EquippedSkill_5 = Enums.Skill.None;
            }
            if (EquippedSkill_1 == Enums.Skill.None)
            {
                EquippedSkill_1 = EquippedSkill_2;
                EquippedSkill_2 = EquippedSkill_3;
                EquippedSkill_3 = EquippedSkill_4;
                EquippedSkill_4 = EquippedSkill_5;
                EquippedSkill_5 = Enums.Skill.None;
            }
        }

        public void LearnNormalClassSkills()
        {
            var learnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsNormalClassSkill);
            foreach (var skill in learnThese) LearnedSkills.Add(skill.SkillID);
        }

        public void LearnCorrinOnlySkills()
        {
            var learnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsCorrinOnlySkill);
            foreach (var skill in learnThese) LearnedSkills.Add(skill.SkillID);
        }

        public void LearnAzuraOnlySkills()
        {
            var learnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsAzuraOnlySkill);
            foreach (var skill in learnThese) LearnedSkills.Add(skill.SkillID);
        }

        public void LearnBeastOnlySkills()
        {
            var learnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsKitsuneOnlySkill && x.IsWolfskinOnlySkill);
            foreach (var skill in learnThese) LearnedSkills.Add(skill.SkillID);
        }

        public void LearnKitsuneOnlySkills()
        {
            var learnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsKitsuneOnlySkill && !x.IsWolfskinOnlySkill);
            foreach (var skill in learnThese) LearnedSkills.Add(skill.SkillID);
        }

        public void LearnWolfskinOnlySkills()
        {
            var learnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsWolfskinOnlySkill && !x.IsKitsuneOnlySkill);
            foreach (var skill in learnThese) LearnedSkills.Add(skill.SkillID);
        }

        public void LearnVillagerOnlySkills()
        {
            var learnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsVillagerOnlySkill);
            foreach (var skill in learnThese) LearnedSkills.Add(skill.SkillID);
        }

        public void LearnPathBonusClassSkills()
        {
            var learnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsPathBonusClassSkill);
            foreach (var skill in learnThese) LearnedSkills.Add(skill.SkillID);
        }

        public void LearnDlcClassSkills()
        {
            var learnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsDlcClassSkill && !x.IsAmiiboClassSkill);
            foreach (var skill in learnThese) LearnedSkills.Add(skill.SkillID);
        }

        public void LearnAmiiboClassSkills()
        {
            var learnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsAmiiboClassSkill);
            foreach (var skill in learnThese) LearnedSkills.Add(skill.SkillID);
        }

        public void LearnEnemyAndNpcSkills()
        {
            var learnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsEnemyAndNpcSkill);
            foreach (var skill in learnThese) LearnedSkills.Add(skill.SkillID);
        }

        public void LearnAllSkills()
        {
            var learnThese = Data.Database.Skills.GetAllLearnable();
            foreach (var skill in learnThese) LearnedSkills.Add(skill.SkillID);
        }

        public void UnlearnNormalClassSkills()
        {
            var unlearnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsNormalClassSkill);
            foreach (var skill in unlearnThese) LearnedSkills.Remove(skill.SkillID);
            UnequipUnlearnedSkills();
        }

        public void UnlearnCorrinOnlySkills()
        {
            var unlearnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsCorrinOnlySkill);
            foreach (var skill in unlearnThese) LearnedSkills.Remove(skill.SkillID);
            UnequipUnlearnedSkills();
        }

        public void UnlearnAzuraOnlySkills()
        {
            var unlearnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsAzuraOnlySkill);
            foreach (var skill in unlearnThese) LearnedSkills.Remove(skill.SkillID);
            UnequipUnlearnedSkills();
        }

        public void UnlearnBeastOnlySkills()
        {
            var unlearnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsKitsuneOnlySkill && x.IsWolfskinOnlySkill);
            foreach (var skill in unlearnThese) LearnedSkills.Remove(skill.SkillID);
            UnequipUnlearnedSkills();
        }

        public void UnlearnKitsuneOnlySkills()
        {
            var unlearnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsKitsuneOnlySkill && !x.IsWolfskinOnlySkill);
            foreach (var skill in unlearnThese) LearnedSkills.Remove(skill.SkillID);
            UnequipUnlearnedSkills();
        }

        public void UnlearnWolfskinOnlySkills()
        {
            var unlearnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsWolfskinOnlySkill && !x.IsKitsuneOnlySkill);
            foreach (var skill in unlearnThese) LearnedSkills.Remove(skill.SkillID);
            UnequipUnlearnedSkills();
        }

        public void UnlearnVillagerOnlySkills()
        {
            var unlearnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsVillagerOnlySkill);
            foreach (var skill in unlearnThese) LearnedSkills.Remove(skill.SkillID);
            UnequipUnlearnedSkills();
        }

        public void UnlearnPathBonusClassSkills()
        {
            var unlearnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsPathBonusClassSkill);
            foreach (var skill in unlearnThese) LearnedSkills.Remove(skill.SkillID);
            UnequipUnlearnedSkills();
        }

        public void UnlearnDlcClassSkills()
        {
            var unlearnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsDlcClassSkill && !x.IsAmiiboClassSkill);
            foreach (var skill in unlearnThese) LearnedSkills.Remove(skill.SkillID);
            UnequipUnlearnedSkills();
        }

        public void UnlearnAmiiboClassSkills()
        {
            var unlearnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsAmiiboClassSkill);
            foreach (var skill in unlearnThese) LearnedSkills.Remove(skill.SkillID);
            UnequipUnlearnedSkills();
        }

        public void UnlearnEnemyAndNpcSkills()
        {
            var unlearnThese = Data.Database.Skills.GetAllLearnable().Where((x) => x.IsEnemyAndNpcSkill);
            foreach (var skill in unlearnThese) LearnedSkills.Remove(skill.SkillID);
            UnequipUnlearnedSkills();
        }

        public void UnlearnAllSkills()
        {
            var unlearnThese = Data.Database.Skills.GetAllLearnable();
            foreach (var skill in unlearnThese) LearnedSkills.Remove(skill.SkillID);
            UnequipUnlearnedSkills();
        }

        #endregion

        #region Other Cheats

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
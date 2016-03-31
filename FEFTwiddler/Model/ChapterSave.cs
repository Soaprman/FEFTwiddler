using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model
{
    class ChapterSave: ISave
    {
        #region Member variables

        private string _filePath;

        /// <summary>You know, just so we're clear!</summary>
        private long _headerOffset = 0;
        /// <summary>Hex: 0xFFFFFFFF / Text: ÿÿÿÿ</summary>
        /// <remarks>Yeah, that's a super suspect marker. It's the first FFFFFFFF in the file though. I hope.</remarks>
        private byte[] _fileDataMarker = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
        private long _fileDataOffset;
        /// <summary>Hex: 0x54494E55 / Text: TINU</summary>
        private byte[] _characterMarker = new byte[] { 0x54, 0x49, 0x4E, 0x55 };
        private long _characterOffset;
        /// <summary>Hex: 0x49464552 / Text: IFER</summary>
        private byte[] _weaponNameMarker = new byte[] { 0x49, 0x46, 0x45, 0x52 };
        private long _weaponNameOffset;
        /// <summary>Hex: 0x4E415254 / Text: NART</summary>
        private byte[] _convoyMarker = new byte[] { 0x4E, 0x41, 0x52, 0x54 };
        private long _convoyOffset;
        /// <summary>Hex: 0x34334143 / Text: 43AC</summary>
        private byte[] _myCastleMarker = new byte[] { 0x34, 0x33, 0x41, 0x43 };
        private long _myCastleOffset;

        #endregion

        #region Properties

        public byte[] AvatarName { get; set; }

        public uint Gold { get; set; }

        public Enums.Difficulty Difficulty { get; set; }
        public Enums.Ruleset Ruleset { get; set; }

        public byte MaterialQuantity_Crystal { get; set; }
        public byte MaterialQuantity_Ruby { get; set; }
        public byte MaterialQuantity_Sapphire { get; set; }
        public byte MaterialQuantity_Onyx { get; set; }
        public byte MaterialQuantity_Emerald { get; set; }
        public byte MaterialQuantity_Topaz { get; set; }
        public byte MaterialQuantity_Pearl { get; set; }
        public byte MaterialQuantity_Coral { get; set; }
        public byte MaterialQuantity_Lapis { get; set; }
        public byte MaterialQuantity_Quartz { get; set; }
        public byte MaterialQuantity_Jade { get; set; }
        public byte MaterialQuantity_Amber { get; set; }
        public byte MaterialQuantity_Meat { get; set; }
        public byte MaterialQuantity_Milk { get; set; }
        public byte MaterialQuantity_Cabbage { get; set; }
        public byte MaterialQuantity_Berries { get; set; }
        public byte MaterialQuantity_Wheat { get; set; }
        public byte MaterialQuantity_Beans { get; set; }
        public byte MaterialQuantity_Fish { get; set; }
        public byte MaterialQuantity_Daikon { get; set; }
        public byte MaterialQuantity_Peaches { get; set; }
        public byte MaterialQuantity_Rice { get; set; }

        private List<Character> _characters = new List<Character>();
        public List<Character> Characters {
            get
            {
                return _characters;
            }
        }

        #endregion

        #region Cheats

        public void MaximizeGold()
        {
            Gold = 0xF423F; // 999,999
        }

        public void MaximizeMaterials()
        {
            byte max = 0x63; // 99
            MaterialQuantity_Crystal = max;
            MaterialQuantity_Ruby = max;
            MaterialQuantity_Sapphire = max;
            MaterialQuantity_Onyx = max;
            MaterialQuantity_Emerald = max;
            MaterialQuantity_Topaz = max;
            MaterialQuantity_Pearl = max;
            MaterialQuantity_Coral = max;
            MaterialQuantity_Lapis = max;
            MaterialQuantity_Quartz = max;
            MaterialQuantity_Jade = max;
            MaterialQuantity_Amber = max;
            MaterialQuantity_Meat = max;
            MaterialQuantity_Milk = max;
            MaterialQuantity_Cabbage = max;
            MaterialQuantity_Berries = max;
            MaterialQuantity_Wheat = max;
            MaterialQuantity_Beans = max;
            MaterialQuantity_Fish = max;
            MaterialQuantity_Daikon = max;
            MaterialQuantity_Peaches = max;
            MaterialQuantity_Rice = max;
        }

        #endregion

        public static ChapterSave FromPath(string path)
        {
            var cs = new ChapterSave();
            cs.Read(path);
            return cs;
        }

        #region General IO

        public void Read(string path)
        {
            _filePath = path;

            using (var fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                ReadHeaderData(br);

                br.AdvanceToValue(_fileDataMarker);
                _fileDataOffset = br.BaseStream.Position;
                ReadFileData(br);

                br.AdvanceToValue(_characterMarker);
                _characterOffset = br.BaseStream.Position;
                ReadCharacters(br);

                br.AdvanceToValue(_myCastleMarker);
                _myCastleOffset = br.BaseStream.Position;
                ReadMyCastle(br);
            }
        }

        public void Write()
        {
            using (var fs = new FileStream(_filePath, FileMode.Open, FileAccess.ReadWrite))
            using (var bw = new BinaryWriter(fs))
            {
                WriteFileData(bw);
                WriteCharacters(bw);
                WriteMyCastle(bw);
            }
        }

        #endregion

        #region Header IO

        public void ReadHeaderData(BinaryReader br)
        {
            byte[] chunk;

            // Stuff
            br.ReadBytes(0x0F);

            // Avatar name
            chunk = new byte[24];
            br.Read(chunk, 0, 24);

            AvatarName = chunk;

            // Not sure about the rest. Guess it's a TODO.
        }

        #endregion

        #region File Data IO

        public void ReadFileData(BinaryReader br)
        {
            byte[] chunk;

            // Stuff
            br.ReadBytes(0x02);

            // Ruleset
            chunk = new byte[1];
            br.Read(chunk, 0, 1);

            Ruleset = (Enums.Ruleset)chunk[0];

            // Stuff
            br.ReadBytes(0x04);

            // Difficulty
            chunk = new byte[1];
            br.Read(chunk, 0, 1);

            Difficulty = (Enums.Difficulty)chunk[0];

            // Stuff
            br.ReadBytes(0x03);

            // Gold
            chunk = new byte[4];
            br.Read(chunk, 0, 4);

            Gold = BitConverter.ToUInt32(chunk, 0);

            // Not sure about the rest. Guess it's a TODO.
        }

        public void WriteFileData(BinaryWriter bw)
        {
            byte[] chunk;

            bw.BaseStream.Seek(_fileDataOffset, SeekOrigin.Begin);

            // Stuff
            bw.BaseStream.Seek(0x02, SeekOrigin.Current);

            // Ruleset
            bw.BaseStream.Seek(0x01, SeekOrigin.Current);

            // Stuff
            bw.BaseStream.Seek(0x04, SeekOrigin.Current);

            // Difficulty
            bw.BaseStream.Seek(0x01, SeekOrigin.Current);

            // Stuff
            bw.BaseStream.Seek(0x03, SeekOrigin.Current);

            // Gold
            bw.Write(Gold);

            // Not sure about the rest. Guess it's a TODO.
        }

        #endregion

        #region Character IO

        private void ReadCharacters(BinaryReader br)
        {

            // Probably a marker to begin the "living units" block.
            // 01 03 on my save
            br.ReadBytes(2);

            // The number of living characters to read
            var livingCharacters = br.ReadByte();

            for (var i = 0; i < livingCharacters; i++)
            {
                ReadCurrentCharacter(br);
            }

            // Probably a marker to begin the "dead units" block.
            // 06 on my save
            br.ReadBytes(1);

            // The number of dead characters to read
            var deadCharacters = br.ReadByte();

            for (var i = 0; i < deadCharacters; i++)
            {
                ReadCurrentCharacter(br);
            }
        }

        private void ReadCurrentCharacter(BinaryReader br)
        {
            byte[] chunk;

            var character = new Character();

            // To make writing easier
            character.BinaryPosition = br.BaseStream.Position;

            // TODO
            br.ReadBytes(9);

            // Character main data
            chunk = new byte[8];
            br.Read(chunk, 0, 8);

            character.Level = chunk[0];
            character.Experience = chunk[1];
            character.Unknown00C = chunk[2];
            character.EternalSealsUsed = chunk[3];
            character.CharacterID = (Enums.Character)chunk[4];
            character.Unknown00F = chunk[5];
            character.ClassID = (Enums.Class)chunk[6];
            character.Unknown011 = chunk[7];

            // TODO
            br.ReadBytes(46);

            // Weapon exp and HP
            chunk = new byte[9];
            br.Read(chunk, 0, 9);

            character.WeaponExperience_Sword = chunk[0];
            character.WeaponExperience_Lance = chunk[1];
            character.WeaponExperience_Axe = chunk[2];
            character.WeaponExperience_Shuriken = chunk[3];
            character.WeaponExperience_Bow = chunk[4];
            character.WeaponExperience_Tome = chunk[5];
            character.WeaponExperience_Staff = chunk[6];
            character.WeaponExperience_Stone = chunk[7];
            character.MaximumHP = chunk[8];

            // Filler - FF FF FF FF
            br.ReadBytes(4);

            // Flags block (shield, dead, etc)
            chunk = new byte[5];
            br.Read(chunk, 0, 5);

            character.IsDead = chunk[0] == 0x18; // Scarlet
            character.IsEinherjar = chunk[1] == 0x01; // Quatro, generic einherjar
            //character.IsRecruited = chunk[3] == 0x30; // Captured bosses
            //character.IsRecruited = chunk[3] == 0x18; // Quatro, generic einherjar
            //character.IsRecruited = chunk[3] == 0x38; // Named einherjars (Niles, etc)
            character.IsRecruited = (chunk[3] & 0x10) == 0x10;

            // Filler - 00 00 00 FF FF 00
            br.ReadBytes(6);

            // TODO
            br.ReadBytes(2);

            // Skills
            chunk = new byte[10];
            br.Read(chunk, 0, 10);

            character.EquippedSkill_1 = (Enums.Skill)chunk[0];
            character.EquippedSkill_2 = (Enums.Skill)chunk[2];
            character.EquippedSkill_3 = (Enums.Skill)chunk[4];
            character.EquippedSkill_4 = (Enums.Skill)chunk[6];
            character.EquippedSkill_5 = (Enums.Skill)chunk[8];

            // Inventory
            br.ReadBytes(25);

            // Supports
            byte supportCount = br.ReadByte();
            if (supportCount > 0)
            {
                chunk = new byte[supportCount];
                br.Read(chunk, 0, supportCount);
                character.MainSupports = chunk;
            }

            // TODO
            br.ReadBytes(7);

            // Boots
            character.Boots = br.ReadByte();

            // TODO
            br.ReadBytes(9);

            // DLC class flags
            chunk = new byte[2];
            br.Read(chunk, 0, 2);
            character.DLCClassFlags = chunk;

            // Hair color
            chunk = new byte[4];
            br.Read(chunk, 0, 4);
            character.HairColor = chunk;

            // TODO
            br.ReadBytes(47);

            // Learned skills
            chunk = new byte[20];
            br.Read(chunk, 0, 20);

            character.LearnedSkills = chunk;

            // TODO
            br.ReadBytes(5);

            // Accessories
            chunk = new byte[4];
            br.Read(chunk, 0, 4);

            character.Headwear = (Enums.Headwear)chunk[0];
            character.Facewear = (Enums.Facewear)chunk[1];
            character.Armwear = (Enums.Armwear)chunk[2];
            character.Underwear = (Enums.Underwear)chunk[3];

            // TODO
            br.ReadBytes(10);

            // Determine end block size
            int endBlockSize;
            byte endByte = br.ReadByte();
            switch (endByte)
            {
                case 0x04:
                    endBlockSize = 44;
                    break;
                case 0x01:
                    endBlockSize = 42;
                    break;
                default:
                    endBlockSize = 0;
                    break;
            }

            // TODO
            br.ReadBytes(endBlockSize);

            _characters.Add(character);
        }

        private void WriteCharacters(BinaryWriter bw)
        {
            foreach (var character in _characters)
            {
                WriteCurrentCharacter(bw, character);
            }
        }

        private void WriteCurrentCharacter(BinaryWriter bw, Model.Character character)
        {
            byte[] chunk;

            bw.BaseStream.Seek(character.BinaryPosition, SeekOrigin.Begin);

            // TODO
            bw.BaseStream.Seek(9, SeekOrigin.Current);

            // Character main data
            chunk = new byte[] {
                character.Level,
                character.Experience,
                character.Unknown00C,
                character.EternalSealsUsed,
                (byte)character.CharacterID,
                character.Unknown00F,
                (byte)character.ClassID,
                character.Unknown011
            };
            bw.Write(chunk);

            // TODO
            bw.BaseStream.Seek(46, SeekOrigin.Current);

            // Weapon exp
            chunk = new byte[] {
                character.WeaponExperience_Sword,
                character.WeaponExperience_Lance,
                character.WeaponExperience_Axe,
                character.WeaponExperience_Shuriken,
                character.WeaponExperience_Bow,
                character.WeaponExperience_Tome,
                character.WeaponExperience_Staff,
                character.WeaponExperience_Stone
            };
            bw.Write(chunk);

            // Max HP
            bw.BaseStream.Seek(1, SeekOrigin.Current);

            // Filler - FF FF FF FF
            bw.BaseStream.Seek(4, SeekOrigin.Current);

            // Flags block (shield, dead, etc)
            bw.BaseStream.Seek(5, SeekOrigin.Current);

            // Filler - 00 00 00 FF FF 00
            bw.BaseStream.Seek(6, SeekOrigin.Current);

            // TODO
            bw.BaseStream.Seek(2, SeekOrigin.Current);

            // Skills
            bw.BaseStream.Seek(10, SeekOrigin.Current);

            // Inventory
            bw.BaseStream.Seek(25, SeekOrigin.Current);

            // Supports
            int supportCount = bw.BaseStream.ReadByte();
            if (supportCount > 0)
            {
                bw.BaseStream.Seek(supportCount, SeekOrigin.Current);
            }

            // TODO
            bw.BaseStream.Seek(7, SeekOrigin.Current);

            // Boots
            bw.Write(character.Boots);

            // TODO
            bw.BaseStream.Seek(9, SeekOrigin.Current);

            // DLC class flags
            bw.Write(character.DLCClassFlags);

            // Hair color
            bw.Write(character.HairColor);

            // TODO
            bw.BaseStream.Seek(47, SeekOrigin.Current);

            // Learned skills
            bw.Write(character.LearnedSkills);

            // TODO
            bw.BaseStream.Seek(5, SeekOrigin.Current);

            // Accessories
            chunk = new byte[] {
                (byte)character.Headwear,
                (byte)character.Facewear,
                (byte)character.Armwear,
                (byte)character.Underwear
            };
            bw.Write(chunk);

            // TODO
            bw.BaseStream.Seek(10, SeekOrigin.Current);

            // Determine end block size
            int endBlockSize;
            int endByte = bw.BaseStream.ReadByte();
            switch (endByte)
            {
                case 0x04:
                    endBlockSize = 44;
                    break;
                case 0x01:
                    endBlockSize = 42;
                    break;
                default:
                    endBlockSize = 0;
                    break;
            }

            // TODO
            bw.BaseStream.Seek(endBlockSize, SeekOrigin.Current);
        }

        #endregion

        #region My Castle IO

        public void ReadMyCastle(BinaryReader br)
        {
            byte[] chunk;

            // Stuff
            br.ReadBytes(0x6C);

            // Castle name (size is a guess)
            br.ReadBytes(0x20);

            // Stuff
            br.ReadBytes(0x19D);

            // Materials
            chunk = new byte[22];
            br.Read(chunk, 0, 22);

            MaterialQuantity_Crystal = chunk[0];
            MaterialQuantity_Ruby = chunk[1];
            MaterialQuantity_Sapphire = chunk[2];
            MaterialQuantity_Onyx = chunk[3];
            MaterialQuantity_Emerald = chunk[4];
            MaterialQuantity_Topaz = chunk[5];
            MaterialQuantity_Pearl = chunk[6];
            MaterialQuantity_Coral = chunk[7];
            MaterialQuantity_Lapis = chunk[8];
            MaterialQuantity_Quartz = chunk[9];
            MaterialQuantity_Jade = chunk[10];
            MaterialQuantity_Amber = chunk[11];
            MaterialQuantity_Meat = chunk[12];
            MaterialQuantity_Milk = chunk[13];
            MaterialQuantity_Cabbage = chunk[14];
            MaterialQuantity_Berries = chunk[15];
            MaterialQuantity_Wheat = chunk[16];
            MaterialQuantity_Beans = chunk[17];
            MaterialQuantity_Fish = chunk[18];
            MaterialQuantity_Daikon = chunk[19];
            MaterialQuantity_Peaches = chunk[20];
            MaterialQuantity_Rice = chunk[21];

            // Not sure about the rest. Guess it's a TODO.
        }

        public void WriteMyCastle(BinaryWriter bw)
        {
            byte[] chunk;

            bw.BaseStream.Seek(_myCastleOffset, SeekOrigin.Begin);

            // Stuff
            bw.BaseStream.Seek(0x6C, SeekOrigin.Current);

            // Castle name (size is a guess)
            bw.BaseStream.Seek(0x20, SeekOrigin.Current);

            // Stuff
            bw.BaseStream.Seek(0x19D, SeekOrigin.Current);

            // Materials
            chunk = new byte[] {
                MaterialQuantity_Crystal,
                MaterialQuantity_Ruby,
                MaterialQuantity_Sapphire,
                MaterialQuantity_Onyx,
                MaterialQuantity_Emerald,
                MaterialQuantity_Topaz,
                MaterialQuantity_Pearl,
                MaterialQuantity_Coral,
                MaterialQuantity_Lapis,
                MaterialQuantity_Quartz,
                MaterialQuantity_Jade,
                MaterialQuantity_Amber,
                MaterialQuantity_Meat,
                MaterialQuantity_Milk,
                MaterialQuantity_Cabbage,
                MaterialQuantity_Berries,
                MaterialQuantity_Wheat,
                MaterialQuantity_Beans,
                MaterialQuantity_Fish,
                MaterialQuantity_Daikon,
                MaterialQuantity_Peaches,
                MaterialQuantity_Rice
            };
            bw.Write(chunk);

            // Not sure about the rest. Guess it's a TODO.
        }

        #endregion
    }
}

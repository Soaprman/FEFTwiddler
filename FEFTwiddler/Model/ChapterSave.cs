﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model
{
    /// <summary>
    /// A Fire Emblem Fates "Chapter" save
    /// </summary>
    /// 

    public class ChapterSave : ISave
    {
        #region Member variables

        private SaveFile _file;

        /// <summary>You know, just so we're clear!</summary>
        private long _headerOffset = 0;
        /// <summary>Hex: 0x52455355 / Text: RESU</summary>
        private byte[] _userMarker = new byte[] { 0x52, 0x45, 0x53, 0x55 };
        private long _userOffset;
        /// <summary>Hex: 0x30000000FFFF / Text: 0...ÿÿ</summary>
        /// <remarks>This is some hacky shit right here. It sure would be nice to figure out the size of the USER block...</remarks>
        private byte[] _fileDataMarker = new byte[] { 0x30, 0x00, 0x00, 0x00, 0xFF, 0xFF };
        private long _fileDataOffset;
        /// <summary>Hex: 0x504F4853 / Text: POHS</summary>
        private byte[] _shopMarker = new byte[] { 0x50, 0x4F, 0x48, 0x53 };
        private long _shopOffset;
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
        public byte[] chunk { get; set; }

        public uint Gold { get; set; }

        public Enums.Difficulty Difficulty { get; set; }
        public Enums.Ruleset Ruleset { get; set; }

        public ushort DragonVeinPoint { get; set; }

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

        public uint BattlePoints { get; set; }
        public uint VisitPoints { get; set; }

        public Enums.Map CurrentMap { get; set; }

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

        public static ChapterSave FromSaveFile(SaveFile file)
        {
            var cs = new ChapterSave();
            cs.ReadFromFile(file);
            return cs;
        }

        #region General IO

        private void ReadFromFile(SaveFile file)
        {
            _file = file;
            Read();
        }

        public void Read()
        {
            using (var ms = new MemoryStream(_file.DecompressedBytes))
            using (var br = new BinaryReader(ms))
            {
                ReadHeaderData(br);

                br.AdvanceToValue(_userMarker);
                _fileDataOffset = br.BaseStream.Position;
                ReadUserData(br);

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
            using (var ms = new MemoryStream(_file.DecompressedBytes))
            using (var bw = new BinaryWriter(ms))
            {
                WriteFileData(bw);
                WriteCharacters(bw);
                WriteMyCastle(bw);
            }

            _file.Write();
        }

        #endregion

        #region Header IO

        public void ReadHeaderData(BinaryReader br)
        {
 

            // Stuff
            br.ReadBytes(0x0F);

            // Avatar name
            chunk = new byte[24];
            br.Read(chunk, 0, 24);

            AvatarName = chunk;

            // Not sure about the rest. Guess it's a TODO.
        }

        #endregion

        #region User Data IO

        public void ReadUserData(BinaryReader br)
        {
    
            // Stuff
            br.ReadBytes(0x09);

            // Map
            chunk = new byte[1];
            br.Read(chunk, 0, 1);

            // TODO: Add this once the enum is better fleshed-out
            //CurrentMap = (Enums.Map)chunk[0];

            // Not sure about the rest. Guess it's a TODO.
        }

        #endregion

        #region File Data IO

        public void ReadFileData(BinaryReader br)
        {
              // Stuff
            br.ReadBytes(0x04);

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


            bw.BaseStream.Seek(_fileDataOffset, SeekOrigin.Begin);

            // Stuff
            bw.BaseStream.Seek(0x04, SeekOrigin.Current);

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
            // 01
            br.ReadBytes(1);

            bool stillReading = true;
            while (stillReading)
            {
                // What's next?
                var nextBlock = br.ReadByte();

                switch (nextBlock)
                {
                    case 0x00: // Deployed living units (battle prep only)
                    case 0x03: // Undeployed living units (all living units in my castle)
                    case 0x06: // Dead units
                        var characterCount = br.ReadByte();
                        for (var i = 0; i < characterCount; i++)
                        {
                            ReadCurrentCharacter(br);
                        }
                        break;
                    case 0xFF: // End of unit block
                    default: // Just a failsafe
                        stillReading = false;
                        break;
                }
            }
        }

        private void ReadCurrentCharacter(BinaryReader br)
        {
            var character = new Character();

            // To make writing easier
            character.BinaryPosition = br.BaseStream.Position;

            // TODO
            br.ReadBytes(1);

            // Read flags
            byte[] flags = new byte[8];
            br.Read(flags, 0, 8);

            // Character main data
            chunk = new byte[8];
            br.Read(chunk, 0, 8);

            character.Level = chunk[0];
            character.Experience = chunk[1];
            character.InternalLevel = chunk[2];
            character.EternalSealsUsed = chunk[3];
            character.CharacterID = (Enums.Character)(ushort)(chunk[4] + chunk[5] * 0x100);
            character.ClassID = (Enums.Class)chunk[6];
            character.Unknown011 = chunk[7];

            // Process flags
            // character._IsCorrin = (chunk[0] & 0x01) == 0x01;
            character.IsManakete = Model.Character.IsCorrin(character.CharacterID) ||
                ((flags[2] & 0x80) == 0x80);
            character.IsBeast = Model.Character.IsBeastCharacter(character.CharacterID) ||
                (flags[3] & 0x01) == 0x01;
            character.CanUseDragonVein = Model.Character.IsRoyal(character.CharacterID) ||
                ((flags[4] & 0x08) == 0x08);

            // Some bytes
            chunk = new byte[2];
            br.Read(chunk, 0, 2);
            character.UnknownBytesBeforeStatBytes1 = chunk;

            // Some bytes
            chunk = new byte[12];
            br.Read(chunk, 0, 12);
            character.UnknownBytesBeforeStatBytes2 = chunk;

            // Stat bytes 1
            chunk = new byte[8];
            br.Read(chunk, 0, 8);
            character.StatBytes1 = (sbyte[])(Array)chunk;

            // Statue bonuses
            chunk = new byte[8];
            br.Read(chunk, 0, 8);
            character.StatueBonuses = chunk;

            // Some bytes
            chunk = new byte[8];
            br.Read(chunk, 0, 8);
            character.UnknownBytesBetweenStatBytes = chunk;

            // Stat bytes 2
            chunk = new byte[8];
            br.Read(chunk, 0, 8);
            character.StatBytes2 = (sbyte[])(Array)chunk;

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

            // Map position (battle prep only)
            chunk = new byte[2];
            br.Read(chunk, 0, 2);
            character.Position_FromLeft = chunk[0];
            character.Position_FromTop = chunk[0];
            character.IsDeployed = character.Position_FromLeft != 0xFF || character.Position_FromTop != 0xFF; // There might be an actual flag for this, but this works too

            // Filler - FF FF
            br.ReadBytes(2);

            // Flags block (shield, dead, etc)
            chunk = new byte[5];
            br.Read(chunk, 0, 5);

            character.IsDead = chunk[0] == 0x18; // Scarlet
            character.IsEinherjar = (chunk[3] & 0x08) == 0x08;
            //character.IsEinherjar = chunk[1] == 0x01; // Quatro, generic einherjar
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
            br.ReadBytes(1);
            chunk = new byte[4];
            br.Read(chunk, 0, 4);
            character.Item_1 = new InventoryItem(chunk);
            br.ReadBytes(1);
            chunk = new byte[4];
            br.Read(chunk, 0, 4);
            character.Item_2 = new InventoryItem(chunk);
            br.ReadBytes(1);
            chunk = new byte[4];
            br.Read(chunk, 0, 4);
            character.Item_3 = new InventoryItem(chunk);
            br.ReadBytes(1);
            chunk = new byte[4];
            br.Read(chunk, 0, 4);
            character.Item_4 = new InventoryItem(chunk);
            br.ReadBytes(1);
            chunk = new byte[4];
            br.Read(chunk, 0, 4);
            character.Item_5 = new InventoryItem(chunk);

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

            character.LearnedSkills = new LearnedSkills(chunk);

            // Extra data on deployed characters in battle prep saves.
            // Might contain debuffs, status effects, and other battle-specific status info
            // This is what it looks like in my "030_Chapter1" save:
            // 00 00 00 00 00 FF FF 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF
            if (character.IsDeployed)
            {
                br.ReadBytes(54);
            }

            // TODO (this is the 02 that comes after the learned skills block)
            br.ReadBytes(1);

            // TODO
            br.ReadBytes(4);

            // Accessories
            chunk = new byte[4];
            br.Read(chunk, 0, 4);

            character.Headwear = (Enums.Headwear)chunk[0];
            character.Facewear = (Enums.Facewear)chunk[1];
            character.Armwear = (Enums.Armwear)chunk[2];
            character.Underwear = (Enums.Underwear)chunk[3];

            // TODO
            br.ReadBytes(1);

            // Battles and Victories
            chunk = new byte[4];
            br.Read(chunk, 0, 4);
            character.BattleCount = (ushort)((chunk[1] << 8) | chunk[0]);
            character.VictoryCount = (ushort)((chunk[3] << 8) | chunk[2]);

            // TODO
            br.ReadBytes(5);

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


            bw.BaseStream.Seek(character.BinaryPosition, SeekOrigin.Begin);

            // TODO
            bw.BaseStream.Seek(1, SeekOrigin.Current);

            // Flags
            chunk = new byte[8];
            bw.BaseStream.Read(chunk, 0, 8);
            if (character.IsManakete && !Model.Character.IsCorrin(character.CharacterID))
                chunk[2] |= 0x80;
            else
                chunk[2] &= 0x7F;
            if (character.IsBeast && !Model.Character.IsBeastCharacter(character.CharacterID))
                chunk[3] |= 0x01;
            else
                chunk[3] &= 0xFE;
            if (character.CanUseDragonVein && !Model.Character.IsRoyal(character.CharacterID))
                chunk[4] |= 0x08;
            else
                chunk[4] &= 0xF7;
            bw.BaseStream.Seek(-8, SeekOrigin.Current);
            bw.BaseStream.Write(chunk, 0, 8);

            // Character main data
            chunk = new byte[] {
                character.Level,
                character.Experience,
                character.InternalLevel,
                character.EternalSealsUsed,
                (byte)((ushort)character.CharacterID & 0xFF),
                (byte)(((ushort)character.CharacterID >> 8) & 0xFF),
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
            bw.BaseStream.Seek(1, SeekOrigin.Current);
            bw.Write(character.Item_1.Raw());
            bw.BaseStream.Seek(1, SeekOrigin.Current);
            bw.Write(character.Item_2.Raw());
            bw.BaseStream.Seek(1, SeekOrigin.Current);
            bw.Write(character.Item_3.Raw());
            bw.BaseStream.Seek(1, SeekOrigin.Current);
            bw.Write(character.Item_4.Raw());
            bw.BaseStream.Seek(1, SeekOrigin.Current);
            bw.Write(character.Item_5.Raw());

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
            bw.Write(character.LearnedSkills.Bytes);

            // Stuff only on deployed characters
            if (character.IsDeployed)
            {
                bw.BaseStream.Seek(54, SeekOrigin.Current);
            }

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
            bw.BaseStream.Seek(1, SeekOrigin.Current);

            // Battles and Victories
            bw.Write(BitConverter.GetBytes(character.BattleCount));
            bw.Write(BitConverter.GetBytes(character.VictoryCount));

            // TODO
            bw.BaseStream.Seek(5, SeekOrigin.Current);

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

            // Stuff
            br.ReadBytes(0x6C);

            // Castle name (size is a guess)
            br.ReadBytes(0x20);

            // Stuff
            br.ReadBytes(0x19);

            // Dragon Vein point
            chunk = new byte[0x2];
            br.Read(chunk, 0, 0x2);
            DragonVeinPoint = (ushort)((chunk[1] << 8) | chunk[0]);

            // Stuff
            br.ReadBytes(0x182);

            // Materials
            chunk = new byte[0x16];
            br.Read(chunk, 0, 0x16);

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

            // Stuff (need to confirm size. it's variable) (maybe 0x25 on birthright save)
            br.ReadBytes(0x37);

            // Some block that I *think* is a list of chapters completed
            // Or something that scales with game length, anyway (it's way longer in the revelation save)
            var butWaitTheresMore = true;
            var stopHere = new byte[] { 0x00, 0x00, 0x00, 0x01 };
            while (butWaitTheresMore)
            {
                chunk = new byte[4];
                br.Read(chunk, 0, 4);

                if (Enumerable.SequenceEqual(chunk, stopHere)) butWaitTheresMore = false;
            }

            // Stuff
            br.ReadBytes(0x107);

            // Avatar name
            chunk = new byte[0x18];
            br.Read(chunk, 0, 0x18);

            // Stuff
            br.ReadBytes(0xA8);

            // Battle points
            chunk = new byte[4];
            br.Read(chunk, 0, 4);

            BattlePoints = (uint)(chunk[0] + chunk[1] * 0x100 + chunk[2] * 0x10000 + chunk[3] * 0x1000000);

            // Visit points
            chunk = new byte[4];
            br.Read(chunk, 0, 4);

            VisitPoints = (uint)(chunk[0] + chunk[1] * 0x100 + chunk[2] * 0x10000 + chunk[3] * 0x1000000);

            // 0x1D5 between that and the FF FF FF that begins the Chaos Block

            // Not sure about the rest. Guess it's a TODO.
        }

        public void WriteMyCastle(BinaryWriter bw)
        {


            bw.BaseStream.Seek(_myCastleOffset, SeekOrigin.Begin);

            // Stuff
            bw.BaseStream.Seek(0x6C, SeekOrigin.Current);

            // Castle name (size is a guess)
            bw.BaseStream.Seek(0x20, SeekOrigin.Current);

            // Stuff
            bw.BaseStream.Seek(0x19, SeekOrigin.Current);

            // Dragon Vein point
            chunk = new byte[] { (byte)(DragonVeinPoint & 0xFF), (byte)(DragonVeinPoint >> 8) };
            bw.BaseStream.Write(chunk, 0, 0x2);

            // Stuff
            bw.BaseStream.Seek(0x182, SeekOrigin.Current);

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

            // Stuff
            bw.BaseStream.Seek(0x2EE, SeekOrigin.Current);

            //// Battle points
            //chunk = new byte[]
            //{
            //    (byte)(BattlePoints & 0xFF),
            //    (byte)(BattlePoints >> 8 & 0xFF),
            //    (byte)(BattlePoints >> 16 & 0xFF),
            //    (byte)(BattlePoints >> 24 & 0xFF)
            //};
            //bw.Write(chunk);

            //// Visit points
            //chunk = new byte[]
            //{
            //    (byte)(VisitPoints & 0xFF),
            //    (byte)(VisitPoints >> 8 & 0xFF),
            //    (byte)(VisitPoints >> 16 & 0xFF),
            //    (byte)(VisitPoints >> 24 & 0xFF)
            //};
            //bw.Write(chunk);

            // Not sure about the rest. Guess it's a TODO.
        }

        #endregion
    }
}

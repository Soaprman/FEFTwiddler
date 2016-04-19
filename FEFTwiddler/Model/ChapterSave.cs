using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using FEFTwiddler.Enums;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model
{
    /// <summary>
    /// A Fire Emblem Fates "Chapter" save
    /// </summary>
    public class ChapterSave : ISave
    {
        #region Member variables

        private SaveFile _file;

        /// <summary>Hex: 0x4E415254 / Text: NART</summary>
        private byte[] _convoyMarker = new byte[] { 0x4E, 0x41, 0x52, 0x54 };
        private long _convoyOffset;
        /// <summary>Hex: 0x34334143 / Text: 43AC</summary>
        private byte[] _myCastleMarker = new byte[] { 0x34, 0x33, 0x41, 0x43 };
        private long _myCastleOffset;

        #endregion

        #region Properties

        public byte[] AvatarName { get; set; }

        public Shop HoshidoArmory { get; set; }
        public Shop NohrArmory { get; set; }
        public Shop HoshidoStore { get; set; }
        public Shop NohrStore { get; set; }

        public uint Gold { get; set; }

        public Enums.Difficulty Difficulty { get; set; }
        public Enums.Ruleset Ruleset { get; set; }

        public ushort DragonVeinPoint { get; set; }

        public byte[] UnlockedStatues1 { get; set; }
        public byte[] UnlockedStatues2 { get; set; }
        public byte[] UnlockedStatues3 { get; set; }

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

        public Enums.Chapter CurrentChapter { get; set; }
        public List<ChapterHistoryEntry> ChapterHistory { get; set; }

        // These are hacks that can hopefully one day be removed
        public int ChapterHistoryOriginalCount { get; set; }
        public const int ChapterHistoryEntryLength = 0x10;

        private List<Character> _characters = new List<Character>();
        public List<Character> Characters {
            get
            {
                return _characters;
            }
        }

        // Another massive hack
        private int ModifiedArraySizeRelativeToOriginalSize = 0;

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

        public void UnlockAllStatues()
        {
            UnlockedStatues1 = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            UnlockedStatues2 = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            UnlockedStatues3 = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
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
                ReadCompressionRegionData(br);
                ReadUserData(br);
                ReadSPOTData(br);
                ReadShopData(br);
                ReadCharacters(br);
                //ReadWeaponNameData(br);
                //ReadConvoyData(br);

                br.AdvanceToValue(_myCastleMarker);
                _myCastleOffset = br.BaseStream.Position;
                ReadMyCastle(br);
            }
        }

        public void Write()
        {
            using (var ms = new MemoryStream(_file.DecompressedBytes, 0, _file.DecompressedBytes.Length, true, true))
            using (var bw = new BinaryWriter(ms))
            {
                WriteHeaderData(bw);
                WriteCompressionRegionData(bw);
                WriteUserData(bw);
                WriteSPOTData(bw);
                WriteShopData(bw);
                WriteCharacters(bw);
                //WriteWeaponNameData(bw);
                //WriteConvoyData(bw);

                WriteMyCastle(bw);
            }

            // Adjust array length based on changes made during the write process
            if (ModifiedArraySizeRelativeToOriginalSize < 0)
            {
                var bytes = _file.DecompressedBytes;
                _file.DecompressedBytes = bytes.Take(bytes.Length + ModifiedArraySizeRelativeToOriginalSize).ToArray();
            }
            else if (ModifiedArraySizeRelativeToOriginalSize > 0)
            {
                // Not implemented yet
            }

            _file.Write();
        }

        #endregion

        #region Header IO

        public void ReadHeaderData(BinaryReader br)
        {
            byte[] chunk;

            // Stuff
            br.ReadBytes(0x0F);

            // Avatar name
            chunk = new byte[0x18];
            br.Read(chunk, 0, 0x18);

            AvatarName = chunk;

            // TODO
            br.ReadBytes(0x99);
        }

        public void WriteHeaderData(BinaryWriter bw)
        {
            bw.BaseStream.Seek(0xC0, SeekOrigin.Current);
        }

        #endregion

        #region Compression Region IO

        public void ReadCompressionRegionData(BinaryReader br)
        {
            // EDNI
            br.ReadBytes(0x4);

            // Stuff
            br.ReadBytes(0x80);
        }

        public void WriteCompressionRegionData(BinaryWriter bw)
        {
            // EDNI
            bw.BaseStream.Seek(0x4, SeekOrigin.Current);

            // Stuff
            bw.BaseStream.Seek(0x80, SeekOrigin.Current);
        }

        #endregion

        #region User Data IO

        public void ReadUserData(BinaryReader br)
        {
            byte[] chunk;

            // RESU
            br.ReadBytes(0x04);

            // Stuff
            br.ReadBytes(0x09);

            // Chapter
            CurrentChapter = (Enums.Chapter)br.ReadByte();

            // Stuff
            br.ReadBytes(0x04);

            // Chapter history
            ChapterHistory = new List<ChapterHistoryEntry>();
            var chapterCount = br.ReadByte();
            for (var i = 0; i < chapterCount; i++)
            {
                chunk = new byte[0x10];
                br.Read(chunk, 0, 0x10);

                ChapterHistory.Add(new ChapterHistoryEntry(chunk));
            }
            ChapterHistoryOriginalCount = ChapterHistory.Count;

            // Stuff (block of mostly 00s that starts with 00 80 and ends with 30 00 00 00 FF FF)
            br.ReadBytes(0xE1);

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

            // TODO
            br.ReadBytes(0x14);

            // FF FF FF FF
            br.ReadBytes(0x04);

            // Stuff (ends with 0x02)
            br.ReadBytes(0x4D);

            // TODO
            //br.ReadBytes(0x49A);
            // 0x216 in 9Hopper save
        }

        public void WriteUserData(BinaryWriter bw)
        {
            // RESU
            bw.BaseStream.Seek(0x04, SeekOrigin.Current);

            // Stuff
            bw.BaseStream.Seek(0x09, SeekOrigin.Current);

            // Chapter
            bw.Write((byte)CurrentChapter);

            // Stuff
            bw.BaseStream.Seek(0x04, SeekOrigin.Current);

            // Chapter history
            bw.Write((byte)ChapterHistory.Count);
            foreach (var chapterEntry in ChapterHistory)
            {
                bw.Write(chapterEntry.Raw);
            }
            // Hack: If entries have been removed, "collapse" the save to fit
            // TODO: Refactor this whole file to work in a different way, maybe?
            for (var i = ChapterHistory.Count; i < ChapterHistoryOriginalCount; i++)
            {
                var ms = (MemoryStream)(bw.BaseStream);
                var position = ms.Position;
                var buffer = ms.GetBuffer();
                Buffer.BlockCopy(buffer, (int)(position + ChapterHistoryEntryLength), buffer, (int)position, (int)(ms.Length - ChapterHistoryEntryLength - position));
                ms.SetLength(ms.Length - ChapterHistoryEntryLength);
                ModifiedArraySizeRelativeToOriginalSize -= ChapterHistoryEntryLength;

                // Also make sure future offset-based things start off in the right spot!
                _myCastleOffset -= ChapterHistoryEntryLength;
            }

            // Stuff (block of mostly 00s that starts with 00 80 and ends with 30 00 00 00 FF FF)
            bw.BaseStream.Seek(0xE1, SeekOrigin.Current);

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

            // TODO
            bw.BaseStream.Seek(0x14, SeekOrigin.Current);

            // FF FF FF FF
            bw.BaseStream.Seek(0x04, SeekOrigin.Current);

            // Stuff (ends with 0x02)
            bw.BaseStream.Seek(0x4D, SeekOrigin.Current);

            // TODO
            //bw.BaseStream.Seek(0x49A, SeekOrigin.Current);
            // 0x216 in 9Hopper save
        }

        #endregion

        #region SPOT Region IO

        // TODO: Rename these when it's known what SPOT is (maybe Spotpass?)

        public void ReadSPOTData(BinaryReader br)
        {
            // TOPS
            //br.ReadBytes(0x04);
            // Use this until User Data size is fully understood
            br.BaseStream.AdvanceToValue(new byte[] { 0x54, 0x4F, 0x50, 0x53 });

            // Stuff
            br.ReadBytes(0x1C);

            // Stuff
            var blockCount = br.ReadByte();
            for (var i = 0; i < blockCount; i++)
            {
                br.ReadBytes(0x21);
            }
        }

        public void WriteSPOTData(BinaryWriter bw)
        {
            // TOPS
            //bw.BaseStream.Seek(0x04, SeekOrigin.Current);
            // Use this until User Data size is fully understood
            bw.BaseStream.AdvanceToValue(new byte[] { 0x54, 0x4F, 0x50, 0x53 });

            // Stuff
            bw.BaseStream.Seek(0x1C, SeekOrigin.Current);

            var blockCount = bw.BaseStream.ReadByte();
            for (var i = 0; i < blockCount; i++)
            {
                bw.BaseStream.Seek(0x21, SeekOrigin.Current);
            }
        }

        #endregion

        #region Shop IO

        public void ReadShopData(BinaryReader br)
        {
            byte[] chunk; byte itemCount;

            // POHS
            br.ReadBytes(0x04);

            // 00
            br.ReadBytes(0x01);

            // Dawn Armory
            chunk = new byte[0x03];
            br.Read(chunk, 0, 0x03);

            HoshidoArmory = new Shop();
            HoshidoArmory.Level = chunk[1];
            itemCount = chunk[2];
            for (var i = 0; i < itemCount; i++)
            {
                chunk = new byte[0x04];
                br.Read(chunk, 0, 0x04);
                HoshidoArmory.Items.Add(new ShopItem(chunk));
            }

            // Dusk Armory
            chunk = new byte[0x03];
            br.Read(chunk, 0, 0x03);

            NohrArmory = new Shop();
            NohrArmory.Level = chunk[1];
            itemCount = chunk[2];
            for (var i = 0; i < itemCount; i++)
            {
                chunk = new byte[0x04];
                br.Read(chunk, 0, 0x04);
                NohrArmory.Items.Add(new ShopItem(chunk));
            }

            // Rod Shop
            chunk = new byte[0x03];
            br.Read(chunk, 0, 0x03);

            HoshidoStore = new Shop();
            HoshidoStore.Level = chunk[1];
            itemCount = chunk[2];
            for (var i = 0; i < itemCount; i++)
            {
                chunk = new byte[0x04];
                br.Read(chunk, 0, 0x04);
                HoshidoStore.Items.Add(new ShopItem(chunk));
            }

            // Staff Store
            chunk = new byte[0x03];
            br.Read(chunk, 0, 0x03);

            NohrStore = new Shop();
            NohrStore.Level = chunk[1];
            itemCount = chunk[2];
            for (var i = 0; i < itemCount; i++)
            {
                chunk = new byte[0x04];
                br.Read(chunk, 0, 0x04);
                NohrStore.Items.Add(new ShopItem(chunk));
            }
        }

        public void WriteShopData(BinaryWriter bw)
        {
            // POHS
            bw.BaseStream.Seek(0x04, SeekOrigin.Current);

            // 00
            bw.BaseStream.Seek(0x01, SeekOrigin.Current);

            // Dawn Armory
            bw.Write(new byte[] {
                0x00,
                HoshidoArmory.Level,
                (byte)HoshidoArmory.Items.Count
            });
            foreach (var item in HoshidoArmory.Items)
            {
                bw.Write(item.Raw);
            }

            // Dusk Armory
            bw.Write(new byte[] {
                0x00,
                NohrArmory.Level,
                (byte)NohrArmory.Items.Count
            });
            foreach (var item in NohrArmory.Items)
            {
                bw.Write(item.Raw);
            }

            // Rod Shop
            bw.Write(new byte[] {
                0x00,
                HoshidoStore.Level,
                (byte)HoshidoStore.Items.Count
            });
            foreach (var item in HoshidoStore.Items)
            {
                bw.Write(item.Raw);
            }

            // Staff Store
            bw.Write(new byte[] {
                0x00,
                NohrStore.Level,
                (byte)NohrStore.Items.Count
            });
            foreach (var item in NohrStore.Items)
            {
                bw.Write(item.Raw);
            }
        }

        #endregion

        #region Character IO

        private void ReadCharacters(BinaryReader br)
        {
            // TINU
            br.ReadBytes(4);

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
                    case 0x05: // If after chapter 6, units you had at some point before the split who haven't rejoined yet (or at least I think that's what this is)
                    case 0x06: // Dead units
                        var characterCount = br.ReadByte();
                        for (var i = 0; i < characterCount; i++)
                        {
                            ReadCurrentCharacter(br, nextBlock == 0x05);
                        }
                        break;
                    case 0xFF: // End of unit block
                    default: // Just a failsafe
                        stillReading = false;
                        break;
                }
            }
        }

        private void ReadCurrentCharacter(BinaryReader br, bool isAbsent)
        {
            byte[] chunk;

            var character = new Character();

            character.IsAbsent = isAbsent;

            chunk = new byte[Model.Character.RawBlock1Length];
            br.Read(chunk, 0, Model.Character.RawBlock1Length);
            character.RawBlock1 = chunk;

            chunk = new byte[Model.Character.RawInventoryLength];
            br.Read(chunk, 0, Model.Character.RawInventoryLength);
            character.RawInventory = chunk;

            chunk = new byte[0x01];
            br.Read(chunk, 0, 0x01);
            character.RawNumberOfSupports = chunk.First();

            chunk = new byte[character.RawNumberOfSupports];
            br.Read(chunk, 0, character.RawNumberOfSupports);
            character.RawSupports = chunk;

            chunk = new byte[Model.Character.RawBlock2Length];
            br.Read(chunk, 0, Model.Character.RawBlock2Length);
            character.RawBlock2 = chunk;

            chunk = new byte[Model.Character.RawLearnedSkillsLength];
            br.Read(chunk, 0, Model.Character.RawLearnedSkillsLength);
            character.RawLearnedSkills = chunk;

            var depLength = (character.IsDeployed ? Model.Character.RawDeployedUnitInfoLengthIfDeployed : Model.Character.RawDeployedUnitInfoLengthIfNotDeployed);
            chunk = new byte[depLength];
            br.Read(chunk, 0, depLength);
            character.RawDeployedUnitInfo = chunk;

            chunk = new byte[Model.Character.RawBlock3Length];
            br.Read(chunk, 0, Model.Character.RawBlock3Length);
            character.RawBlock3 = chunk;

            chunk = new byte[0x01];
            br.Read(chunk, 0, 0x01);
            character.RawEndBlockType = chunk.First();

            chunk = new byte[character.GetRawEndBlockSize()];
            br.Read(chunk, 0, character.GetRawEndBlockSize());
            character.RawEndBlock = chunk;

            _characters.Add(character);
        }

        private void WriteCharacters(BinaryWriter bw)
        {
            // TINU
            bw.BaseStream.Seek(4, SeekOrigin.Current);

            // 01
            bw.BaseStream.Seek(1, SeekOrigin.Current);

            var deployedCharacters = _characters.Where((x) => !x.IsAbsent && x.IsDeployed).ToList();
            if (deployedCharacters.Count > 0)
            {
                bw.Write((byte)0x00);
                bw.Write((byte)deployedCharacters.Count);
                foreach (var character in deployedCharacters)
                {
                    WriteCurrentCharacter(bw, character);
                }
            }

            var livingCharacters = _characters.Where((x) => !x.IsAbsent && !x.IsDeployed && !x.IsDead).ToList();
            if (livingCharacters.Count > 0)
            {
                bw.Write((byte)0x03);
                bw.Write((byte)livingCharacters.Count);
                foreach (var character in livingCharacters)
                {
                    WriteCurrentCharacter(bw, character);
                }
            }

            var absentCharacters = _characters.Where((x) => x.IsAbsent).ToList();
            if (absentCharacters.Count > 0)
            {
                bw.Write((byte)0x05);
                bw.Write((byte)absentCharacters.Count);
                foreach (var character in absentCharacters)
                {
                    WriteCurrentCharacter(bw, character);
                }
            }

            var deadCharacters = _characters.Where((x) => x.IsDead).ToList();
            if (deadCharacters.Count > 0)
            {
                bw.Write((byte)0x06);
                bw.Write((byte)deadCharacters.Count);
                foreach (var character in deadCharacters)
                {
                    WriteCurrentCharacter(bw, character);
                }
            }

            // End character block
            bw.Write((byte)0xFF);
        }

        private void WriteCurrentCharacter(BinaryWriter bw, Model.Character character)
        {
            bw.Write(character.Raw);
        }

        #endregion

        #region Weapon Name IO

        public void ReadWeaponNameData(BinaryReader br)
        {
            // IFER
            br.ReadBytes(0x04);
        }

        public void WriteWeaponNameData(BinaryWriter bw)
        {
            // IFER
            bw.BaseStream.Seek(0x04, SeekOrigin.Current);
        }

        #endregion

        #region Convoy IO

        public void ReadConvoyData(BinaryReader br)
        {
            // NART
            br.ReadBytes(0x04);
        }

        public void WriteConvoyData(BinaryWriter bw)
        {
            // NART
            bw.BaseStream.Seek(0x04, SeekOrigin.Current);
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
            br.ReadBytes(0x19);

            // Dragon Vein point
            chunk = new byte[0x2];
            br.Read(chunk, 0, 0x2);
            DragonVeinPoint = (ushort)((chunk[1] << 8) | chunk[0]);

            // Stuff
            br.ReadBytes(0x2);

            // Unlocked buildings block 1? (begins with XX XX XX F1 FF)
            br.ReadBytes(0x14);
            chunk = new byte[0x1B];
            br.Read(chunk, 0, 0x1B);
            UnlockedStatues1 = chunk;
            br.ReadBytes(0x11);

            // Stuff (a bunch of 0x00s)
            br.ReadBytes(0x40);

            // Unlocked buildings block 2? (begins with XX XX XX F1 FF)
            br.ReadBytes(0x14);
            chunk = new byte[0x1B];
            br.Read(chunk, 0, 0x1B);
            UnlockedStatues2 = chunk;
            br.ReadBytes(0x11);

            // Stuff (a bunch of 0x00s)
            br.ReadBytes(0x40);

            // Unlocked buildings block 3? (begins with XX XX XX F1 FF)
            br.ReadBytes(0x14);
            chunk = new byte[0x1B];
            br.Read(chunk, 0, 0x1B);
            UnlockedStatues3 = chunk;
            br.ReadBytes(0x11);

            // Stuff (a bunch of 0x00s)
            br.ReadBytes(0x40);

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
            byte[] chunk;

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
            bw.BaseStream.Seek(0x2, SeekOrigin.Current);

            // Unlocked buildings block 1? (begins with XX XX XX F1 FF)
            bw.BaseStream.Seek(0x14, SeekOrigin.Current);
            bw.BaseStream.Write(UnlockedStatues1, 0, 0x1B);
            bw.BaseStream.Seek(0x11, SeekOrigin.Current);

            // Stuff (a bunch of 0x00s)
            bw.BaseStream.Seek(0x40, SeekOrigin.Current);

            // Unlocked buildings block 2? (begins with XX XX XX F1 FF)
            bw.BaseStream.Seek(0x14, SeekOrigin.Current);
            bw.BaseStream.Write(UnlockedStatues2, 0, 0x1B);
            bw.BaseStream.Seek(0x11, SeekOrigin.Current);

            // Stuff (a bunch of 0x00s)
            bw.BaseStream.Seek(0x40, SeekOrigin.Current);

            // Unlocked buildings block 3? (begins with XX XX XX F1 FF)
            bw.BaseStream.Seek(0x14, SeekOrigin.Current);
            bw.BaseStream.Write(UnlockedStatues3, 0, 0x1B);
            bw.BaseStream.Seek(0x11, SeekOrigin.Current);

            // Stuff (a bunch of 0x00s)
            bw.BaseStream.Seek(0x40, SeekOrigin.Current);

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

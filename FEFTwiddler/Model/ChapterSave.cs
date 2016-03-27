using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FEFTwiddler.Model
{
    class ChapterSave: ISave
    {
        private List<Character> _characters = new List<Character>();
        public List<Character> Characters {
            get
            {
                return _characters;
            }
        }

        private string _filePath;

        public static ChapterSave FromPath(string path)
        {
            var cs = new ChapterSave();
            cs.Read(path);
            return cs;
        }

        public void Read(string path)
        {
            _filePath = path;

            using (var fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                var characterMarker = new byte[] { 0x54, 0x49, 0x4E, 0x55 }; // TINU

                AdvanceToValue(br, characterMarker);
                ReadCharacters(br);
            }
        }

        /// <summary>
        /// Read until hitting the value.
        /// </summary>
        /// <remarks>What could possibly go wrong?</remarks>
        private void AdvanceToValue(BinaryReader br, byte[] value)
        {
            if (value.Length == 0) return;

            try
            {
                byte[] match = new byte[value.Length];
                int position = 0;

                while (true)
                {
                    var curByte = br.ReadByte();

                    if (curByte == value[position])
                    {
                        match[position] = curByte;
                        if (Enumerable.SequenceEqual(match, value)) return;
                        position++;
                    }
                    else
                    {
                        match = new byte[value.Length];
                        position = 0;
                    }

                    if (position >= value.Length) position = 0;
                }
            }
            catch (EndOfStreamException)
            {
                throw new EndOfStreamException("Value not found");
            }
        }

        public void Write()
        {
            using (var fs = new FileStream(_filePath, FileMode.Open, FileAccess.ReadWrite))
            using (var bw = new BinaryWriter(fs))
            {
                WriteCharacters(bw);
            }
        }

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

            character.IsDead = chunk[0] == 0x18;
            character.IsRecruited = chunk[3] == 0x30;

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
            br.ReadBytes(character.GetSupportBlockSize());

            // TODO
            br.ReadBytes(17);

            // TODO
            br.ReadBytes(48);

            // Learned skills
            chunk = new byte[20];
            br.Read(chunk, 0, 20);

            character.LearnedSkills = chunk;

            // Supports 2
            br.ReadBytes(character.GetSupportBlock2Size());

            // TODO
            br.ReadBytes(20);

            // TODO
            br.ReadBytes(character.GetEndBlockSize());

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

            // Weapon exp and HP
            bw.BaseStream.Seek(9, SeekOrigin.Current);

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
            bw.BaseStream.Seek(character.GetSupportBlockSize(), SeekOrigin.Current);

            // TODO
            bw.BaseStream.Seek(17, SeekOrigin.Current);

            // TODO
            bw.BaseStream.Seek(48, SeekOrigin.Current);

            // Learned skills
            bw.Write(character.LearnedSkills);

            // Supports 2
            bw.BaseStream.Seek(character.GetSupportBlock2Size(), SeekOrigin.Current);

            // TODO
            bw.BaseStream.Seek(20, SeekOrigin.Current);

            // TODO
            bw.BaseStream.Seek(character.GetEndBlockSize(), SeekOrigin.Current);
        }
    }
}

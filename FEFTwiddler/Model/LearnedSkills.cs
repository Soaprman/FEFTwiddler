using System;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model
{
    public class LearnedSkills
    {
        private byte[] _bytes;
        public byte[] Bytes
        {
            get
            {
                return _bytes;
            }
        }

        public LearnedSkills(byte[] bytes)
        {
            if (bytes.Length != 20) throw new ArgumentException("The LearnedSkills array must be 20 bytes long");
            _bytes = bytes;
        }

        public void Add(Enums.Skill skillId)
        {
            var skillInfo = Data.Database.Skills.GetByID(skillId);
            _bytes[skillInfo.LearnedSkillByteOffset] = (byte)(_bytes[skillInfo.LearnedSkillByteOffset] | skillInfo.LearnedSkillBitMask);
        }

        public void Add(byte[] bytes)
        {
            _bytes = _bytes.Or(bytes);
        }

        public void Remove(Enums.Skill skillId)
        {
            var skillInfo = Data.Database.Skills.GetByID(skillId);
            _bytes[skillInfo.LearnedSkillByteOffset] = (byte)(_bytes[skillInfo.LearnedSkillByteOffset] & ~skillInfo.LearnedSkillBitMask);
        }

        public void Remove(byte[] bytes)
        {
            _bytes = _bytes.AndNot(bytes);
        }

        public bool Contains(Enums.Skill skillId)
        {
            var skillInfo = Data.Database.Skills.GetByID(skillId);
            return (byte)(_bytes[skillInfo.LearnedSkillByteOffset] & skillInfo.LearnedSkillBitMask) == skillInfo.LearnedSkillBitMask;
        }
    }
}

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
            UpdateNoneSkill();
        }

        public void Add(Enums.Skill skillId)
        {
            var skillInfo = Data.Database.Skills.GetByID(skillId);
            _bytes[skillInfo.LearnedSkillByteOffset] = (byte)(_bytes[skillInfo.LearnedSkillByteOffset] | skillInfo.LearnedSkillBitMask);
            UpdateNoneSkill();
        }

        public void Add(byte[] bytes)
        {
            _bytes = _bytes.Or(bytes);
            UpdateNoneSkill();
        }

        public void Remove(Enums.Skill skillId)
        {
            var skillInfo = Data.Database.Skills.GetByID(skillId);
            _bytes[skillInfo.LearnedSkillByteOffset] = (byte)(_bytes[skillInfo.LearnedSkillByteOffset] & ~skillInfo.LearnedSkillBitMask);
            UpdateNoneSkill();
        }

        public void Remove(byte[] bytes)
        {
            _bytes = _bytes.AndNot(bytes);
            UpdateNoneSkill();
        }

        public bool Contains(Enums.Skill skillId)
        {
            var skillInfo = Data.Database.Skills.GetByID(skillId);
            return (byte)(_bytes[skillInfo.LearnedSkillByteOffset] & skillInfo.LearnedSkillBitMask) == skillInfo.LearnedSkillBitMask;
        }

        /// <summary>
        /// The "None" skill (BitMask 1) is only known if no other skills it shares a byte with are known.
        /// </summary>
        private void UpdateNoneSkill()
        {
            return;
            // I'm not entirely sure this actually needs done.
            // In one of the test saves, _bytes[0] is 65 to begin with, and this causes the unit test to fail for that save.
            // I guess more research is needed or something.
            //if (_bytes[0] > 1 && ((_bytes[0] & 1) == 1)) _bytes[0] -= 1;
            //else _bytes[0] = 1;
        }
    }
}

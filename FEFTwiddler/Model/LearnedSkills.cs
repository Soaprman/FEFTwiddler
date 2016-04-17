using System;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Model
{
    public class LearnedSkills
    {
        private byte[] _raw;
        public byte[] Raw
        {
            get
            {
                return _raw;
            }
        }

        public LearnedSkills(byte[] raw)
        {
            if (raw.Length != 20) throw new ArgumentException("The LearnedSkills array must be 20 bytes long");
            _raw = raw;
            UpdateNoneSkill();
        }

        public void Add(Enums.Skill skillId)
        {
            var skillInfo = Data.Database.Skills.GetByID(skillId);
            _raw[skillInfo.LearnedSkillByteOffset] = (byte)(_raw[skillInfo.LearnedSkillByteOffset] | skillInfo.LearnedSkillBitMask);
            UpdateNoneSkill();
        }

        public void Add(byte[] bytes)
        {
            _raw = _raw.Or(bytes);
            UpdateNoneSkill();
        }

        public void Remove(Enums.Skill skillId)
        {
            var skillInfo = Data.Database.Skills.GetByID(skillId);
            _raw[skillInfo.LearnedSkillByteOffset] = (byte)(_raw[skillInfo.LearnedSkillByteOffset] & ~skillInfo.LearnedSkillBitMask);
            UpdateNoneSkill();
        }

        public void Remove(byte[] bytes)
        {
            _raw = _raw.AndNot(bytes);
            UpdateNoneSkill();
        }

        public bool Contains(Enums.Skill skillId)
        {
            var skillInfo = Data.Database.Skills.GetByID(skillId);
            return (byte)(_raw[skillInfo.LearnedSkillByteOffset] & skillInfo.LearnedSkillBitMask) == skillInfo.LearnedSkillBitMask;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Data
{
    public class SkillDatabase : BaseDatabase
    {
        public SkillDatabase(Enums.Language language) : base(language)
        {
            _data = XElement.Parse(Properties.Resources.Data_Skills);
        }

        public Skill GetByID(Enums.Skill skillId)
        {
            var row = _data
                .Elements("skill")
                .Where((x) => x.Attribute("id").Value == ((byte)skillId).ToString())
                .First();

            return FromElement(row);
        }

        /// <summary>
        /// Get all skills (even unlearnable ones)
        /// </summary>
        public IEnumerable<Skill> GetAll()
        {
            var elements = _data.Elements("skill");
            var rows = new List<Skill>();
            foreach (var e in elements)
            {
                rows.Add(FromElement(e));
            }
            return rows;
        }

        /// <summary>
        /// Get all learnable (i.e. not personal or defined as unlearnable) skills
        /// </summary>
        public IEnumerable<Skill> GetAllLearnable()
        {
            var elements = _data.Elements("skill");
            var rows = new List<Skill>();
            foreach (var e in elements)
            {
                var skill = FromElement(e);
                if (!skill.IsPersonal && !skill.IsUnlearnable) rows.Add(skill);
            }
            return rows;
        }

        private Skill FromElement(XElement row)
        {
            var displayName = GetDisplayName(row);
            var learnedSkillInfo = row.Elements("learnedSkillInfo").First();
            var categories = row.Elements("categories").First();

            return new Skill
            {
                SkillID = (Enums.Skill)row.GetAttribute<byte>("id"),
                DisplayName = displayName,
                IsPersonal = row.GetAttribute("personal", false),
                IsUnlearnable = row.GetAttribute("unlearnable", false),
                LearnedSkillByteOffset = learnedSkillInfo.GetAttribute<byte>("byteOffset"),
                LearnedSkillBitMask = learnedSkillInfo.GetAttribute<byte>("bitMask"),
                IsNormalClassSkill = categories.GetAttribute<bool>("normalClass"),
                IsCorrinOnlySkill = categories.GetAttribute<bool>("corrinOnly"),
                IsAzuraOnlySkill = categories.GetAttribute<bool>("azuraOnly"),
                IsKitsuneOnlySkill = categories.GetAttribute<bool>("kitsuneOnly"),
                IsWolfskinOnlySkill = categories.GetAttribute<bool>("wolfskinOnly"),
                IsVillagerOnlySkill = categories.GetAttribute<bool>("villagerOnly"),
                IsPathBonusClassSkill = categories.GetAttribute<bool>("pathBonusClass"),
                IsDlcClassSkill = categories.GetAttribute<bool>("dlcClass"),
                IsAmiiboClassSkill = categories.GetAttribute<bool>("amiiboClass"),
                IsEnemyAndNpcSkill = categories.GetAttribute<bool>("enemyAndNpc")
            };
        }
    }
}

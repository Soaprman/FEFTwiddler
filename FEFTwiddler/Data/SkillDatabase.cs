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

        private Skill FromElement(XElement row)
        {
            var displayName = GetDisplayName(row);
            var learnedSkillInfo = row.Elements("learnedSkillInfo").First();

            return new Skill
            {
                SkillID = (Enums.Skill)row.GetAttribute<byte>("id"),
                DisplayName = displayName,
                IsEnemyOnly = row.GetAttribute("enemyOnly", false),
                IsDLC = row.GetAttribute("dlc", false),
                IsPersonal = row.GetAttribute("personal", false),
                IsUnlearnable = row.GetAttribute("unlearnable", false),
                LearnedSkillByteOffset = learnedSkillInfo.GetAttribute<byte>("byteOffset"),
                LearnedSkillBitMask = learnedSkillInfo.GetAttribute<byte>("bitMask")
            };
        }
    }
}

using System;
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

            var displayName = GetDisplayName(row);

            return new Skill
            {
                SkillID = (Enums.Skill)row.GetAttribute<byte>("id"),
                DisplayName = displayName
            };
        }
    }
}

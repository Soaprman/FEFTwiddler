using System;
using System.Linq;
using System.Xml.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Data
{
    public class CharacterDatabase : BaseDatabase
    {
        public CharacterDatabase(Enums.Language language) : base(language)
        {
            _data = XElement.Parse(Properties.Resources.Data_Characters);
        }

        public Character GetByID(Enums.Character characterId)
        {
            var row = _data
                .Elements("character")
                .Where((x) => x.Attribute("id").Value == ((ushort)characterId).ToString())
                .First();

            var displayName = GetDisplayName(row);
            var baseStats = row.Elements("baseStats").First();

            return new Character
            {
                CharacterID = (Enums.Character)row.GetAttribute<ushort>("id"),
                DisplayName = displayName,
                Base_HP = baseStats.GetAttribute<byte>("hp"),
                Base_Str = baseStats.GetAttribute<byte>("str"),
                Base_Mag = baseStats.GetAttribute<byte>("mag"),
                Base_Skl = baseStats.GetAttribute<byte>("skl"),
                Base_Spd = baseStats.GetAttribute<byte>("spd"),
                Base_Lck = baseStats.GetAttribute<byte>("lck"),
                Base_Def = baseStats.GetAttribute<byte>("def"),
                Base_Res = baseStats.GetAttribute<byte>("res")
            };
        }
    }
}

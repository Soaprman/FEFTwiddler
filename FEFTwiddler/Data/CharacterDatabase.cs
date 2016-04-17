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
            var modifier = row.Elements("modifier").First();
            var flags = row.Elements("flags").First();

            return new Character
            {
                CharacterID = (Enums.Character)row.GetAttribute<ushort>("id"),
                DisplayName = displayName,
                Base_HP = baseStats.GetAttribute<sbyte>("hp"),
                Base_Str = baseStats.GetAttribute<sbyte>("str"),
                Base_Mag = baseStats.GetAttribute<sbyte>("mag"),
                Base_Skl = baseStats.GetAttribute<sbyte>("skl"),
                Base_Spd = baseStats.GetAttribute<sbyte>("spd"),
                Base_Lck = baseStats.GetAttribute<sbyte>("lck"),
                Base_Def = baseStats.GetAttribute<sbyte>("def"),
                Base_Res = baseStats.GetAttribute<sbyte>("res"),
                Modifier_HP = modifier.GetAttribute<sbyte>("hp"),
                Modifier_Str = modifier.GetAttribute<sbyte>("str"),
                Modifier_Mag = modifier.GetAttribute<sbyte>("mag"),
                Modifier_Skl = modifier.GetAttribute<sbyte>("skl"),
                Modifier_Spd = modifier.GetAttribute<sbyte>("spd"),
                Modifier_Lck = modifier.GetAttribute<sbyte>("lck"),
                Modifier_Def = modifier.GetAttribute<sbyte>("def"),
                Modifier_Res = modifier.GetAttribute<sbyte>("res"),
                CanUseStones = flags.GetAttribute<bool>("canUseStones")
            };
        }
    }
}

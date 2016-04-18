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
                BaseStats = new Model.Stat
                {
                    HP = baseStats.GetAttribute<sbyte>("hp"),
                    Str = baseStats.GetAttribute<sbyte>("str"),
                    Mag = baseStats.GetAttribute<sbyte>("mag"),
                    Skl = baseStats.GetAttribute<sbyte>("skl"),
                    Spd = baseStats.GetAttribute<sbyte>("spd"),
                    Lck = baseStats.GetAttribute<sbyte>("lck"),
                    Def = baseStats.GetAttribute<sbyte>("def"),
                    Res = baseStats.GetAttribute<sbyte>("res")
                },
                Modifiers = new Model.Stat
                {
                    HP = modifier.GetAttribute<sbyte>("hp"),
                    Str = modifier.GetAttribute<sbyte>("str"),
                    Mag = modifier.GetAttribute<sbyte>("mag"),
                    Skl = modifier.GetAttribute<sbyte>("skl"),
                    Spd = modifier.GetAttribute<sbyte>("spd"),
                    Lck = modifier.GetAttribute<sbyte>("lck"),
                    Def = modifier.GetAttribute<sbyte>("def"),
                    Res = modifier.GetAttribute<sbyte>("res")
                },
                CanUseStones = flags.GetAttribute<bool>("canUseStones"),
                IsCorrin = flags.GetAttribute<bool>("isCorrin"),
                IsChild = flags.GetAttribute<bool>("isChild")
            };
        }
    }
}

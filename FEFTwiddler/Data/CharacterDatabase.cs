using System;
using System.Collections.Generic;
using System.Drawing;
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

            return FromElement(row);
        }

        /// <summary>
        /// Get all characters
        /// </summary>
        public IEnumerable<Character> GetAll()
        {
            var elements = _data.Elements("character");
            var rows = new List<Character>();
            foreach (var e in elements)
            {
                rows.Add(FromElement(e));
            }
            return rows;
        }

        /// <summary>
        /// Get all characters who are named and playable
        /// </summary>
        public IEnumerable<Character> GetAllNamedPlayable()
        {
            var elements = _data.Elements("character");
            var rows = new List<Character>();
            foreach (var e in elements)
            {
                var character = FromElement(e);
                // CorrinM through Zhara
                if ((ushort)character.CharacterID > 0 && (ushort)character.CharacterID <= 84) rows.Add(character);
            }
            return rows;
        }

        private Character FromElement(XElement row)
        {
            var displayName = GetDisplayName(row);
            var baseStats = row.Elements("baseStats").First();
            var modifier = row.Elements("modifier").First();
            var growthRates = row.Elements("growthRates").First();
            var flags = row.Elements("flags").First();
            var mainSupports = row.Elements("mainSupports").First();
            var familySupports = row.Elements("familySupports").First();
            var hairColor = row.Elements("hairColor").First();

            var character = new Character
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
                GrowthRates = new Model.Stat()
                {
                    HP = growthRates.GetAttribute<sbyte>("hp"),
                    Str = growthRates.GetAttribute<sbyte>("str"),
                    Mag = growthRates.GetAttribute<sbyte>("mag"),
                    Skl = growthRates.GetAttribute<sbyte>("skl"),
                    Spd = growthRates.GetAttribute<sbyte>("spd"),
                    Lck = growthRates.GetAttribute<sbyte>("lck"),
                    Def = growthRates.GetAttribute<sbyte>("def"),
                    Res = growthRates.GetAttribute<sbyte>("res")
                },
                CanUseStones = flags.GetAttribute<bool>("canUseStones"),
                IsCorrin = flags.GetAttribute<bool>("isCorrin"),
                IsChild = flags.GetAttribute<bool>("isChild"),
                MainSupportCount = mainSupports.GetAttribute<byte>("count"),
                FamilySupportCount = familySupports.GetAttribute<byte>("count"),
                HairColor = Color.FromArgb(hairColor.GetAttribute<byte>("a"), hairColor.GetAttribute<byte>("r"), hairColor.GetAttribute<byte>("g"), hairColor.GetAttribute<byte>("b")),
                IsPrisoner = flags.GetAttribute<bool>("isPrisoner"),
                IsFemale = flags.GetAttribute<bool>("isFemale"),
                IsManakete = flags.GetAttribute<bool>("isManakete"),
                IsBeast = flags.GetAttribute<bool>("isBeast"),
                CanUseDragonVein = flags.GetAttribute<bool>("canUseDragonVein")
            };

            character.EndBlockType = GetEndBlockType(character);

            return character;
        }

        public static byte GetEndBlockType(Character character)
        {
            if (character.IsCorrin) return 0x04;
            else if (character.IsChild) return 0x01;
            else return 0x00;
        }
    }
}

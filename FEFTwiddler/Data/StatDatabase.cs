using System;
using System.Linq;
using System.Xml.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Data
{
    public class StatDatabase : BaseDatabase
    {
        public StatDatabase(Enums.Language language) : base(language)
        {
            _data = XElement.Parse(Properties.Resources.Data_Stats);
        }

        public Stat GetByID(Enums.Stat statId)
        {
            var row = _data
                .Elements("stat")
                .Where((x) => x.Attribute("id").Value == ((ushort)statId).ToString())
                .First();

            var displayName = GetDisplayName(row);
            var baseBoonModifiers = row.Elements("baseBoonModifiers").First();
            var maxBoonModifiers = row.Elements("maxBoonModifiers").First();
            var baseBaneModifiers = row.Elements("baseBaneModifiers").First();
            var maxBaneModifiers = row.Elements("maxBaneModifiers").First();

            return new Stat
            {
                StatID = (Enums.Stat)row.GetAttribute<byte>("id"),
                DisplayName = displayName,
                BaseBoonModifiers = new Model.Stat()
                {
                    HP = baseBoonModifiers.GetAttribute<sbyte>("hp"),
                    Str = baseBoonModifiers.GetAttribute<sbyte>("str"),
                    Mag = baseBoonModifiers.GetAttribute<sbyte>("mag"),
                    Skl = baseBoonModifiers.GetAttribute<sbyte>("skl"),
                    Spd = baseBoonModifiers.GetAttribute<sbyte>("spd"),
                    Lck = baseBoonModifiers.GetAttribute<sbyte>("lck"),
                    Def = baseBoonModifiers.GetAttribute<sbyte>("def"),
                    Res = baseBoonModifiers.GetAttribute<sbyte>("res")
                },
                MaxBoonModifiers = new Model.Stat()
                {
                    HP = maxBoonModifiers.GetAttribute<sbyte>("hp"),
                    Str = maxBoonModifiers.GetAttribute<sbyte>("str"),
                    Mag = maxBoonModifiers.GetAttribute<sbyte>("mag"),
                    Skl = maxBoonModifiers.GetAttribute<sbyte>("skl"),
                    Spd = maxBoonModifiers.GetAttribute<sbyte>("spd"),
                    Lck = maxBoonModifiers.GetAttribute<sbyte>("lck"),
                    Def = maxBoonModifiers.GetAttribute<sbyte>("def"),
                    Res = maxBoonModifiers.GetAttribute<sbyte>("res")
                },
                BaseBaneModifiers = new Model.Stat()
                {
                    HP = baseBaneModifiers.GetAttribute<sbyte>("hp"),
                    Str = baseBaneModifiers.GetAttribute<sbyte>("str"),
                    Mag = baseBaneModifiers.GetAttribute<sbyte>("mag"),
                    Skl = baseBaneModifiers.GetAttribute<sbyte>("skl"),
                    Spd = baseBaneModifiers.GetAttribute<sbyte>("spd"),
                    Lck = baseBaneModifiers.GetAttribute<sbyte>("lck"),
                    Def = baseBaneModifiers.GetAttribute<sbyte>("def"),
                    Res = baseBaneModifiers.GetAttribute<sbyte>("res")
                },
                MaxBaneModifiers = new Model.Stat()
                {
                    HP = maxBaneModifiers.GetAttribute<sbyte>("hp"),
                    Str = maxBaneModifiers.GetAttribute<sbyte>("str"),
                    Mag = maxBaneModifiers.GetAttribute<sbyte>("mag"),
                    Skl = maxBaneModifiers.GetAttribute<sbyte>("skl"),
                    Spd = maxBaneModifiers.GetAttribute<sbyte>("spd"),
                    Lck = maxBaneModifiers.GetAttribute<sbyte>("lck"),
                    Def = maxBaneModifiers.GetAttribute<sbyte>("def"),
                    Res = maxBaneModifiers.GetAttribute<sbyte>("res")
                }
            };
        }
    }
}

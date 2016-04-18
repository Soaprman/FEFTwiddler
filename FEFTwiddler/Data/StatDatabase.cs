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
            var boonStats = row.Elements("boonStats").First();
            var baneStats = row.Elements("baneStats").First();

            return new Stat
            {
                StatID = (Enums.Stat)row.GetAttribute<byte>("id"),
                DisplayName = displayName,
                BoonStats = new Model.Stat()
                {
                    HP = boonStats.GetAttribute<sbyte>("hp"),
                    Str = boonStats.GetAttribute<sbyte>("str"),
                    Mag = boonStats.GetAttribute<sbyte>("mag"),
                    Skl = boonStats.GetAttribute<sbyte>("skl"),
                    Spd = boonStats.GetAttribute<sbyte>("spd"),
                    Lck = boonStats.GetAttribute<sbyte>("lck"),
                    Def = boonStats.GetAttribute<sbyte>("def"),
                    Res = boonStats.GetAttribute<sbyte>("res")
                },
                BaneStats = new Model.Stat()
                {
                    HP = baneStats.GetAttribute<sbyte>("hp"),
                    Str = baneStats.GetAttribute<sbyte>("str"),
                    Mag = baneStats.GetAttribute<sbyte>("mag"),
                    Skl = baneStats.GetAttribute<sbyte>("skl"),
                    Spd = baneStats.GetAttribute<sbyte>("spd"),
                    Lck = baneStats.GetAttribute<sbyte>("lck"),
                    Def = baneStats.GetAttribute<sbyte>("def"),
                    Res = baneStats.GetAttribute<sbyte>("res")
                }
            };
        }
    }
}

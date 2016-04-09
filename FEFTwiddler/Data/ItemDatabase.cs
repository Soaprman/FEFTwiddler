using System;
using System.Linq;
using System.Xml.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Data
{
    public class ItemDatabase : BaseDatabase
    {
        public ItemDatabase(Enums.Language language) : base(language)
        {
            _data = XElement.Parse(Properties.Resources.Data_Items);
        }

        public Item GetByID(Enums.Item itemId)
        {
            var row = _data
                .Elements("item")
                .Where((x) => x.Attribute("id").Value == ((ushort)itemId).ToString())
                .First();

            var displayName = GetDisplayName(row);

            return new Item
            {
                ItemID = (Enums.Item)row.GetAttribute<ushort>("id"),
                DisplayName = displayName,
                Type = (Enums.ItemType)Enum.Parse(typeof(Enums.ItemType), row.GetAttribute("type")),
                MaximumUses = row.GetAttribute<byte>("maxUses"),
                IsEnemyOnly = row.GetAttribute("enemyOnly", false),
                IsMapOnly = row.GetAttribute("mapOnly", false)
            };
        }
    }
}

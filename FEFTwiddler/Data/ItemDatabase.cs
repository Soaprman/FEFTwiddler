using System;
using System.Linq;
using System.Xml.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Data
{
    public class ItemDatabase
    {
        private XElement _data;

        public ItemDatabase()
        {
            _data = XElement.Parse(Properties.Resources.Data_Items);
        }

        public Item GetByID(Enums.Item itemId)
        {
            var row = _data
                .Elements("item")
                .Where((x) => x.Attribute("id").Value == ((ushort)itemId).ToString())
                .First();

            // Until data entry is finished in Items.xml, use "name" as a fallback
            var displayName = (row.GetAttribute("displayName") == "BBBBBBB" ? row.GetAttribute("name") : row.GetAttribute("displayName"));

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

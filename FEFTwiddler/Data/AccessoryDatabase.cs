using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Data
{
    public class AccessoryDatabase : BaseDatabase
    {
        public AccessoryDatabase(Enums.Language language) : base(language)
        {
            _data = XElement.Parse(Properties.Resources.Data_Accessories);
        }

        public Accessory GetByID(Enums.Accessory accessoryId)
        {
            var row = _data
                .Elements("accessory")
                .Where((x) => x.Attribute("id").Value == ((byte)accessoryId).ToString())
                .First();

            return FromElement(row);
        }

        public Accessory FromElement(XElement row)
        {
            var displayName = GetDisplayName(row);

            return new Accessory
            {
                AccessoryID = (Enums.Accessory)row.GetAttribute<byte>("id"),
                DisplayName = displayName,
                Type = (Enums.AccessoryType)Enum.Parse(typeof(Enums.AccessoryType), row.GetAttribute("type")),
            };
        }

        /// <summary>
        /// Get all accessories
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<Accessory> GetAll()
        {
            var elements = _data.Elements("accessory");
            var rows = new List<Accessory>();
            foreach (var e in elements)
            {
                rows.Add(FromElement(e));
            }
            return rows;
        }
    }
}

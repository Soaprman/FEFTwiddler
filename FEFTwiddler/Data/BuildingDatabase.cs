using System;
using System.Linq;
using System.Xml.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Data
{
    public class BuildingDatabase : BaseDatabase
    {
        public BuildingDatabase(Enums.Language language) : base(language)
        {
            _data = XElement.Parse(Properties.Resources.Data_Buildings);
        }

        public Building GetByID(Enums.Building buildingId)
        {
            var row = _data
                .Elements("building")
                .Where((x) => x.Attribute("id").Value == ((ushort)buildingId).ToString())
                .First();

            var displayName = GetDisplayName(row);

            return new Building
            {
                BuildingID = (Enums.Building)row.GetAttribute<ushort>("id"),
                DisplayName = displayName
            };
        }
    }
}

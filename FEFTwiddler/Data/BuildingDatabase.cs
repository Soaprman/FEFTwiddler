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
            LoadData(Properties.Resources.Data_Buildings);
        }

        public Building GetByID(Enums.Building buildingId)
        {
            var row = _data
                .Elements("building")
                .Where((x) => x.Attribute("id").Value == ((ushort)buildingId).ToString())
                .First();

            return FromElement(row);
        }

        private Building FromElement(XElement row)
        {
            var displayName = GetDisplayName(row);

            var properties = row.Elements("properties").First();
            var image = row.Elements("image").First();

            return new Building
            {
                BuildingID = (Enums.Building)row.GetAttribute<ushort>("id"),
                DisplayName = displayName,
                Size = properties.GetAttribute<int>("size"),
                Rank = (Enums.BuildingRank)properties.GetAttribute<byte>("rank"),
                IsStatue = properties.GetAttribute<bool>("isStatue"),
                IsGatheringSpot = properties.GetAttribute<bool>("isGatheringSpot"),
                IsLilithsTemple = properties.GetAttribute<bool>("isLilithsTemple"),
                IsTravelersPlaza = properties.GetAttribute<bool>("isTravelersPlaza"),
                ImageName = image.GetAttribute<string>("imageName"),
                UsesRankBackground = image.GetAttribute<bool>("usesRankBackground")
            };
        }
    }
}

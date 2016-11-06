namespace FEFTwiddler.Data
{
    public class Building
    {
        public Enums.Building BuildingID { get; set; }
        public string DisplayName { get; set; }

        public int Size { get; set; }
        public Enums.BuildingRank Rank { get; set; }

        public bool IsStatue { get; set; }
        public bool IsGatheringSpot { get; set; }
        public bool IsLilithsTemple { get; set; }
        public bool IsTravelersPlaza { get; set; }

        public string ImageName { get; set; }
        public bool UsesRankBackground { get; set; }
    }
}

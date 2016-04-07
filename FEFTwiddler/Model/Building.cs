namespace FEFTwiddler.Model
{
    public class Building
    {
        public Enums.Building BuildingID { get; set; }

        /// <summary>
        /// Position from left. Range: 0x01 through 0x1D (29)
        /// </summary>
        public byte LeftPosition { get; set; }

        /// <summary>
        /// Position from top. Range: 0x01 through 0x1E (30)
        /// </summary>
        public byte TopPosition { get; set; }

        /// <summary>
        /// 0: down; 1: left; 2: up; 3: right
        /// </summary>
        public byte DirectionFacing { get; set; }

        /// <summary>
        /// Not actually sure what this is for. But it's 00 on statues and 01 on everything else
        /// </summary>
        /// <remarks>It's also 01 on Travelers' Plaza, so it probably isn't size directly, though it is probably related to size</remarks>
        public byte NotAStatue { get; set; }

        public bool IsLilithsTemple()
        {
            return BuildingID == Enums.Building.LilithsTemple_1 ||
                BuildingID == Enums.Building.LilithsTemple_2 ||
                BuildingID == Enums.Building.LilithsTemple_3;
        }

        public bool IsTravelersPlaza()
        {
            return BuildingID == Enums.Building.TravelersPlaza_1 ||
                BuildingID == Enums.Building.TravelersPlaza_2 ||
                BuildingID == Enums.Building.TravelersPlaza_3;
        }
    }
}

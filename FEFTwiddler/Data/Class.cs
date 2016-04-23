namespace FEFTwiddler.Data
{
    public class Class
    {
        public Enums.Class ClassID { get; set; }
        public string DisplayName { get; set; }

        public Model.Stat BaseStats;
        public Model.Stat MaximumStats;
        public Model.Stat GrowthRates;

        public bool UsesSword { get; set; }
        public bool UsesLance { get; set; }
        public bool UsesAxe { get; set; }
        public bool UsesShuriken { get; set; }
        public bool UsesBow { get; set; }
        public bool UsesTome { get; set; }
        public bool UsesStaff { get; set; }
        public bool UsesStone { get; set; }
    }
}

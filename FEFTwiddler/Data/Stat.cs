namespace FEFTwiddler.Data
{
    public class Stat
    {
        public Enums.Stat StatID { get; set; }
        public string DisplayName { get; set; }

        public Model.Stat BoonStats;
        public Model.Stat BaneStats;
    }
}

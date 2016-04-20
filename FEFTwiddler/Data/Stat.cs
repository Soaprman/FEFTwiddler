namespace FEFTwiddler.Data
{
    public class Stat
    {
        public Enums.Stat StatID { get; set; }
        public string DisplayName { get; set; }

        public Model.Stat BaseBoonModifiers;
        public Model.Stat MaxBoonModifiers;
        public Model.Stat BaseBaneModifiers;
        public Model.Stat MaxBaneModifiers;
    }
}

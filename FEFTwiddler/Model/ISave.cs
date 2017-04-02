namespace FEFTwiddler.Model
{
    public interface ISave
    {
        Enums.SaveFileType GetSaveFileType();
        void Read();
        void Write();
    }
}

using System.Collections.Generic;

namespace FEFTwiddler.Data
{
    public class Chapter
    {
        public Enums.Chapter ChapterID { get; set; }
        public string DisplayName { get; set; }

        public List<Enums.Chapter> UnlocksChapters { get; set; }
    }
}

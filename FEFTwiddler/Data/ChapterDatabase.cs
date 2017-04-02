using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Data
{
    public class ChapterDatabase : BaseDatabase
    {
        public ChapterDatabase(Enums.Language language) : base(language)
        {
            LoadData(Properties.Resources.Data_Chapters);
        }

        public Chapter GetByID(Enums.Chapter chapterId)
        {
            var row = _data
                .Elements("chapter")
                .Where((x) => x.Attribute("id").Value == ((byte)chapterId).ToString())
                .First();

            return FromElement(row);
        }

        public Chapter FromElement(XElement row)
        {
            var displayName = GetDisplayName(row);

            var chapter = new Chapter
            {
                ChapterID = (Enums.Chapter)row.GetAttribute<byte>("id"),
                DisplayName = displayName,
            };

            chapter.UnlocksChapters = new List<Enums.Chapter>();
            if (row.Element("unlocks") != null)
            {
                foreach (var unlockElem in row.Element("unlocks").Elements("chapter"))
                {
                    chapter.UnlocksChapters.Add((Enums.Chapter)unlockElem.GetAttribute<byte>("id"));
                }
            }

            return chapter;
        }

        public IEnumerable<Chapter> GetAll()
        {
            var elements = _data.Elements("chapter");
            var rows = new List<Chapter>();
            foreach (var e in elements)
            {
                rows.Add(FromElement(e));
            }
            return rows;
        }
    }
}
